using ABCBrasil.Hackathon.Api.Domain.Interfaces;
using ABCBrasil.Hackathon.Api.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ABCBrasil.Hackathon.Api.Infra.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected HackathonContext Db { get; }

        protected DbSet<T> DbSet { get; }

        protected RepositoryBase(HackathonContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                return;

            DbSet.Remove(entity);
            await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
        }
    }
}
