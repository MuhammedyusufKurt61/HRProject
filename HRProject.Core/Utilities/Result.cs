using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Core.Utilities
{
    public class Result
    {
        public Result()
        {
            Success = true;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Data { get; set; }
    }
    public class GetManyResult<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> Data { get; set; }
    }
    public class GetManyResultAsQueryable<TEntity>:Result where TEntity : class, new()
    {
        public IQueryable<TEntity> Data { get; set; }
    }
}
