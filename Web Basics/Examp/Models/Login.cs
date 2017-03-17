namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    public class Login : ILogin
    {
        public int Id { get; set; }

        [Required, StringLength(4000, MinimumLength = 1), Index(IsUnique = true)]
        public string SessionId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}