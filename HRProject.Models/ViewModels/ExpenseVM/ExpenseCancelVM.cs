using HRProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.ExpenseVM
{
    public class ExpenseCancelVM : IViewModel
    {
        public Guid Id { get; set; }
        public Guid PersonelId { get; set; }
    }
}
