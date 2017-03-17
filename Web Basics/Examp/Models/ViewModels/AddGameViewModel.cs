namespace Models.ViewModels
{
    using System;

    public class AddGameViewModel
    {
        private string formattedHtml;
        private string content;

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageThumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Trailer { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GetFormattedHtml(string header, string navbar, string main, string footer)
        {
            if (string.IsNullOrEmpty(this.formattedHtml))
            {
                this.formattedHtml = $"{header}{navbar}{main}{footer}";
            }

            return formattedHtml;
        }

        public string GetFormattedContent(string content)
        {
            if (string.IsNullOrEmpty(this.content))
            {
                this.content = content;
            }

            return this.content;
        }

        public override string ToString()
        {
            return string.Format(
                this.formattedHtml,
                this.Title,
                this.Description,
                this.ImageThumbnail,
                this.Price,
                this.Size,
                this.Title,
                this.ReleaseDate);
        }
    }
}