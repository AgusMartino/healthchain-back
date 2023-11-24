using BLL.Service;
using Domain.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiABMs.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmpresaController : Controller
    {
        [HttpPost(Name = "RegisterEmpresa")]
        public async Task<ActionResult> RegisterEmpresa(Empresa empresa)
        {
            try
            {
                EmpresaService.Current.Add(empresa);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{cuit}",Name = "GetOneEmpresa")]
        public async Task<ActionResult<Empresa>> GetOneEmpresa(string cuit)
        {
            try
            {
                Empresa empresa = new Empresa();
                empresa = EmpresaService.Current.GetOne(cuit);
                return empresa;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet( Name = "GetAllEmpresa")]
        public async Task<ActionResult<List<Empresa>>> GetAllEmpresa()
        {
            try
            {
                List<Empresa> empresa = new List<Empresa>();
                empresa = (List<Empresa>)EmpresaService.Current.GetAll();
                return empresa;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{username}",Name = "GetAllEmpresaAsociadasMedico")]
        public async Task<ActionResult<List<Empresa>>> GetAllEmpresaAsociadasMedico(string username)
        {
            try
            {
                List<Empresa> Empresa = new List<Empresa>();
                Empresa = (List<Empresa>)EmpresaService.Current.GetAllEmpresasAsociadasMedico(username);
                return Empresa;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
