namespace Application.Views.Admin
{
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Add : IRenderable<AddGameViewModel>
    {
        private readonly string Header = Constants.HeaderHtml.GetContentByName();
        private readonly string Navbar = Constants.NavHtml;
        private readonly string Footer = Constants.FooterHtml.GetContentByName();
        private readonly string MainAdd = Constants.AddGameHtml.GetContentByName();

        public AddGameViewModel Model { get; set; }

        public string Render()
        {
            string content = this.Model.GetFormattedContent(this.MainAdd);
            return this.Model.GetFormattedHtml(this.Header, this.Navbar, content, this.Footer);
        }
    }
}