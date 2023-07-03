using HRProject.Core.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelUpdateViewModel : IViewModel
    {
        public Guid Id { get; set; }
        public string TC { get; set; }
        [Required(ErrorMessage = "Adresi alanının doldurulması zorunludur.")]
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public string UserName { get; set; }
        [ValidateNever]
        public string? PhotoPath { get; set; }
        [ValidateNever]
        public IFormFile? Photo { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        [Required(ErrorMessage = "Telefon numarası alanının doldurulması zorunludur.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
     
    }
}
