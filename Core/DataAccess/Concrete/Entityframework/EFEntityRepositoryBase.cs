using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.Concrete.Entityframework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var added = context.Entry(entity);
                added.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updated = context.Entry(entity);
                updated.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deleted = context.Entry(entity);
                deleted.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context
                    .Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(filter);
            }
        }

        public async Task<IList<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter)
        {
            using (var context = new TContext())
            {
                if (filter is null)
                {
                    return await context
                        .Set<TEntity>()
                        .AsNoTracking()
                        .ToListAsync();
                }
                return await context
                    .Set<TEntity>()
                    .AsNoTracking()
                    .Where(filter)
                    .ToListAsync();
            }
        }
    }
}
