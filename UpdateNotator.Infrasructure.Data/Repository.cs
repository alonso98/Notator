using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqSpecs.Core;
using Microsoft.EntityFrameworkCore;
using UpdateNotator.Domain.Core.Common.Repository;

namespace UpdateNotator.Infrasructure.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected AppContext context;
        protected DbSet<TEntity> collection;

        public EFRepository(AppContext context)
        {
            this.context = context;
            this.collection = context.Set<TEntity>();
        }

        public Task<int> CountAll() => collection.CountAsync();

        public Task<int> CountWhere(Specification<TEntity> specification) => collection.CountAsync(specification.ToExpression());


        public Task<TEntity> FirstOrDefault(Specification<TEntity> specification) => collection.FirstOrDefaultAsync(specification.ToExpression());

        public async Task<IEnumerable<TEntity>> GetAll() => await collection.ToListAsync();

        public Task<TEntity> GetById(Guid id) => collection.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetWhere(Specification<TEntity> specification) => await collection.Where(specification.ToExpression()).ToListAsync();


        public Task Add(TEntity entity) => collection.AddAsync(entity);

        public Task Remove(TEntity entity) => Task.Run(() => { collection.Remove(entity); });

        public Task Update(TEntity entity) => Task.Run(() => { context.Entry(entity).State = EntityState.Modified; });
    }
}
