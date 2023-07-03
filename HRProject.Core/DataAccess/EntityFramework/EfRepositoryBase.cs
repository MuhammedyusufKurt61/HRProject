using HRProject.Core.Entities;
using HRProject.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Core.DataAccess.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class,IEntity, new()
        where TContext : DbContext, new()
    {
        public GetOneResult<TEntity> Add(TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Added;
            try
            {
                context.SaveChanges();
                result.Data = entity;
                return result;
            }
            catch (Exception ex)
            {
                result.Data = default;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<GetOneResult<TEntity>> AddAsync(TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Added;
            try
            {
                await context.SaveChangesAsync();
                result.Data = entity;
                return result;
            }
            catch (Exception ex)
            {
                result.Data = default;
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public Result Delete(TEntity entity)
        {
            var result = new Result();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Deleted;
            try
            {
                context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<Result> DeleteAsync(TEntity entity)
        {
            var result = new Result();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Deleted;
            try
            {
                await context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public Result DeleteByFilter(Expression<Func<TEntity, bool>> filter)
        {
            var result = new Result();
            using TContext context = new TContext();
            ICollection<TEntity> entities = context.Set<TEntity>().Where(filter).ToList();
            try
            {
                context.RemoveRange(entities);
                context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
            
        }

        public async Task<Result> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = new Result();
            using TContext context = new TContext();
            ICollection<TEntity> entities = context.Set<TEntity>().Where(filter).ToList();
            try
            {
                context.RemoveRange(entities);
                await context.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public GetOneResult<TEntity> Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            result.Data = context.Set<TEntity>()
                        .Where(filter)
                        .MyIncludes(includes)
                        .FirstOrDefault();
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = "Gönderilen filtereye ait kayıt bulunamadı.";
                return result;
            }
            return result;
        }

        public GetManyResult<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = new GetManyResult<TEntity>();
            using TContext context = new TContext();
            result.Data = filter == null ? context.Set<TEntity>()
                                            .MyIncludes(includes).ToList() :
                                    context.Set<TEntity>()
                                             .Where(filter)
                                             .MyIncludes(includes)
                                             .ToList();
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = "Gönderilen filtereye ait kayıt bulunamadı.";
                return result;
            }
            return result;
        }

        public async Task<GetManyResult<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = new GetManyResult<TEntity>();
            using TContext context = new TContext();
            result.Data = filter == null ? await context.Set<TEntity>()
                                            .MyIncludes(includes).ToListAsync() :
                                    await context.Set<TEntity>()
                                             .Where(filter)
                                             .MyIncludes(includes)
                                             .ToListAsync();
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = "Gönderilen filtereye ait kayıt bulunamadı.";
                return result;
            }
            return result;
        }

        public async Task<GetOneResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            result.Data =await context.Set<TEntity>()
                        .Where(filter)
                        .MyIncludes(includes)
                        .FirstOrDefaultAsync();
            if (result.Data == null)
            {
                result.Success = false;
                result.Message = "Gönderilen filtereye ait kayıt bulunamadı.";
                return result;
            }
            return result;
        }
        
        public GetOneResult<TEntity> Update(TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Modified;
            try
            {
                context.SaveChanges();
                result.Data = entity;
                return result;
            }
            catch (Exception ex)
            {
                result.Data = default;
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }

        public async Task<GetOneResult<TEntity>> UpdateAsync(TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            using TContext context = new TContext();
            context.Entry(entity).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
                result.Data = entity;
                return result;
            }
            catch (Exception ex)
            {
                result.Data = default;
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }     
    }
}
