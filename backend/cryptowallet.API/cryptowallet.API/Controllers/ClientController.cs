    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using cryptowallet.API.Datos;
    using cryptowallet.API.Models;
using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;

namespace cryptowallet.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ClientController : ControllerBase
        {
            private readonly IClientService _clientService;

            public ClientController(IClientService clientService)
            {
                _clientService = clientService;
            }

            // GET: api/Client
            [HttpGet]
            public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
            {
                var clients = await _clientService.GetAll();
                return Ok(clients);
            }

            // GET: api/Client/5
            [HttpGet("{id}")]
            public async Task<ActionResult<ClientDto>> GetById(int id)
            {
                var client = await _clientService.GetById(id);

                if (client == null)
                {
                    return NotFound();
                }

                return Ok(client);
            }

            
            // POST: api/Client
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<ClientDto>> Create(Client client)
            {
                var clientDto = await _clientService.Create(client);

                return CreatedAtAction(nameof(GetAll), new {id = clientDto.Id}, clientDto);
            }

            // DELETE: api/Client/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteClient(int id)
            {
                var boolExito = await _clientService.Delete(id);

                if (!boolExito) 
                {
                    return NotFound();
                }

                return NoContent();
            }

           
        }
    }
