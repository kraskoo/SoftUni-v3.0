namespace Application.Views.Home
{
    using System.Collections.Generic;
    using System.Text;
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Index : IRenderable<IEnumerable<HomeGameViewModel>>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string navNotLogged = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();
        private readonly string homeStart = Constants.HomeStart;
        private readonly string homeEnd = Constants.HomeEndHtml.GetContentByName();

        public IEnumerable<HomeGameViewModel> Model { get; set; }

        public string Render()
        {
            var homeGames = this.GetHomeGames();
            return this.GetOutputHtml(homeGames);
        }

        private string GetHomeGames()
        {
            var homeGamesOutput = new StringBuilder();
            homeGamesOutput.AppendLine(homeStart);
            int counter = 0;
            foreach (var homeGame in this.Model)
            {
                if (counter != 0 && counter % 3 == 0)
                {
                    homeGamesOutput.AppendLine("</div>");
                }

                if (counter % 3 == 0)
                {
                    homeGamesOutput.AppendLine("<div class=\"card-group\">");
                }

                homeGamesOutput.AppendLine(homeGame.ToString());
                counter++;
            }

            homeGamesOutput.AppendLine(homeEnd);
            return homeGamesOutput.ToString();
        }

        private string GetOutputHtml(string homeGames)
        {
            return $"{this.header}{this.navNotLogged}{homeGames}{this.footer}";
        }
    }
}