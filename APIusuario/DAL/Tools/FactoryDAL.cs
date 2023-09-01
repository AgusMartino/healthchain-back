using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tools
{

    public sealed class FactoryDAL : IFactory
    {
        /*
        #region singleton
        private readonly static FactoryDAL _instance = new FactoryDAL();

        public static FactoryDAL Current
        {
            get
            {
                return _instance;
            }
        }

        private FactoryDAL()
        {
            //Implent here the initialization of your singleton
        }
        #endregion
        */
        
        private readonly IConfiguration configuration;

        public FactoryDAL()
        {
            
            var conn2 = 0;
        }

        public void Prueba()
        {
            var bbddSeguridad = configuration.GetSection("ConnectionStrings.connectionString").Value;
        }
    }

}
