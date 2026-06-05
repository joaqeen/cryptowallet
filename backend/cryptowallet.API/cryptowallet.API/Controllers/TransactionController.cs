using cryptowallet.API.Datos;
using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using cryptowallet.API.Models;
using cryptowallet.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cryptowallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }



        // GET: api/Transaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll() //Muestra todas las transacciones de todos los clientes
        {
            var transactions = await _transactionService.GetAll();
            return Ok(transactions);
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetById(int id) //Muestra transaccion segun su ID.
        {
            var transaction = await _transactionService.GetById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAllByClient(int clientId) //Muestra historial de transacciones segun cliente
        {
            var clientTransactions = await _transactionService.GetAllByClient(clientId);
            return Ok(clientTransactions);
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Transaction transaction)
        {
            var boolExito = await _transactionService.Update(id, transaction);

            if (!boolExito)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> Create(Transaction transaction)
        {
            var transactionDto = await _transactionService.Create(transaction);
            if(transactionDto == null)
            {
                return BadRequest("No tenés suficiente cryptos para vender.");
            }

            return CreatedAtAction(nameof(GetAll), new { id = transactionDto.Id }, transactionDto);
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var boolExito = await _transactionService.Delete(id);

            if (!boolExito)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
