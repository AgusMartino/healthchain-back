using BLL.Services;
using DOMAIN.DomainDal;
using DOMAIN.DomainRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBlockChain.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransaccionController : Controller
    {
        [HttpGet("{TrxTransaccion}", Name = "getOneTransaccion")]
        public async Task<ActionResult<Transaccion>> getOneTransaccion(string TrxTransaccion)
        {
            try
            {
                Transaccion transaccion = new Transaccion();
                transaccion = TransaccionService.Current.getOne(TrxTransaccion);
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet(Name = "getListTransaccion")]
        public async Task<ActionResult<List<Transaccion>>> getListTransaccion()
        {
            try
            {
                List<Transaccion> transaccion = new List<Transaccion>();
                transaccion = (List<Transaccion>)TransaccionService.Current.getTransaccionesList();
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("{user}",Name = "getListTransaccionUser")]
        public async Task<ActionResult<List<Transaccion>>> getListTransaccionUser(string user)
        {
            try
            {
                List<Transaccion> transaccion = new List<Transaccion>();
                transaccion = (List<Transaccion>)TransaccionService.Current.getTransaccionesUser(user);
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "getListTransaccionFechas")]
        public async Task<ActionResult<List<Transaccion>>> getListTransaccionFechas(FechasRequest fechasRequest)
        {
            try
            {
                List<Transaccion> transaccion = new List<Transaccion>();
                transaccion = (List<Transaccion>)TransaccionService.Current.getTransaccionesFechas(fechasRequest.fechaInicio, fechasRequest.fechaFin);
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "getListTransaccionFechasUser")]
        public async Task<ActionResult<List<Transaccion>>> getListTransaccionFechasUser(FechasRequest fechasRequest)
        {
            try
            {
                List<Transaccion> transaccion = new List<Transaccion>();
                transaccion = (List<Transaccion>)TransaccionService.Current.getTransaccionesFechasUser(fechasRequest.fechaInicio, fechasRequest.fechaFin, fechasRequest.user);
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(Name = "getListTransaccionFechasCompany")]
        public async Task<ActionResult<List<Transaccion>>> getListTransaccionFechasCompany(FechasRequest fechasRequest)
        {
            try
            {
                List<Transaccion> transaccion = new List<Transaccion>();
                transaccion = (List<Transaccion>)TransaccionService.Current.getTransaccionesFechasCompany(fechasRequest.fechaInicio, fechasRequest.fechaFin, fechasRequest.user);
                return transaccion;
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
