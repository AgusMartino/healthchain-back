using DAL.Interface;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public sealed class RolUsuarioManager : IGenericRelationship<Usuario, Rol>
    {
        #region singleton
        private readonly static RolUsuarioManager _instance = new RolUsuarioManager();

        public static RolUsuarioManager Current
        {
            get
            {
                return _instance;
            }
        }

        private RolUsuarioManager()
        {
            //Implent here the initialization of your singleton
        }
        #endregion
        public void Join(Usuario obj1, Rol obj2)
        {
            throw new NotImplementedException();
        }

        public void delete(Usuario obj1, Rol obj2)
        {
            throw new NotImplementedException();
        }

        public List<Rol> GetAll(Usuario obj)
        {
            throw new NotImplementedException();
        }
        
    }

}
