using HRProject.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelChangePasswordViewModel : IViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage = "Oluşturduğunuz şifreler uyuşmuyor.")]
        public string NewPasswordCheck { get; set; }
    }
}
