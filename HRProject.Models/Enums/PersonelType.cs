using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.Enums
{
    public enum PersonelType
    {
        [Display(Name = "Yönetici")]
        Manager,
        [Display(Name = "Personel")]
        Personel
    }
}
