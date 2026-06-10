using cryptowallet.API.Datos;
using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using cryptowallet.API.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace cryptowallet.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        private readonly ICryptoYaService _cryptoYaService;
        public TransactionService(AppDbContext context, ICryptoYaService cryptoYaService)
        {
            _context = context;
            _cryptoYaService = cryptoYaService;
        }

        public async Task<List<TransactionDto>> GetAll()
        {
            var transaction = await _context.Transactions
                .Include(t => t.Clients)
                .ToListAsync();

            var transactionDto = transaction.Select(t => new TransactionDto
            {
                Id = t.Id,
                Accion = t.Accion,
                CodCrypto = t.CodCrypto,
                IdCliente = t.IdCliente,
                CantidadCrypto = t.CantidadCrypto,
                TotalMoneda = t.TotalMoneda,
                Fecha = t.Fecha,
            }).ToList();

            return transactionDto;
        }

        public async Task<TransactionDto?> GetById(int id)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                return null;
            }

            var transactionDto = new TransactionDto()
            {
                Id = transaction.Id,
                Accion = transaction.Accion,
                CodCrypto = transaction.CodCrypto,
                IdCliente = transaction.IdCliente,
                CantidadCrypto = transaction.CantidadCrypto,
                TotalMoneda = transaction.TotalMoneda,
                Fecha = transaction.Fecha,
            };

            return transactionDto;
        }

        public async Task<List<TransactionDto>> GetAllByClient(int idCliente)
        {
            var clientTransactions = await _context.Transactions
                .Where(t => t.IdCliente == idCliente)
                .OrderByDescending(t => t.Fecha)
                .ToListAsync();

            if (clientTransactions == null)
            {
                return null;
            }

            var transactionDto = clientTransactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Accion = t.Accion,
                CodCrypto = t.CodCrypto,
                IdCliente = t.IdCliente,
                CantidadCrypto = t.CantidadCrypto,
                TotalMoneda = t.TotalMoneda,
                Fecha = t.Fecha,
            }).ToList();

            return transactionDto;
        }

        public async Task<TransactionDto> Create(Transaction transaction)
        {
            if (transaction.Accion == "sale") //Si la accion de la transaccion es vender, debemos verificar si contamos con las cryptos disponibles para venderlas.
            {
                var transactions = await _context.Transactions
                    .Where(t => t.IdCliente == transaction.IdCliente && t.CodCrypto == transaction.CodCrypto) //t -> es la transaccion que trae de la DB.
                    .ToListAsync();  //guardamos en transactions las transacciones del mismo cliente con la misma moneda.

                var totalPurchase = transactions.Where(t => t.Accion == "purchase").Sum(t => t.CantidadCrypto); //calculamos el total de ventas de la moneda del cliente
                var totalSale = transactions.Where(t => t.Accion == "sale").Sum(t => t.CantidadCrypto); //calculamos el total de compras de la moneda del cliente

                var disponible = totalPurchase - totalSale; //total compras - total ventas te da el disponible de cryptos que tiene en la billetera el cliente.

                if (transaction.CantidadCrypto > disponible) //si la cantidad de cripto que quiere vender es mayor al disponible retornamos null y en el controller damos el error.
                {
                    return null; 
                }
            }
    
            var precio = await _cryptoYaService.GetPrice(transaction.CodCrypto, transaction.Accion);
            transaction.TotalMoneda = precio * transaction.CantidadCrypto; //Calculamos el precio de la moneda * la cantidad y lo guardamos a Total Moneda

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            var transactionDto = new TransactionDto
            {
                Id = transaction.Id,
                Accion = transaction.Accion,
                CodCrypto = transaction.CodCrypto,
                IdCliente = transaction.IdCliente,
                CantidadCrypto = transaction.CantidadCrypto,
                TotalMoneda = transaction.TotalMoneda,
                Fecha = transaction.Fecha,
            };

            return transactionDto;
        }

        public async Task<bool> Update(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return false;
            }

            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var transaction = await _context.Transactions
                .FindAsync(id);

            if (transaction == null)
            {
                return false;
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
