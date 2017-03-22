namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Interfaces;

    public class Game : IModel
    {
        private ICollection<User> owners;

        public Game()
        {
            this.owners = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required, StringLength(100, MinimumLength = 3), RegularExpression(@"([A-Z][\w\W]{2,100})")]
        public string Title { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Size { get; set; }

        [RegularExpression(@"([\w\W]{11})")]
        public string Trailer { get; set; }

        [RegularExpression(@"(https?:\/\/[\w\W]+)")]
        public string ImageThumbnail { get; set; }

        [Required, StringLength(4000, MinimumLength = 20)]
        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual ICollection<User> Owners
        {
            get { return this.owners; }
            set { this.owners = value; }
        }
    }
}