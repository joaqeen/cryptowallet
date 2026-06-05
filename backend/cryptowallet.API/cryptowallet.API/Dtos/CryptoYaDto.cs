namespace cryptowallet.API.Dtos
{
    public class CryptoYaDto
    {
        public decimal Ask { get; set; }
        public decimal TotalAsk { get; set; }
        public decimal Bid { get; set; }
        public decimal TotalBid { get; set; }
        public long Time { get; set; }
    }
}
