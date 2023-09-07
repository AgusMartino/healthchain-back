using DAL.Manager;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace BLL.Service
{

    public sealed class SolicitudService
    {
        #region singleton
        private readonly static SolicitudService _instance = new SolicitudService();

        public static SolicitudService Current
        {
            get
            {
                return _instance;
            }
        }

        private SolicitudService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Solicitud GetOne(int cuit, Guid usuario)
        {
            try
            {
                string[] criterios = { "cuit_empresa", "id_usuario" };
                string[] valores = { cuit.ToString(), usuario.ToString() };
                Solicitud solicitud = new Solicitud();
                solicitud = SolicitudManager.Current.GetOne(criterios, valores);
                return solicitud;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Add(Solicitud solicitud)
        {
            try
            {
                solicitud.id_solicitud = Guid.NewGuid().ToString();
                solicitud.fecha_creacion = DateTime.Now;
                solicitud.fecha_modificacion = DateTime.Now;
                SolicitudManager.Current.Add(solicitud);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void Updtae(Solicitud solicitud)
        {
            try
            {
                solicitud.fecha_modificacion = DateTime.Now;
                SolicitudManager.Current.Update(solicitud);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Solicitud> GetAll(int tipo, int cuit)
        {
            try
            {
                List<Solicitud> solicituds = new List<Solicitud>();
                solicituds = (List<Solicitud>)SolicitudManager.Current.GetAllSolicitudes(tipo, cuit);
                return solicituds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Solicitud> GetAllSolicitudesUser(string id_usuario)
        {
            try
            {
                List<Solicitud> solicituds = new List<Solicitud>();
                solicituds = (List<Solicitud>)SolicitudManager.Current.GetAllSolicitudesUser(id_usuario);
                return solicituds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
