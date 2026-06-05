using cryptowallet.API.Dtos;

namespace cryptowallet.API.Interfaces
{
    public interface IWalletService
    {
        public Task<List<WalletDto>> GetWallet(int idCliente); 
    }
}
