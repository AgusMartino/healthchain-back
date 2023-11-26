using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBlockChain.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BilleteraController : Controller
    {
        [HttpGet("{id}", Name = "CreateWallet")]
        public async Task<ActionResult> CreateWalletUser(string id)
        {
            try
            {
                BilleteraService.Current.CreateWalletUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{id}", Name = "CreateWalletCompany")]
        public async Task<ActionResult> CreateWalletCompany(string id)
        {
            try
            {
                BilleteraService.Current.CreateWalletCompany(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
