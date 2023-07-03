using HRProject.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelForgetPasswordViewModel : IViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
