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

        public async Task<TransactionDto> Create(Transaction transaction)
        {
            var precio = await _cryptoYaService.GetPrice(transaction.CodCrypto, transaction.Accion);
            transaction.TotalMoneda = precio * transaction.CantidadCrypto;

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
