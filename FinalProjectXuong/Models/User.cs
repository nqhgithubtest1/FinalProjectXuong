using System.ComponentModel.DataAnnotations;

namespace FinalProjectXuong.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public Cart Cart { get; set; }
    }
}
