using HRProject.Core.Entities;
using HRProject.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Core.DataAccess.EntityFramework
{
    public interface IRepository<TEntity> where TEntity : class,IEntity, new()
    {
        GetOneResult<TEntity> Add(TEntity entity);
        Task<GetOneResult<TEntity>> AddAsync(TEntity entity);
        GetOneResult<TEntity> Update(TEntity entity);
        Task<GetOneResult<TEntity>> UpdateAsync(TEntity entity);
        Result Delete(TEntity entity);
        Task<Result> DeleteAsync(TEntity entity);
        Result DeleteByFilter(Expression<Func<TEntity, bool>> filter);
        Task<Result> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter);        
        GetOneResult<TEntity> Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        Task<GetOneResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        GetManyResult<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
        Task<GetManyResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes);
    }
}
