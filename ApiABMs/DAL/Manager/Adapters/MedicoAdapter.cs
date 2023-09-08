using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapters
{

    public sealed class MedicoAdapter
    {
        #region singleton
        private readonly static MedicoAdapter _instance = new MedicoAdapter();

        public static MedicoAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private MedicoAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Medico adapt(object[] values)
        {
            Medico medico = new Medico
            {
                nombre = values[(int)Columns.nombre].ToString(),
                apellido = values[(int)Columns.apellido].ToString(),
                usuario = values[(int)Columns.usuario].ToString(),
                especialidad = values[(int)Columns.especialidad].ToString()
            };
            return medico;
        }

        private enum Columns
        {
            nombre,
            apellido,
            usuario,
            especialidad
        }
    }

}
