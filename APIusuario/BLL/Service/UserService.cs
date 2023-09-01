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
                usuario.Id = Guid.NewGuid();
                if (usuario.password != null)
                {
                    usuario.password = EncrypService.EncryptPassword(usuario.password);
                }
                UserManager.Current.RegisterUser(usuario);
                RolUsuario rolUsuario = new RolUsuario
                {
                    id_rol = usuario.rol,
                    id_usuario = usuario.Id
                };
                RolUsuarioService.Current.Add(rolUsuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool ValidateUser(string user)
        {
            try
            {
                Usuario getUser = UserManager.Current.GetUserByUser(user);
                if(getUser != null)
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

        public bool LoginUserBO(Usuario usuario)
        {
            try
            {
                usuario.password = EncrypService.EncryptPassword(usuario.password);
                return UserManager.Current.LoginUserBo(usuario.user, usuario.password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

             
        }
    }
}
