using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.AllowanceVM
{
    public class CreateAllowanceVM : IViewModel
    {
        public Guid Id { get; set; }
        public AllowanceType Type { get; set; }
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [Range(0, int.MaxValue, ErrorMessage = "Avans miktari sıfırdan küçük olamaz")]
        public decimal? Amount { get; set; }
        public AllowanceState State => AllowanceState.Pending;
  
        [DataType(DataType.Date)]
        public DateTime DemandDate => DateTime.Now;
        [DataType(DataType.Currency)]
        public Currency CurrentUnit { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public Guid PersonelID { get; set; }
       
    }

}
