namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Edit : IRenderable<EditGameViewModel>
    {
        private readonly string Header = Constants.HeaderHtml.GetContentByName();
        private readonly string Navbar = Constants.NavHtml;
        private readonly string Footer = Constants.FooterHtml.GetContentByName();
        private readonly string MainEdit = Constants.EditGameHtml.GetContentByName();

        public EditGameViewModel Model { get; set; }

        public string Render()
        {
            return $"{this.Header}{this.Navbar}{this.GetMain()}{this.Footer}";
        }

        private string GetMain()
        {
            return string.Format(
                this.MainEdit,
                Model.Id,
                Model.Title,
                Model.Description,
                Model.Thumbnail,
                Model.Price,
                Model.Size,
                Model.Trailer);
        }
    }
}