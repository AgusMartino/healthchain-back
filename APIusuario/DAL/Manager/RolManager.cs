using DAL.Interface;
using Domain.DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public sealed class RolManager : IGenericRepository<Rol>
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

        public Rol GetOne(string[] criterios, string[] valores)
        {
            throw new NotImplementedException();
        }

        public void delete(Rol entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Rol entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rol> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Rol entity)
        {
            throw new NotImplementedException();
        }
        
    }

}
