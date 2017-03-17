namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using SimpleMVC.Interfaces.Generic;
    using Models.ViewModels;

    public class Delete : IRenderable<DeleteGameViewModel>
    {
        private readonly string Header = Constants.HeaderHtml.GetContentByName();
        private readonly string Navbar = Constants.NavHtml;
        private readonly string Footer = Constants.FooterHtml.GetContentByName();
        private readonly string MainDelete = Constants.DeleteGameHtml.GetContentByName();

        public DeleteGameViewModel Model { get; set; }

        public string Render()
        {
            return $"{this.Header}{this.Navbar}{this.GetDelete()}{this.Footer}";
        }

        private string GetDelete()
        {
            return string.Format(
                this.MainDelete,
                this.Model.Id,
                this.Model.Title);
        }
    }
}