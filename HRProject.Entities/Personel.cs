using HRProject.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Entities
{
    public class Personel : IdentityUser<Guid>, IEntity, ISoftDeleteable
    {
        public string TC { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string? SecondName { get; set; }
        public string? SecondSurname { get; set; }
        public string? PhotoPath { get; set; } = "defaultuser.jpg";
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public bool IsActive { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public bool IsPasswordResetted { get; set; } = false;
        public decimal Salary { get; set; }
        public decimal TotalAllowanceAmount { get; set; } = -1;
        public Guid? CompanyId  { get; set; }
        public virtual Company? MyCompany { get; set; }
        public virtual ICollection<Allowance>? Allowances { get; set; }
        public virtual ICollection<Expense>? Expenses { get; set; }
        public Personel()
        {
            Allowances= new List<Allowance>();
            Expenses = new List<Expense>();
        }
    }
}
