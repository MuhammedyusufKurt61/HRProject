using HRProject.Core.Utilities;
using HRProject.Entities;
using HRProject.Models.ViewModels.PersonelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Business.Abstract
{
    public interface IManagerBLL
    {
        Task<GetOneResult<PersonelCreateVm>> CreateNewPersonel(PersonelCreateVm personel, params string[] role);
        GetOneResult<Expense> ApproveExpense(Expense model);
        GetOneResult<Expense> RejectExpense(Expense model);
        GetManyResultAsQueryable<PersonelSummaryViewModel> GetPersonelist(Personel model);
    }
}
