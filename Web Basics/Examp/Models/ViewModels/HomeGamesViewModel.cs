namespace Models.ViewModels
{
    using Common;
    using Common.Utilities;

    public class HomeGameViewModel
    {
        private string content;

        public string ImageThumbnail { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Description { get; set; }

        public int Id { get; set; }

        public override string ToString()
        {
            this.content = string.Format(
                    Constants.HomeGameCellHtml.GetContentByName(),
                    this.ImageThumbnail,
                    this.Title,
                    this.Price,
                    this.Size,
                    this.Description,
                    $"/home/details?id={this.Id}");
            return this.content;
        }
    }
}