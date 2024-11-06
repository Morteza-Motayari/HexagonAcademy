using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task InserAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task SaveChangeAsync();
        void Update(T entity);
        void Delete(T entity);
    }
    public class T
    {
        public bool IsDeleted { get; set; }
    }
}
