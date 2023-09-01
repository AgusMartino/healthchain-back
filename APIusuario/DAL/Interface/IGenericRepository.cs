using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IGenericRepository<T>
    {
        T GetOne(string[] criterios, string[] valores);
        void delete(T entity);
        void Add(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);

    }
}
