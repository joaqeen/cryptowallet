using System.ComponentModel.DataAnnotations;

namespace cryptowallet.API.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
    }
}
