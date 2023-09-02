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
                return NotFound();
                throw ex;
            }
        }

        [HttpGet("{cuit}",Name = "GetOneEmpresa")]
        public async Task<ActionResult<Empresa>> GetOneEmpresa(int cuit)
        {
            try
            {
                Empresa empresa = new Empresa();
                empresa = EmpresaService.Current.GetOne(cuit);
                return empresa;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
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
                return NotFound();
                throw ex;
            }
        }
    }
}
