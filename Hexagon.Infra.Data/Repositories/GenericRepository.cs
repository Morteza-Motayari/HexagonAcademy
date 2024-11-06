using Hexagon.Domain.Interfaces;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories
{
    public class GenericRepository<T> (HexagonContext db): IGenericRepository<T> where T : class
    {
        public void Delete(T entity)
        => db.Remove(entity);

        public async Task<List<T>> GetAllAsync()
        => await db.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
        => await db.Set<T>().FindAsync(id);

        public async Task InserAsync(T entity)
        => await db.AddAsync(entity);

        public async Task SaveChangeAsync()
        => await db.SaveChangesAsync();

        public void Update(T entity)
        {
            db.Update(entity);
        }
    }
}
