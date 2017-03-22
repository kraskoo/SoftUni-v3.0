namespace Application.Views.Admin
{
    using System.Collections.Generic;
    using System.Text;
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Games : IRenderable<IEnumerable<AdminGamesViewModel>>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string navbar = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();
        private readonly string cell = Constants.AdminGameCellHtml.GetContentByName();

        public IEnumerable<AdminGamesViewModel> Model { get; set; }

        public string Render()
        {
            return $"{this.header}{this.navbar}{this.GetAllGamesInCells()}{this.footer}";
        }

        private string GetAllGamesInCells()
        {
            var gamesOutput = new StringBuilder();
            foreach (var gamesViewModel in this.Model)
            {
                gamesOutput.AppendLine(string.Format(
                        this.cell,
                        gamesViewModel.Title,
                        gamesViewModel.Price,
                        gamesViewModel.Size,
                        gamesViewModel.GameId,
                        gamesViewModel.GameId));
            }
            
            return string.Format(Constants.AdminGamesHtml.GetContentByName(), gamesOutput);
        }
    }
}