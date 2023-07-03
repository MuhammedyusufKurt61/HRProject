using HRProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework.Configurations
{
    public class PersonelConfiguration : IEntityTypeConfiguration<Personel>
    {

        public void Configure(EntityTypeBuilder<Personel> builder)
        {
            builder
                .Property(x => x.TC)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("nvarchar(11)");
            
            builder.HasIndex(i => i.TC).IsUnique();

            builder
                .Property(x => x.FirstName)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder
                .Property(x => x.Surname)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder
                .Property(x => x.SecondName)
                .HasColumnType("nvarchar(20)")
                .IsRequired(false); 

            builder
                .Property(x => x.SecondSurname)
                .HasColumnType("nvarchar(20)")
                .IsRequired(false); 

            builder
                .Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(x => x.BirthPlace)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder
               .Property(x => x.BirthDate)
               .HasMaxLength(12)
               .IsRequired()
               .HasColumnType("datetime");           

            builder
               .Property(x => x.QuitDate)
               .HasMaxLength(12)
               .HasColumnType("datetime")
               .IsRequired(false);

            builder
               .Property(x => x.ReceiptDate)
               .HasMaxLength(12)
               .IsRequired()
               .HasColumnType("datetime");

            builder
                .Property(x => x.IsActive)
                .HasColumnType("bit");

            builder
               .Property(x => x.Job)
               .HasColumnType("nvarchar(30)")
               .IsRequired();

            builder
               .Property(x => x.Department)
               .HasMaxLength(40)
               .IsRequired();

            builder
               .Property(x => x.TotalAllowanceAmount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder
               .Property(x => x.Salary)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.HasOne(x=>x.MyCompany).WithMany(c=>c.Personels)/*.HasForeignKey(x=>x.CompanyId).OnDelete(DeleteBehavior.NoAction)*/;



        }
    }
}
