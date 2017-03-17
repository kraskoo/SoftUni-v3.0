namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Edit : IRenderable<EditGameViewModel>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string navbar = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();
        private readonly string mainEdit = Constants.EditGameHtml.GetContentByName();

        public EditGameViewModel Model { get; set; }

        public string Render()
        {
            return $"{this.header}{this.navbar}{this.GetMain()}{this.footer}";
        }

        private string GetMain()
        {
            return string.Format(
                this.mainEdit,
                this.Model.Id,
                this.Model.Title,
                this.Model.Description,
                this.Model.Thumbnail,
                this.Model.Price,
                this.Model.Size,
                this.Model.Trailer);
        }
    }
}