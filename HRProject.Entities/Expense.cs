using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRProject.Entities
{
    public class Expense : IEntity
    {
        public Guid Id { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Amount { get; set; }
        public ExpenseState State { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ReplyDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DemandDate { get; private set; }

        // [DataType(DataType.Currency)]
        public Currency CurrentUnit { get; set; }
        public string? Description { get; set; }
        public Guid PersonelId { get; set; }
        public string? FilePath { get; set; } = "defaultuser.jpg";
        public virtual Personel MyPersonel { get; set; }
        public Expense()
        {
            DemandDate = DateTime.Now;
        }
    }
}
