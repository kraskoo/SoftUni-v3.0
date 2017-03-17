namespace Application.Views.Admin
{
    using System.Collections.Generic;
    using System.Text;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;
    using Common;
    using Common.Utilities;

    public class Games : IRenderable<IEnumerable<AdminGamesViewModel>>
    {
        private readonly string Header = Constants.HeaderHtml.GetContentByName();
        private readonly string Navbar = Constants.NavHtml;
        private readonly string Footer = Constants.FooterHtml.GetContentByName();
        private readonly string Cell = Constants.AdminGameCellHtml.GetContentByName();

        public IEnumerable<AdminGamesViewModel> Model { get; set; }

        public string Render()
        {
            return $"{this.Header}{this.Navbar}{this.GetAllGamesInCells()}{this.Footer}";
        }

        private string GetAllGamesInCells()
        {
            var gamesOutput = new StringBuilder();
            foreach (var gamesViewModel in this.Model)
            {
                gamesOutput.AppendLine(string.Format(
                        this.Cell,
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