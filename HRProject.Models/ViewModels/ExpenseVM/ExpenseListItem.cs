using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.ExpenseVM
{
    public class ExpenseListItem : IViewModel
    {
        public Guid Id { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public Currency CurrentUnit { get; set; }
        public ExpenseState State { get; set; }
        public DateTime DemandDate { get; set; }
        public DateTime ReplyDate { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public string Username { get; set; }
    }
}
