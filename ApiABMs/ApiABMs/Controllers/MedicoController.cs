using BLL.Service;
using Domain.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiABMs.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MedicoController : Controller
    {
        [HttpGet("{cuit_empresa}", Name = "GetAllMedicosEmpresas")]
        public async Task<ActionResult<List<Medico>>> GetAllMedicosEmpresas(string cuit_empresa)
        {
            try
            {
                List<Medico> medicos = new List<Medico>();
                medicos = (List<Medico>)MedicoService.Current.getAllmedicosEmpresas(cuit_empresa);
                return medicos;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{username}", Name = "GetMedico")]
        public async Task<ActionResult<Medico>> GetMedico(string username)
        {
            try
            {
                Medico medico = new Medico();
                medico = MedicoService.Current.GetMedico(username);
                return medico;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "AddEspecialidad")]
        public async Task<ActionResult> AddEspecialidad(Medico medico)
        {
            try
            {
                MedicoService.Current.RegisterEspecialidad(medico);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
