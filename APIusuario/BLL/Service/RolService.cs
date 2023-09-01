using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public sealed class RolService
    {
        #region singleton
        private readonly static RolService _instance = new RolService();

        public static RolService Current
        {
            get
            {
                return _instance;
            }
        }

        private RolService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public List<Rol> GetRols()
        {
            List<Rol> rols = new List<Rol>();
            return rols;

        }

        public Rol GetRol(string name)
        {
            Rol rol = new Rol();
            return rol;
        }

    }

}
