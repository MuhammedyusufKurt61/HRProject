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
    public interface IPersonelBLL
    {
        GetOneResult<PersonelSummaryViewModel> GetPersonelSummaryInfos(Guid id);
        GetOneResult<PersonelDetailViewModel> GetPersonelDetailInfos(Guid id);
        GetOneResult<PersonelUpdateViewModel> GetUpdatePersonel(Guid id);
        GetOneResult<PersonelUpdateViewModel> UpdatePersonel(PersonelUpdateViewModel model);
        Task<GetOneResult<PersonelUpdateViewModel>> ChangePasswordAsync(string email, string password); 
        Task<GetOneResult<PersonelUpdateViewModel>> ForgetPasswordAsync(string email, string password);
        Task<bool> CheckEmailAndPassword(string email, string password);
        


    }
}
