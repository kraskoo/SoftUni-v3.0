namespace Models.BindingModels
{
    using System;

    public class AddGameBindingModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageThumbnail { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public string Trailer { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}