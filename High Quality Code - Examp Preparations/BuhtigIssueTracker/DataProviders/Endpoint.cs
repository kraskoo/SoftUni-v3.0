namespace BuhtigIssueTracker.DataProviders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Interfaces;

    public class Endpoint : IEndpoint
    {
        public Endpoint(string actionName)
        {
            int questionMarkIndex = actionName.IndexOf('?');
            if (questionMarkIndex != -1)
            {
                this.ActionName = actionName.Substring(0, questionMarkIndex);
                var pairs = actionName
                    .Substring(questionMarkIndex + 1)
                        .Split('&')
                        .Select(x => x.Split('=')
                        .Select(WebUtility.UrlDecode)
                        .ToArray());
                var parameters = new Dictionary<string, string>();
                foreach (var pair in pairs)
                {
                    parameters.Add(pair[0], pair[1]);
                }

                this.Parameters = parameters;
            }
            else
            {
                this.ActionName = actionName;
            }
        }

        public string ActionName { get; }

        public IDictionary<string, string> Parameters { get; }
    }
}