using Domain.DOMAIN;

namespace DAL.Manager
{
    public sealed class UserManager
    {
        #region singleton
        private readonly static UserManager _instance = new UserManager();

        public static UserManager Current
        {
            get
            {
                return _instance;
            }
        }

        private UserManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public void RegisterUser(Usuario usuario)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Usuario GetUserByUser(string user)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool LoginUserBo(string user, string password)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }

}