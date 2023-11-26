using Domain.DOMAIN;
using DAL.Manager;
using DAL.Tools.Service;

namespace BLL.Service
{
    public sealed class UserService
    {
        #region singleton
        private readonly static UserService _instance = new UserService();

        public static UserService Current
        {
            get
            {
                return _instance;
            }
        }

        private UserService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public void RegisterUser(Usuario usuario)
        {
            try
            {
                usuario.id = Guid.NewGuid().ToString();
                usuario.fecha_creacion = DateTime.Now;
                usuario.fecha_modificacion = DateTime.Now;
                if (usuario.password != null)
                {
                    usuario.password = EncrypService.EncryptPassword(usuario.password);
                }
                UserManager.Current.Add(usuario);
                RolUsuarioService.Current.Add(usuario);
                if(usuario.user_type == "2")
                {
                    BilleteraService.Current.AddBilletera(usuario.id);
                }
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }


        public Usuario ValidateUser(string user)
        {
            Usuario getUser = new Usuario();
            try
            {
                getUser = UserManager.Current.GetUserByUser(user);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return getUser;
        }

        public Usuario LoginUserBO(UsuarioLoginBO usuario)
        {
            Usuario usuarioValidate = new Usuario();
            try
            {
                usuario.password = EncrypService.EncryptPassword(usuario.password);
                usuarioValidate = UserManager.Current.LoginUserBo(usuario.usuario, usuario.password);
                
                
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuarioValidate;
        }

        public IEnumerable<Usuario> GetAll(string cuit_empresa)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                usuarios = (List<Usuario>)UserManager.Current.GetAll(cuit_empresa);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return usuarios;
        }

        public void UpdateUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.password != null || usuario.password != "")
                {
                    usuario.password = EncrypService.EncryptPassword(usuario.password);
                }
                usuario.fecha_modificacion = DateTime.Now;
                UserManager.Current.Update(usuario);
                RolUsuarioService.Current.UpdateRolUsuario(usuario);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
        }

        public Usuario GetUser(string user)
        {
            Usuario getUser = new Usuario();
            try
            {
                getUser = UserManager.Current.GetUser(user);
            }
            catch (Exception ex)
            {
                BitacoraService.Current.AddBitacora("ERROR", ex.Message.ToString(), "084757d9-cbf3-4098-9374-b9e6563dcfb3");
            }
            return getUser;
        }
    }
}
