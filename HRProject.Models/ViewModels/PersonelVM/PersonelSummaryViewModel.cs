using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRProject.Core.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelSummaryViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
