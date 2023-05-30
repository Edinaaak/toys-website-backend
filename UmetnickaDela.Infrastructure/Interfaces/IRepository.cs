using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmetnickaDela.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Add (T entity);
        Task<bool> Update (T entity, int id);
        Task<bool> Delete (T entity);
        Task<List<T>> GetAll ();
        Task<T> getById (int id);
    }
}
