using BLL.Service;
using Domain.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSolicitudes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TipoSolicitudController : Controller
    {
        [HttpGet("{id}", Name = "GetOneTipoSolicitud")]
        public async Task<ActionResult<tipo_solicitud>> GetOneTipoSolicitud(int id)
        {
            try
            {
                tipo_solicitud tipo_Solicitud = new tipo_solicitud();
                tipo_Solicitud = Tipo_solicitudService.Current.GetOne(id);
                return tipo_Solicitud;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet(Name = "GetAllTipoSolicitud")]
        public async Task<ActionResult<List<tipo_solicitud>>> GetAllTipoSolicitud()
        {
            try
            {
                List<tipo_solicitud> tipo_Solicitud = new List<tipo_solicitud>();
                tipo_Solicitud = (List<tipo_solicitud>)Tipo_solicitudService.Current.GetAll();
                return tipo_Solicitud;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
