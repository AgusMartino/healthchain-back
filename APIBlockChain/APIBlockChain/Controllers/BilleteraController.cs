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
        public async Task<ActionResult> CreateWalletUser(string id_user)
        {
            try
            {
                BilleteraService.Current.CreateWalletUser(id_user);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet("{id}", Name = "CreateWalletCompany")]
        public async Task<ActionResult> CreateWalletCompany(string id_company)
        {
            try
            {
                BilleteraService.Current.CreateWalletCompany(id_company);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }
    }
}
