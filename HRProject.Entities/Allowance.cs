using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Entities
{
    public class Allowance:IEntity
    {
        public Guid Id { get; set; }
        public AllowanceType Type { get; set; }
       
        public decimal Amount { get; set; }
        public AllowanceState State { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime? ReplyDate { get; set; }
       
        [DataType(DataType.Date)]
        public DateTime DemandDate { get; private set; }

        // [DataType(DataType.Currency)]
        public Currency CurrentUnit { get; set; }
        public string? Description { get; set; }
        public Guid PersonelId { get; set; }
        public virtual Personel MyPersonel { get; set; }

        public Allowance()
        {
            DemandDate= DateTime.Now;
        }
    }
}
