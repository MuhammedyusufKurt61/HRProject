using HRProject.Core.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelDetailViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string TC { get; set; }
        public string FullName { get; set; }
        public string? PhotoPath { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? QuitDate { get; set; }
        public bool IsActive { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }

    }
}
