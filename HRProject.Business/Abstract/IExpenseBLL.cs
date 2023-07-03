using HRProject.Core.Utilities;
using HRProject.Models.ViewModels.AllowanceVM;
using HRProject.Models.ViewModels.ExpenseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Business.Abstract
{
    public interface IExpenseBLL
    {
        GetManyResultAsQueryable<ExpenseListItem> GetExpenses(Guid id);
        GetManyResultAsQueryable<ExpenseListItem> GetAllExpenses();
        GetOneResult<CreateExpenseVM> CreateExpense(CreateExpenseVM Expense);
        GetOneResult<ExpenseCancelVM> CancelExpense(ExpenseCancelVM Expense);
        GetOneResult<ExpenseApproveVm> ExpenseApprove(ExpenseApproveVm model);
        GetOneResult<ExpenseRejectVm> ExpenseReject(ExpenseRejectVm model);
    }
}
