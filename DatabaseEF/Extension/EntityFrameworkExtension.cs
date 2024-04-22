using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EntityFramework.Extension
{
    public static class EntityFrameworkExtension
    {
        public static async Task<int> DeleteAsync<TEntity>(
            this DbSet<TEntity> dbSet,
            Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var entitiesToDelete = await dbSet
                .Where(predicate)
                .ToListAsync();

            dbSet.RemoveRange(entitiesToDelete);

            return entitiesToDelete.Count;
        }
    }

}
