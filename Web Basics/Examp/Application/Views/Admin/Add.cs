namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Add : IRenderable<AddGameViewModel>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string navbar = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();
        private readonly string mainAdd = Constants.AddGameHtml.GetContentByName();

        public AddGameViewModel Model { get; set; }

        public string Render()
        {
            string content = this.Model.GetFormattedContent(this.mainAdd);
            return this.Model.GetFormattedHtml(this.header, this.navbar, content, this.footer);
        }
    }
}