using HRProject.Core.DataAccess.EntityFramework;
using HRProject.Core.Entities;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Abstract;
using HRProject.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework
{
    public class EfPersonelRepository : EfRepositoryBase<Personel, HRProjectContext>, IPersonelRepository
    {
        public GetOneResult<Personel> SoftDelete(Personel entity)
        {
            Personel _entity = entity;

            if (entity.GetType().GetProperty("IsActive") != null)
            {
                _entity.GetType().GetProperty("IsActive").SetValue(_entity, false);               
            }          
            return Update(_entity);
        }

        public async Task<GetOneResult<Personel>> SoftDeleteAsync(Personel entity)
        {
            Personel _entity = entity;

            if (entity.GetType().GetProperty("IsActive") != null)
            {
                _entity.GetType().GetProperty("IsActive").SetValue(_entity, false);
            }
            return await UpdateAsync(_entity);
        }
    }
}
