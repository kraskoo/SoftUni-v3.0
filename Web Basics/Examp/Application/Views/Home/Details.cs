namespace Application.Views.Home
{
    using System;
    using System.Text;
    using Common;
    using Common.Utilities;
    using Models.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Details : IRenderable<DetailsGameViewModel>
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string nav = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();

        public DetailsGameViewModel Model { get; set; }

        public string Render()
        {
            var outputHtml = new StringBuilder();
            outputHtml.AppendLine(this.header)
                .AppendLine(this.nav)
                .AppendLine(
                $"{this.Model}{Constants.GameDetailsForm}{Constants.GameDetailsEndHtml.GetContentByName()}")
                .AppendLine(this.footer);
            Console.WriteLine(outputHtml.ToString());
            return outputHtml.ToString();
        }
    }
}