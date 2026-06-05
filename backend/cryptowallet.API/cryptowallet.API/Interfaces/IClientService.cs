using cryptowallet.API.Dtos;
using cryptowallet.API.Models;

namespace cryptowallet.API.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAll();
        Task<ClientDto?> GetById(int id);
        Task<ClientDto> Create (Client client);
        Task<bool> Delete (int id);
    }
}
