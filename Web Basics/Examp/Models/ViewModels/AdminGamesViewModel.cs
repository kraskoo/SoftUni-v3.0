namespace Models.ViewModels
{
    using Common;
    using Common.Utilities;

    public class AdminGamesViewModel
    {
        public string Title { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public int GameId { get; set; }

        public override string ToString()
        {
            return string.Format(
                Constants.AdminGameCellHtml.GetContentByName(),
                this.Title,
                this.Size,
                this.Price,
                this.GameId,
                this.GameId);
        }
    }
}