namespace cryptowallet.API.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Accion { get; set; }
        public string CodCrypto {  get; set; }
        public int IdCliente { get; set; }
        public decimal CantidadCrypto { get; set; }
        public decimal TotalMoneda { get; set; }
        public DateTime Fecha { get; set; }
    }
}
