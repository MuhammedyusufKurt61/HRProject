using HRProject.Core.DataAccess.EntityFramework;
using HRProject.Core.Entities;
using HRProject.Core.Utilities;
using HRProject.Entities;

namespace HRProject.DataAccess.Abstract
{
    public interface IPersonelRepository:IRepository<Personel>
    {
        GetOneResult<Personel> SoftDelete(Personel entity);
        Task<GetOneResult<Personel>> SoftDeleteAsync(Personel entity);
    }
}
