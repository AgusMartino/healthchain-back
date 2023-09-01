using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BLL.Service
{
    public sealed class EncrypService
    {
        #region singleton
        private readonly static EncrypService _instance = new EncrypService();

        public static EncrypService Current
        {
            get
            {
                return _instance;
            }
        }

        private EncrypService()
        {
            //Implent here the initialization of your singleton
        }
        #endregion

        public static string EncryptPassword(string Contraseña)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(Contraseña));
                return Convert.ToBase64String(data);
            }
        }
    }

}
