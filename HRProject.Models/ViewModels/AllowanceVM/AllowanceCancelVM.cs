using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Models.ViewModels.AllowanceVM
{
    public class AllowanceCancelVM : IViewModel
    {
        public Guid Id { get; set; }
        public Guid PersonelId { get; set; }
    }
}
