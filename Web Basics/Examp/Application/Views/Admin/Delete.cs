namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Delete : IRenderable<DeleteGameViewModel>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string navbar = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();
        private readonly string mainDelete = Constants.DeleteGameHtml.GetContentByName();

        public DeleteGameViewModel Model { get; set; }

        public string Render()
        {
            return $"{this.header}{this.navbar}{this.GetDelete()}{this.footer}";
        }

        private string GetDelete()
        {
            return string.Format(
                this.mainDelete,
                this.Model.Id,
                this.Model.Title);
        }
    }
}