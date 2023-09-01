using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public sealed class RolManager
    {
        #region singleton
        private readonly static RolManager _instance = new RolManager();

        public static RolManager Current
        {
            get
            {
                return _instance;
            }
        }

        private RolManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion



    }

}
