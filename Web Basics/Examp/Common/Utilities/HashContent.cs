namespace Common.Utilities
{
    using System.Collections.Generic;
    using System.IO;

    public static class HashContent
    {
        private static readonly IDictionary<string, string> FileByName =
            new Dictionary<string, string>
            {
                {
                    Constants.AdminGamesHtml,
                    $"{Constants.SiteContentPath}{Constants.AdminGamesHtml}"
                },
                { Constants.AdminGameCellHtml, $"{Constants.SiteContentPath}{Constants.AdminGameCellHtml}" },
                {
                    Constants.DeleteGameHtml,
                    $"{Constants.SiteContentPath}{Constants.DeleteGameHtml}"
                },
                { Constants.AddGameHtml, $"{Constants.SiteContentPath}{Constants.AddGameHtml}" },
                { Constants.EditGameHtml, $"{Constants.SiteContentPath}{Constants.EditGameHtml}" },
                { Constants.FooterHtml, $"{Constants.SiteContentPath}{Constants.FooterHtml}" },
                { Constants.GameDetailsHtml, $"{Constants.SiteContentPath}{Constants.GameDetailsHtml}" },
                {
                    Constants.GameDetailsEndHtml,
                    $"{Constants.SiteContentPath}{Constants.GameDetailsEndHtml}"
                },
                { Constants.GameDetailsFormHtml, $"{Constants.SiteContentPath}{Constants.GameDetailsFormHtml}" },
                { Constants.HeaderHtml, $"{Constants.SiteContentPath}{Constants.HeaderHtml}" },
                { Constants.HomeHtml, $"{Constants.SiteContentPath}{Constants.HomeHtml}" },
                { Constants.HomeStartHtml, $"{Constants.SiteContentPath}{Constants.HomeStartHtml}" },
                { Constants.HomeStartLinksHtml, $"{Constants.SiteContentPath}{Constants.HomeStartLinksHtml}" },
                { Constants.HomeEndHtml, $"{Constants.SiteContentPath}{Constants.HomeEndHtml}" },
                { Constants.HomeGameCellHtml, $"{Constants.SiteContentPath}{Constants.HomeGameCellHtml}"},
                { Constants.LoginHtml, $"{Constants.SiteContentPath}{Constants.LoginHtml}" },
                { Constants.NavLoggedHtml, $"{Constants.SiteContentPath}{Constants.NavLoggedHtml}" },
                { Constants.NavLoggedAdminHtml, $"{Constants.SiteContentPath}{Constants.NavLoggedAdminHtml}" },
                {
                    Constants.NavNotLoggedHtml,
                    $"{Constants.SiteContentPath}{Constants.NavNotLoggedHtml}"
                },
                { Constants.Register, $"{Constants.SiteContentPath}{Constants.Register}" }
            };

        private static readonly IDictionary<string, string> ContentByName =
            new Dictionary<string, string>();

        public static string GetHomeStart(this string homeStartHtml, string startLinks = null)
        {
            return string.Format(homeStartHtml, startLinks);
        }

        public static string GetContentByName(this string contentName)
        {
            if (!ContentByName.ContainsKey(contentName))
            {
                ContentByName.Add(contentName, File.ReadAllText(FileByName[contentName]));
            }

            return ContentByName[contentName];
        }
    }
}