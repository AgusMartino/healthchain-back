using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IGenericRelationship<T, U>
    {
        void Join(T obj1, U obj2);
        void delete(T obj1, U obj2);
        List<U> GetAll(T obj);
    }
}
