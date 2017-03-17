namespace Application
{
    using System.Collections.Generic;
    using System.IO;
    using Common;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;
    using SimpleMVC.Routers;

    public static class RoutesTable
    {
        private static IEnumerable<Route> routes;

        public static IEnumerable<Route> Routes => routes ?? (routes = RouteEntries());

        private static IEnumerable<Route> RouteEntries()
        {
            return new[]
            {
                new Route
                {
                    Name = "Favicon",
                    Method = RequestMethod.GET,
                    UrlRegex = "/favicon.ico$",
                    Callable = request =>
                    {
                        var response = new HttpResponse
                        {
                            StatusCode = ResponseStatusCode.Ok,
                            Content = File.ReadAllBytes($"{Constants.SiteContentPath}images/creditcard.ico"),
                            Header = { ContentType = "image/*" }
                        };

                        response.Header.ContentLength = response.Content.Length.ToString();
                        return response;
                    }
                },

                // Redirect example
                new Route
                {
                    Name = "Home",
                    Method = RequestMethod.GET,
                    UrlRegex = "^/$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Found,
                        Header =
                        {
                            Location = "home/index",
                            ContentType = "text/html",
                            Type = HeaderType.HttpResponse
                        }
                    }
                },
                new Route
                {
                    Name = "Bootstrap JS",
                    Method = RequestMethod.GET,
                    UrlRegex = "/js/bootstrap.min.js$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText(
                            $"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/x-javascript" }
                    }
                },
                new Route
                {
                    Name = "JQuery",
                    Method = RequestMethod.GET,
                    UrlRegex = "/scripts/jquery-3.1.1.min.js$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText(
                            $"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/x-javascript" }
                    }
                },
                new Route
                {
                    Name = "CSS",
                    Method = RequestMethod.GET,
                    UrlRegex = "/css/(.+)\\.css$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "text/css" }
                    }
                },
                new Route
                {
                    Name = "MAP",
                    Method = RequestMethod.GET,
                    UrlRegex = "/css/(.+)\\.css\\.map$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/json" }
                    }
                },
                new Route
                {
                    Name = "Font EOT",
                    Method = RequestMethod.GET,
                    UrlRegex = "/fonts/glyphicons-halflings-regular.eot$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/octet-stream" }
                    }
                },
                new Route
                {
                    Name = "Font TTF",
                    Method = RequestMethod.GET,
                    UrlRegex = "/fonts/glyphicons-halflings-regular.ttf$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/octet-stream" }
                    }
                },
                new Route
                {
                    Name = "Font WOFF",
                    Method = RequestMethod.GET,
                    UrlRegex = "/fonts/glyphicons-halflings-regular.woff$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/font-woff" }
                    }
                },
                new Route
                {
                    Name = "Font WOFF2",
                    Method = RequestMethod.GET,
                    UrlRegex = "/fonts/glyphicons-halflings-regular.woff2$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "application/font-woff2" }
                    }
                },
                new Route
                {
                    Name = "Font SVG",
                    Method = RequestMethod.GET,
                    UrlRegex = "/fonts/glyphicons-halflings-regular.svg$",
                    Callable = request => new HttpResponse
                    {
                        StatusCode = ResponseStatusCode.Ok,
                        ContentAsUTF8 = File.ReadAllText($"{Constants.SiteContentPath}{request.Url}"),
                        Header = { ContentType = "image/svg+xml" }
                    }
                },
                new Route
                {
                    Name = "Controller/Action/GET",
                    Method = RequestMethod.GET,
                    UrlRegex = @"^/(.+)/(.+)$",
                    Callable = new ControllerRouter().Handle
                },
                new Route
                {
                    Name = "Controller/Action/POST",
                    Method = RequestMethod.POST,
                    UrlRegex = @"^/(.+)/(.+)$",
                    Callable = new ControllerRouter().Handle
                }
            };
        }
    }
}