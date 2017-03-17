namespace Models.ViewModels
{
    using Common;
    using Common.Utilities;

    public class EditGameViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Trailer { get; set; }

        public override string ToString()
        {
            return string.Format(
                Constants.EditGameHtml.GetContentByName(),
                this.Id,
                this.Title,
                this.Description,
                this.Thumbnail,
                this.Price,
                this.Size,
                this.Trailer);
        }
    }
}