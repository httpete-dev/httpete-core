using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T?> GetById(int id, CancellationToken cancellationToken = default);
        Task<T> Add(T entity, CancellationToken cancellationToken = default);
        Task<T> Update(T entity, CancellationToken cancellationToken = default);
        Task<T> Delete(int id, CancellationToken cancellationToken = default);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
            => await _dbSet.ToArrayAsync(cancellationToken);

        public virtual async Task<T?> GetById(int id, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public virtual async Task<T> Add(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<T> Update(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<T> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(id, cancellationToken);
            if (entity == null)
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
