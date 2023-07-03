using HRProject.Core.Entities;
using HRProject.Models.Enums;

namespace HRProject.Models.ViewModels.AllowanceVM
{
    public class AllowanceListItem : IViewModel
    {
        public Guid Id { get; set; }
        public AllowanceType Type { get; set; }
        public decimal Amount { get; set; }
        public Currency CurrentUnit { get; set; }
        public AllowanceState State { get; set; }
        public DateTime DemandDate { get; set; }
        public DateTime ReplyDate { get; set; }
        public string? Description { get; set; }
        public string Username { get; set; }
    }

}
