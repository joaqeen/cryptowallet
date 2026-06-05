namespace cryptowallet.API.Interfaces
{
    public interface ICryptoYaService
    {
        public Task<decimal> GetPrice(string cryptoCode, string action);
    }
}
