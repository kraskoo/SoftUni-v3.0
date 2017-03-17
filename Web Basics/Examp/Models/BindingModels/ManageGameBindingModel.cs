namespace Models.BindingModels
{
    using System;
    using Interfaces;

    public class ManageGameBindingModel : IModel
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