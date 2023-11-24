using BLL.Service;
using Domain.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIusuario.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolController : Controller
    {
        [HttpGet("{id}", Name = "GetRol")]
        public async Task<ActionResult<Rol>> GetRol(string id)
        {
            try
            {
                Rol rol = RolService.Current.GetRol(Convert.ToInt32(id));
                return rol;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
