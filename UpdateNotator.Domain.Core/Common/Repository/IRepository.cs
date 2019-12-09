using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UpdateNotator.Domain.Core.Common.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(Guid id);
        Task<TEntity> FirstOrDefault(Specification<TEntity> specification);

        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetWhere(Specification<TEntity> specification);

        Task<int> CountAll();
        Task<int> CountWhere(Specification<TEntity> specification);
    }
}
