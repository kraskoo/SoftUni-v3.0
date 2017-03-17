namespace Models.ViewModels
{
    using System;
    using Common;
    using Common.Utilities;
    using Utilities;

    public class DetailsGameViewModel
    {
        public string Title { get; set; }

        public string Trailer { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public DateTime ReleaseDate { get; set; }

        public override string ToString()
        {
            return string.Format(
                Constants.GameDetailsHtml.GetContentByName(),
                this.Title,
                this.Trailer,
                this.Description,
                this.Price,
                this.Size,
                this.ReleaseDate.GetFormatedDate());
        }
    }
}