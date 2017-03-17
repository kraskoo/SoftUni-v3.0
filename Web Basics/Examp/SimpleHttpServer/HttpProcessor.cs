namespace SimpleHttpServer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using Enums;
    using Models;
    using Utilities;

    public class HttpProcessor
    {
        private IList<Route> routes;
        private HttpRequest request;
        private HttpResponse response;
        private IDictionary<string, HttpSession> sessions;

        public HttpProcessor(IEnumerable<Route> routes, IDictionary<string, HttpSession> sessions)
        {
            this.routes = new List<Route>(routes);
            this.sessions = sessions;
        }

        public void HandleClient(TcpClient tcpClient)
        {
            using (var stream = tcpClient.GetStream())
            {
                this.request = this.GetRequest(stream);
                this.response = this.RouteRequest();
                Console.WriteLine("-RESPONSE-------------");
                Console.WriteLine(this.response.Header);

                // Console.WriteLine(Encoding.UTF8.GetString(response.Content));
                // Console.WriteLine("----------------------");
                StreamUtils.WriteResponse(stream, this.response);
            }
        }

        private HttpRequest GetRequest(Stream inputStream)
        {
            // Read request Line
            string requestLine = StreamUtils.ReadLine(inputStream);
            string[] tokens = requestLine.Split(' ');

            while (tokens.Length != 3)
            {
                requestLine = StreamUtils.ReadLine(inputStream);
                tokens = requestLine.Split(' ');
            }

            RequestMethod method = (RequestMethod)Enum.Parse(typeof(RequestMethod), tokens[0].ToUpper());
            string url = tokens[1];
            string protocolVersion = tokens[2];

            // Read Headers
            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(inputStream)) != null)
            {
                if (line.Equals(string.Empty))
                {
                    break;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }

                string name = line.Substring(0, separator);
                int pos = separator + 1;
                while (pos < line.Length && line[pos] == ' ')
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                if (name == "Cookie")
                {
                    string[] cookieSaves = value.Split(';');
                    foreach (var cookieSave in cookieSaves)
                    {
                        string[] cookiePair = cookieSave.Split('=').Select(x => x.Trim()).ToArray();
                        var cookie = new Cookie(cookiePair[0], cookiePair[1]);
                        header.AddCookie(cookie);
                    }
                }
                else if (name == "Location")
                {
                    header.Location = value;
                }
                else if (name == "Content-Length")
                {
                    header.ContentLength = value;
                }
                else
                {
                    header.OtherParameters.Add(name, value);
                }
            }

            string content = null;
            if (header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);
                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            var httpRequest = new HttpRequest
            {
                Method = method,
                Url = url,
                Header = header,
                Content = content
            };

            if (httpRequest.Header.Cookies.Contains("sessionId"))
            {
                var sessionId = httpRequest.Header.Cookies["sessionId"].Value;
                httpRequest.Session = new HttpSession(sessionId);
                if (!this.sessions.ContainsKey(sessionId))
                {
                    this.sessions.Add(sessionId, httpRequest.Session);
                }
            }

            Console.WriteLine("-REQUEST-----------------------------");
            Console.WriteLine(httpRequest);
            Console.WriteLine("------------------------------");
            return httpRequest;
        }

        private HttpResponse RouteRequest()
        {
            var routeList = this.routes
                .Where(x => Regex.Match(this.request.Url, x.UrlRegex).Success)
                .ToList();

            if (!routeList.Any())
            {
                return HttpResponseBuilder.NotFound();
            }

            var currentRoute = routeList.FirstOrDefault(x => x.Method == this.request.Method);
            if (currentRoute == null)
            {
                return new HttpResponse
                {
                    StatusCode = ResponseStatusCode.MethodNotAllowed
                };
            }

            // trigger the route handler...
            try
            {
                HttpResponse httpResponse;
                if (!this.request.Header.Cookies.Contains("sessionId") || this.request.Session == null)
                {
                    var session = SessionCreator.Create();
                    var sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
                    this.request.Session = session;
                    httpResponse = currentRoute.Callable(this.request);
                    httpResponse.Header.AddCookie(sessionCookie);
                }
                else
                {
                    httpResponse = currentRoute.Callable(this.request);
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return HttpResponseBuilder.InternalServerError();
            }
        }
    }
}