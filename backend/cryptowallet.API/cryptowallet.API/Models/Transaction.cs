using System.ComponentModel.DataAnnotations;

namespace cryptowallet.API.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Accion {  get; set; }
        [Required]
        public string CodCrypto { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public decimal CantidadCrypto { get; set; }
        [Required]
        public decimal TotalMoneda { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public Client? Clients { get; set; }

    }
}
