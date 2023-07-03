using AutoMapper;
using HRProject.Business.Abstract;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Abstract;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.Models.Enums;
using HRProject.Models.ViewModels.AllowanceVM;
using HRProject.Models.ViewModels.ExpenseVM;

namespace HRProject.Business.Concrete
{
    public class ExpenseBLL : IExpenseBLL
    {

        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IPersonelRepository _personelRepository;
        
       

        public ExpenseBLL(IMapper mapper, IExpenseRepository ExpenseRepository, IPersonelRepository personelRepository)
        {
            _mapper = mapper;
            _expenseRepository = ExpenseRepository;
            _personelRepository = personelRepository;             
        }

        public GetOneResult<ExpenseApproveVm> ExpenseApprove(ExpenseApproveVm model)
        {
            GetOneResult<ExpenseApproveVm> result = new GetOneResult<ExpenseApproveVm>();
            var data = _expenseRepository.Get(x => x.Id == model.Id);
            data.Data.State = ExpenseState.Approved;
            data.Data.ReplyDate = DateTime.Now;
            _expenseRepository.Update(data.Data);
            result.Data = model;
            return result;
        }

        public GetOneResult<ExpenseRejectVm> ExpenseReject(ExpenseRejectVm model)
        {
            GetOneResult<ExpenseRejectVm> result = new GetOneResult<ExpenseRejectVm>();
            var data = _expenseRepository.Get(x => x.Id == model.Id);
            data.Data.State = ExpenseState.Rejected;
            data.Data.ReplyDate = DateTime.Now;
            _expenseRepository.Update(data.Data);
            result.Data = model;
            return result;
        }

        public GetOneResult<CreateExpenseVM> CreateExpense(CreateExpenseVM model)
        {
            GetOneResult<CreateExpenseVM> result = new GetOneResult<CreateExpenseVM>();
            Expense expense = _mapper.Map<Expense>(model);
            var data = _expenseRepository.Add(expense);
            result.Success = data.Success;
            result.Data = _mapper.Map<Expense, CreateExpenseVM>(data.Data);
            return result;
        }

        public GetManyResultAsQueryable<ExpenseListItem> GetExpenses(Guid personelId)
        {
            GetManyResultAsQueryable<ExpenseListItem> result = new GetManyResultAsQueryable<ExpenseListItem>();
            var data = _expenseRepository.GetAll(x => x.PersonelId == personelId);
            List<ExpenseListItem> list = new List<ExpenseListItem>();
            foreach (var item in data.Data)
            {
                list.Add(_mapper.Map<ExpenseListItem>(item));
            }

            result.Data = list.AsQueryable();
            return result;
        }

        public GetManyResultAsQueryable<ExpenseListItem> GetAllExpenses()
        {
            GetManyResultAsQueryable<ExpenseListItem> result = new GetManyResultAsQueryable<ExpenseListItem>();
            var data = _expenseRepository.GetAll(null,x=>x.MyPersonel).Data;
            List<ExpenseListItem> list = new List<ExpenseListItem>();

            foreach (var item in data)
            {
                list.Add(_mapper.Map<ExpenseListItem>(item));
            }

            result.Data = list.AsQueryable();
            return result;
        }

        public GetOneResult<ExpenseCancelVM> CancelExpense(ExpenseCancelVM expenseCancel)
        {
            GetOneResult<ExpenseCancelVM> result = new GetOneResult<ExpenseCancelVM>();
            GetOneResult<Expense> expense = new GetOneResult<Expense>();
            expense.Data = _expenseRepository.Get(x => x.Id == expenseCancel.Id).Data;

            //result.Data = mapper.Map<Allowance>(allowance);
            var personel = _personelRepository.Get(y => y.Id == expenseCancel.PersonelId).Data;
          
            expense.Data.State = ExpenseState.Cancelled;
            expense.Data.ReplyDate = DateTime.Now;
            var expenseData = _expenseRepository.Update(expense.Data);
            var personelData = _personelRepository.Update(personel);
            result.Data = new ExpenseCancelVM();
            result.Data.Id = expense.Data.Id;
            result.Data.PersonelId = expenseCancel.PersonelId;
            return result;
        }
    }
}
