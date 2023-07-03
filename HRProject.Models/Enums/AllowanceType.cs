using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HRProject.Models.Enums
{
    public enum AllowanceType
    {
        [Display(Name ="Kurumsal")]
        InSource,
        [Display(Name = "Bireysel")]
        OutSource
    }
    
}
