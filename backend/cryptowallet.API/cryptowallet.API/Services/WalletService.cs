using cryptowallet.API.Datos;
using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace cryptowallet.API.Services
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _context;
        private readonly ICryptoYaService _cryptoYaService;
        public WalletService(AppDbContext context, ICryptoYaService cryptoYaService)
        {
            _context = context;
            _cryptoYaService = cryptoYaService;
        }

        public async Task<List<WalletDto>> GetWallet(int idCliente)
        {
            var clientTransactions = await _context.Transactions
                .Where(t => t.IdCliente == idCliente)
                .GroupBy(t => t.CodCrypto)
                .ToListAsync(); //Traemos las transacciones filtradas por cliente y agrupadas por Crypto.

            var walletItems = new List<WalletDto>(); 

            foreach (var cryptoGroup in clientTransactions)
            {
                var compras = cryptoGroup.Where(t => t.Accion == "purchase").Sum(t => t.CantidadCrypto);
                var ventas = cryptoGroup.Where(t => t.Accion == "sale").Sum(t => t.CantidadCrypto);
                var disponible = compras - ventas;

                if (disponible > 0)
                {
                    var precio = await _cryptoYaService.GetPrice(cryptoGroup.Key, "purchase");
                    walletItems.Add(new WalletDto
                    {
                        CryptoCode = cryptoGroup.Key,
                        Cantidad = disponible,
                        Total = disponible * precio
                    });
                }
            }

            return walletItems;


        }
    }
}
