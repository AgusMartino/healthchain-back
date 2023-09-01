using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public sealed class RolUsuarioService
    {
        #region singleton
        private readonly static RolUsuarioService _instance = new RolUsuarioService();

        public static RolUsuarioService Current
        {
            get
            {
                return _instance;
            }
        }

        private RolUsuarioService()
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

        public void UpdateRolUsuario(Usuario usuario)
        {

        }
    }

}
