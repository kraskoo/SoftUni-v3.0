﻿namespace Application.Views.Users
{
    using System.Text;
    using Common;
    using Common.Utilities;
    using SimpleMVC.Interfaces;

    public class Register : IRenderable
    {
        private readonly string header = Constants.HeaderHtml.GetContentByName();
        private readonly string nav = Constants.NavHtml;
        private readonly string footer = Constants.FooterHtml.GetContentByName();

        public string Render()
        {
            var outputHtml = new StringBuilder();
            outputHtml.AppendLine(header)
                .AppendLine(nav)
                .AppendLine(Constants.Register.GetContentByName())
                .AppendLine(footer);
            return outputHtml.ToString();
        }
    }
}