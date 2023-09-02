using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapter
{
    public sealed class Tipo_SolicitudaAdapter
    {
        #region singleton
        private readonly static Tipo_SolicitudaAdapter _instance = new Tipo_SolicitudaAdapter();

        public static Tipo_SolicitudaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private Tipo_SolicitudaAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public tipo_solicitud adapt(object[] values)
        {
            tipo_solicitud tipo_Solicitud = new tipo_solicitud
            {
                id = values[(int)Columns.id_tipo_solicitud].ToString(),
                tipo = values[(int)Columns.tipo_solicitud].ToString()
            };
            return tipo_Solicitud;
        }

        private enum Columns
        {
            id_tipo_solicitud,
            tipo_solicitud
        }
    }

}
