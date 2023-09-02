using DAL.Manager;
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
            try
            {
                tipo_solicitud solicitud = new tipo_solicitud();
                string[] criterios = { "id" };
                string[] valores = { id.ToString() };
                solicitud = Tipo_solicitudManager.Current.GetOne(criterios, valores);
                return solicitud;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<tipo_solicitud> GetAll()
        {
            try
            {
                List<tipo_solicitud> solicituds = new List<tipo_solicitud>();
                solicituds = (List<tipo_solicitud>)Tipo_solicitudManager.Current.GetAll();
                return solicituds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
