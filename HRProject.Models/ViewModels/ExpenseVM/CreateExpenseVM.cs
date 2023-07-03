using HRProject.Core.Entities;
using HRProject.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.ExpenseVM
{
    public class CreateExpenseVM : IViewModel
    {
        public Guid Id { get; set; }

        public ExpenseType Type { get; set; }

        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [Range(0, int.MaxValue, ErrorMessage = "Harcama miktari sıfırdan küçük olamaz")]
        public decimal? Amount { get; set; }

        public ExpenseState State => ExpenseState.Pending;


        [DataType(DataType.Date)]
        public DateTime DemandDate => DateTime.Now;


        [DataType(DataType.Currency)]
        public Currency CurrentUnit { get; set; }


        [MaxLength(500)]
        public string? Description { get; set; }

        public Guid PersonelID { get; set; }
        [ValidateNever]
        public IFormFile? MyFile { get; set; }
        public string? FilePath { get; set; }
    }
}
