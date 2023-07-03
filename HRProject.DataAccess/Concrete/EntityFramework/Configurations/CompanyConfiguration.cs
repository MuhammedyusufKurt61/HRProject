using HRProject.Entities;
using HRProject.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Id).IsRequired();            

            //builder.HasMany(x => x.Personels).WithOne(x => x.MyCompany).HasForeignKey(x => x.CompanyID).OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(x => x.Id);
            
        }
    }
}
