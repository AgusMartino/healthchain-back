using BLL.Services;
using DOMAIN.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBitacora.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BitacoraController : Controller
    {
        [HttpPost(Name = "AddBitacora")]
        public async Task<ActionResult> AddBitacora(BitacoraRequest bitacoraRequest)
        {
            try
            {
                BitacoraService.Current.AddBitacora(bitacoraRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpPost(Name = "GetBitacora")]
        public async Task<ActionResult<List<BitacoraRequest>>> GetBitacora(FechasRequest fechasRequest)
        {
            try
            {
                List<BitacoraRequest> bitacoraRequests = BitacoraService.Current.GetBitacora(fechasRequest);
                return bitacoraRequests;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }
    }
}
