using Academia.Inventario.API.Infrastructure.InventarioDB;
using System.Linq.Expressions;

namespace Academia.Inventario.API.Infrastructure.Repository
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly InventarioSeqpContext _dbContext;

        public EntityRepository(InventarioSeqpContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().Where(query).FirstOrDefault();
        }

        public List<TEntity> Where(Expression<Func<TEntity, bool>> query)
        {
            return _dbContext.Set<TEntity>().Where(query).ToList();
        }
    }
}
