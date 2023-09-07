using Domain.DOMAIN;
using DAL.Manager;

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
                usuario.Id = Guid.NewGuid().ToString();
                usuario.fecha_creacion = DateTime.Now;
                usuario.fecha_modificacion = DateTime.Now;
                if (usuario.password != null)
                {
                    usuario.password = EncrypService.EncryptPassword(usuario.password);
                }
                UserManager.Current.Add(usuario);
                RolUsuarioService.Current.Add(usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public Usuario ValidateUser(string user)
        {
            try
            {
                Usuario getUser = UserManager.Current.GetUserByUser(user);
                return getUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool LoginUserBO(Usuario usuario)
        {
            try
            {
                usuario.password = EncrypService.EncryptPassword(usuario.password);
                Usuario usuarioValidate = UserManager.Current.LoginUserBo(usuario.user, usuario.password);
                if(usuarioValidate != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }

        public IEnumerable<Usuario> GetAll(int cuit_empresa)
        {
            try
            {
                List<Usuario> usuarios = new List<Usuario>();
                usuarios = (List<Usuario>)UserManager.Current.GetAll(cuit_empresa);
                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateUsuario(Usuario usuario)
        {
            try
            {
                if (usuario.password != null)
                {
                    usuario.password = EncrypService.EncryptPassword(usuario.password);
                }
                usuario.fecha_modificacion = DateTime.Now;
                UserManager.Current.Update(usuario);
                RolUsuarioService.Current.UpdateRolUsuario(usuario);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Usuario GetUser(string user)
        {
            try
            {
                Usuario getUser = UserManager.Current.GetUser(user);
                return getUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
