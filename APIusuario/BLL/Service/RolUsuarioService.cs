using DAL.Manager;
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

        public void Add(Usuario usuario)
        {
            try
            {
                RolUsuarioManager.Current.Join(usuario, usuario.rol);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateRolUsuario(Usuario usuario)
        {
            try
            {
                RolUsuarioManager.Current.delete(usuario, usuario.rol);
                RolUsuarioManager.Current.Join(usuario, usuario.rol);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
