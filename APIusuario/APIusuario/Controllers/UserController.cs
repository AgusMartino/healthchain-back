using Domain.DOMAIN;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIusuario.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpPost(Name = "RegisterUser")]
        public async Task<ActionResult> RegisterUser(Usuario usuario)
        {
            try
            {
                UserService.Current.RegisterUser(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet("{usuario}", Name = "ValidateUser")]
        public async Task<ActionResult<Usuario>> ValidateUser(string usuario)
        {
            try
            {
                Usuario user = UserService.Current.ValidateUser(usuario);
                return user;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet(Name = "LoginUserBO")]
        public async Task<ActionResult<bool>> LoginUserBO(Usuario usuario)
        {
            try
            {
                bool user = UserService.Current.LoginUserBO(usuario);
                return user;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser(Usuario usuario)
        {
            try
            {
                UserService.Current.UpdateUsuario(usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet("{usuario}", Name = "GetUser")]
        public async Task<ActionResult<Usuario>> GetUser(string usuario)
        {
            try
            {
                Usuario user = UserService.Current.GetUser(usuario);
                return user;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet("{cuit_empresa}", Name = "GetUsersEmpresas")]
        public async Task<ActionResult<List<Usuario>>> GetUsersEmpresas(string cuit_empresa)
        {
            try
            {
                List<Usuario> users = (List<Usuario>)UserService.Current.GetAll((int)Convert.ToUInt32(cuit_empresa));
                return users;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

    }
}
