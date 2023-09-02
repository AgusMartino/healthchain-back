﻿using BLL.Service;
using Domain.DOMAIN;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSolicitudes.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SolicitudController : Controller
    {
        [HttpPost(Name = "RegisterSolicitud")]
        public async Task<ActionResult> RegisterSolicitud(Solicitud solicitud)
        {
            try
            {
                SolicitudService.Current.Add(solicitud);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet(Name = "GetOneSolicitud")]
        public async Task<ActionResult<Solicitud>> GetOneSolicitud(GetOneDomain getOneDomain)
        {
            try
            {
                Solicitud solicitud = new Solicitud();
                solicitud = SolicitudService.Current.GetOne(Convert.ToInt32(getOneDomain.cuit), Guid.Parse(getOneDomain.usuario));
                return solicitud;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpGet(Name = "GetAllSolicitudes")]
        public async Task<ActionResult<List<Solicitud>>> GetAllSolicitudes(GetAllDomain getAllDomain)
        {
            try
            {
                List<Solicitud> solicituds = new List<Solicitud>();
                solicituds = (List<Solicitud>)SolicitudService.Current.GetAll(Convert.ToInt32(getAllDomain.tipo), Convert.ToInt32(getAllDomain.cuit));
                return solicituds;
            }
            catch (Exception ex)
            {
                return NotFound();
                throw ex;
            }
        }

        [HttpPut(Name = "UpdateSolicitud")]
        public async Task<ActionResult> UpdateSolicitud(Solicitud solicitud)
        {
            try
            {
                SolicitudService.Current.Updtae(solicitud);
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