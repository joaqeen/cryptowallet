using cryptowallet.API.Dtos;
using cryptowallet.API.Models;

namespace cryptowallet.API.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetAll();
        Task<TransactionDto?> GetById(int id);
        Task<TransactionDto> Create (Transaction transaction);
        Task<bool> Update (int id, Transaction transaction);
        Task<bool> Delete (int id);
        
    }
}
