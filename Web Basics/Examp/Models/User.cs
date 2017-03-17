namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using Enums;
    using Interfaces;

    public class User : IUser
    {
        private ICollection<Game> userGames;
        private ICollection<Login> logins;

        public User()
        {
            this.userGames = new HashSet<Game>();
            this.logins = new List<Login>();
        }
        
        public int Id { get; set; }

        [
            Required,
            StringLength(4000, MinimumLength = 1),
            RegularExpression(@"^([a-zA-Z][\w\d]+[\@\.]\w+[\@\.]\w+[a-zA-Z]$)"), Index(IsUnique = true)
        ]
        public string Email { get; set; }

        [Required, StringLength(4000, MinimumLength = 1), RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,})")]
        public string Password { get; set; }

        [Required, StringLength(4000, MinimumLength = 1), RegularExpression("(^[a-zA-Z -.]+$)")]
        public string FullName { get; set; }

        public UserRole UserRole { get; set; }

        public virtual ICollection<Game> UserGames
        {
            get { return this.userGames; }
            set { this.userGames = value; }
        }

        public virtual ICollection<Login> Logins
        {
            get { return this.logins; }
            set { this.logins = value; }
        }
    }
}