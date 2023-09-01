using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public sealed class RolUsuarioManager
    {
        #region singleton
        private readonly static RolUsuarioManager _instance = new RolUsuarioManager();

        public static RolUsuarioManager Current
        {
            get
            {
                return _instance;
            }
        }

        private RolUsuarioManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public void Add(RolUsuario rolUsuario)
        {

        }

        public RolUsuario GetRolUsuario(Guid user)
        {
            return null;
        }
    }

}
