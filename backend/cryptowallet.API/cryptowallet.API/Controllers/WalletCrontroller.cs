using cryptowallet.API.Dtos;
using cryptowallet.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cryptowallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletCrontroller : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletCrontroller(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult<IEnumerable<WalletDto>>> GetWallet(int idCliente)
        {
            var wallet = await _walletService.GetWallet(idCliente);
            return wallet;
        }
    }
}
