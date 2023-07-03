using HRProject.Core.Utilities;
using HRProject.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Business.Abstract
{
    public interface IAllowanceBLL
    {
        GetManyResultAsQueryable<AllowanceListItem> GetAllowances(Guid id);
        GetManyResultAsQueryable<AllowanceListItem> GetAllAllowances();
        GetOneResult<CreateAllowanceVM> CreateAllowance(CreateAllowanceVM allowance);
        GetOneResult<AllowanceCancelVM> CancelAllowance(AllowanceCancelVM allowance);
        GetOneResult<AllowanceApproveVm> AllowanceApprove(AllowanceApproveVm model);
        GetOneResult<AllowanceRejectVm> AllowanceReject(AllowanceRejectVm model);

    }
}
