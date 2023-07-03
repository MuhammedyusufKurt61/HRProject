using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.PersonelVM
{
    public class PersonelCreateVm
    {
        public string TC { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool PhoneNumberConfirmed { get; set; } = true;
        public bool TwoFactorEnabled { get; set; } = true;
        public bool LockoutEnabled { get; set; } = false;
        public int AccessFailedCount { get; set; } = 5;
        public bool IsPasswordResetted { get; set; } = true;
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string? SecondName { get; set; }
        public string? SecondSurname { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime ReceiptDate { get; set; }= DateTime.Now;
        public DateTime? QuitDate { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; } = 10000;
        public string PhoneNumber { get; set; }

    }
}
