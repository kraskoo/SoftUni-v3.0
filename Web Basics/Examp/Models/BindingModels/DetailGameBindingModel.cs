namespace Models.BindingModels
{
    using System;

    public class DetailGameBindingModel
    {
        public string Title { get; set; }

        public string Trailer { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}