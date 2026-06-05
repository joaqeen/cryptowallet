using cryptowallet.API.Datos;
using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using cryptowallet.API.Models;
using Microsoft.EntityFrameworkCore;

namespace cryptowallet.API.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _context.Clients
                .Include(c => c.Transactions)
                .ToListAsync();

            var clientsDto = clients.Select(c => new ClientDto {
                Id = c.Id,
                Nombre = c.Nombre,
                Email = c.Email,
            }).ToList();

            return clientsDto;
        }

        public async Task<ClientDto?> GetById(int id)
        {
            var clients = await _context.Clients
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            if (clients == null) 
            {
                return null;
            }

            var clientDto = new ClientDto()
            {
                Id = clients.Id,
                Nombre = clients.Nombre,
                Email = clients.Email
            };

            return clientDto;
        }

        public async Task<ClientDto> Create (Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            var clientDto = new ClientDto
            {
                Id = client.Id,
                Nombre = client.Nombre,
                Email = client.Email
            };

            return clientDto;
        }

        public async Task<bool> Delete (int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return false;
            }
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
    } 
}
