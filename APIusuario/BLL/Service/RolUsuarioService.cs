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
                if(usuario.rol.Id == "")
                {
                    usuario.rol = null;
                }
                RolUsuarioManager.Current.Join(usuario, usuario.rol);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public Rol GetRol(string id_usuario)
        {
            Rol rol = new Rol();
            try
            {
                rol = RolUsuarioManager.Current.GetRol(id_usuario);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return rol;
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
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }
    }

}
