using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager.Adapter
{

    public sealed class RolAdapter
    {
        #region singleton
        private readonly static RolAdapter _instance = new RolAdapter();

        public static RolAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private RolAdapter()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public Rol adapt(object[] values)
        {
            Rol rolAdapt = new Rol
            {
                Id = int.Parse(values[(int)Columns.Id_Rol].ToString()),
                rol = values[(int)Columns.Rol].ToString()
            };
            return rolAdapt;
        }

        private enum Columns
        {
            Id_Rol,
            Rol
        }
    }

}
