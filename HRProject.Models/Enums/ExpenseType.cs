using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HRProject.Models.Enums
{
    public enum ExpenseType
    {
        [Display(Name = "Konaklama")]
        Accomodation,
        [Display(Name = "Seyahat")]
        Travel,
        [Display(Name = "Yeme-İçme")]
        Catering
    }
}
