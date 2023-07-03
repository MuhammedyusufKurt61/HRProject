using HRProject.DataAccess.Concrete.EntityFramework.Configurations;
using HRProject.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework
{
    public class HRProjectContext:IdentityDbContext<Personel,UserRole,Guid>
    {     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:yusufazursqldb.database.windows.net,1433;Initial Catalog=HRProject;Persist Security Info=False;User ID=yusuf;Password=***.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");
           // optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DenemeDb;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PersonelConfiguration());
            builder.ApplyConfiguration(new AllowanceConfiguration());
        }

        //public override int SaveChanges()
        //{
        //    OnBeforeSave();
        //    return base.SaveChanges();
        //}
        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    OnBeforeSave();
        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}
        //public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        //{
        //    OnBeforeSave();
        //    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //}
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    OnBeforeSave();
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        //private void OnBeforeSave()
        //{
        //    var addedEntities = ChangeTracker.Entries().Where(i => i.State == EntityState.Added && i.Entity.GetType() == typeof(Personel)).Select(i => (Personel)i.Entity);
        //    PrepareAddedEntities(addedEntities);
        //}

        //private void PrepareAddedEntities(IEnumerable<Personel> addedEntities)
        //{
        //    foreach (var entity in addedEntities)
        //    {
        //        if (entity.TotalAllowanceAmount == -1)
        //            entity.TotalAllowanceAmount = entity.Salary * 3;
        //    }
        //}
    }
}
