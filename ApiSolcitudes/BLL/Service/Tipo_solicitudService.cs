using DAL.Manager;
using DAL.Tools.Service;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public sealed class Tipo_solicitudService
    {
        #region singleton
        private readonly static Tipo_solicitudService _instance = new Tipo_solicitudService();

        public static Tipo_solicitudService Current
        {
            get
            {
                return _instance;
            }
        }

        private Tipo_solicitudService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public tipo_solicitud GetOne(int id)
        {
            tipo_solicitud solicitud = new tipo_solicitud();
            try
            {
                string[] criterios = { "id" };
                string[] valores = { id.ToString() };
                solicitud = Tipo_solicitudManager.Current.GetOne(criterios, valores); 
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicitud;
        }

        public IEnumerable<tipo_solicitud> GetAll()
        {
            List<tipo_solicitud> solicituds = new List<tipo_solicitud>();
            try
            {
                solicituds = (List<tipo_solicitud>)Tipo_solicitudManager.Current.GetAll();
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return solicituds;
        }
    }

}
