using HRProject.Entities;
using HRProject.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired().HasConversion(new EnumToStringConverter<ExpenseType>());
            builder.Property(x => x.CurrentUnit).IsRequired().HasConversion(new EnumToStringConverter<Currency>());
            builder.Property(x => x.State).IsRequired().HasConversion(new EnumToStringConverter<ExpenseType>());
            builder.Property(x => x.Amount).HasColumnType("decimal").HasPrecision(18,2).IsRequired();
            builder.Property(x => x.ReplyDate).HasColumnType("date");
            builder.Property(x => x.DemandDate).HasColumnType("date").IsRequired();
            builder.Property(x => x.Description).HasColumnType("NVARCHAR").HasMaxLength(500);
            builder.HasOne(x => x.MyPersonel).WithMany(x => x.Expenses).HasForeignKey(x => x.PersonelId);
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

        }
    }
}
