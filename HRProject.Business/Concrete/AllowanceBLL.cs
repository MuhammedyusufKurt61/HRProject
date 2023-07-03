using AutoMapper;
using HRProject.Business.Abstract;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Abstract;
using HRProject.Entities;
using HRProject.Models.Enums;
using HRProject.Models.ViewModels.AllowanceVM;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Business.Concrete
{
    public class AllowanceBLL : IAllowanceBLL
    {
        private readonly IMapper mapper;
        private readonly IAllowanceRepository allowanceRepository;
        private readonly IPersonelRepository personelRepository;

        public AllowanceBLL(IMapper mapper, IAllowanceRepository allowanceRepository,IPersonelRepository personelRepository)
        {
            this.mapper = mapper;
            this.allowanceRepository = allowanceRepository;
            this.personelRepository =personelRepository;
        }

        public GetOneResult<AllowanceCancelVM> CancelAllowance(AllowanceCancelVM allowanceCancel)
        {
            GetOneResult<AllowanceCancelVM> result = new GetOneResult<AllowanceCancelVM>();
            GetOneResult<Allowance> allowance = new GetOneResult<Allowance>();
             allowance.Data = allowanceRepository.Get(x => x.Id == allowanceCancel.Id).Data;
            
            //result.Data = mapper.Map<Allowance>(allowance);
            var personel = personelRepository.Get(y => y.Id == allowanceCancel.PersonelId).Data;
            if (allowance.Data.Type == AllowanceType.OutSource)
            {
                personel.TotalAllowanceAmount += allowance.Data.Amount;

            }
            allowance.Data.State = AllowanceState.Cancelled;
            allowance.Data.ReplyDate = DateTime.Now;
            var allowanceData = allowanceRepository.Update(allowance.Data);     
            var personelData = personelRepository.Update(personel);
            result.Data = new AllowanceCancelVM();
            result.Data.Id = allowance.Data.Id;
            result.Data.PersonelId=allowanceCancel.PersonelId;            
            return result;
        }

        public GetOneResult<CreateAllowanceVM> CreateAllowance(CreateAllowanceVM model)
        {
            GetOneResult<CreateAllowanceVM> result = new GetOneResult<CreateAllowanceVM>();
            Allowance allowance = mapper.Map<Allowance>(model);
            var personel = personelRepository.Get(x => x.Id == model.PersonelID).Data;
            if (allowance.Type == AllowanceType.OutSource)
            {
                if (personel.TotalAllowanceAmount >= allowance.Amount)
                {
                    personel.TotalAllowanceAmount-=allowance.Amount;
                }
                else
                {
                    result.Success = false;
                    return result;
                }
            }  
            personelRepository.Update(personel);
            var data = allowanceRepository.Add(allowance);
            result.Success = data.Success;
            result.Data = mapper.Map<Allowance,CreateAllowanceVM>(data.Data);
            return result;

        }

        public GetManyResultAsQueryable<AllowanceListItem> GetAllowances(Guid id)
        {
            GetManyResultAsQueryable<AllowanceListItem> result = new GetManyResultAsQueryable<AllowanceListItem>();
            var data = allowanceRepository.GetAll(x=>x.PersonelId==id);
            List<AllowanceListItem> list = new List<AllowanceListItem>();
            foreach (var item in data.Data)
            {
                list.Add(mapper.Map<AllowanceListItem>(item));
            }

            result.Data=list.AsQueryable();
            return result;  
        }

        public GetOneResult<AllowanceApproveVm> AllowanceApprove(AllowanceApproveVm model)
        {
            GetOneResult<AllowanceApproveVm> result = new GetOneResult<AllowanceApproveVm>();
            var data = allowanceRepository.Get(x => x.Id == model.Id);
            data.Data.State = AllowanceState.Approved;
            data.Data.ReplyDate = DateTime.Now;
            allowanceRepository.Update(data.Data);
            result.Data = model;
            return result;
        }

        public GetOneResult<AllowanceRejectVm> AllowanceReject(AllowanceRejectVm model)
        {
            GetOneResult<AllowanceRejectVm> result = new GetOneResult<AllowanceRejectVm>();
            var data = allowanceRepository.Get(x => x.Id == model.Id);
            data.Data.State = AllowanceState.Rejected;
            data.Data.ReplyDate = DateTime.Now;
            allowanceRepository.Update(data.Data);
            result.Data = model;
            return result;
        }

        public GetManyResultAsQueryable<AllowanceListItem> GetAllAllowances()
        {
            GetManyResultAsQueryable<AllowanceListItem> result = new GetManyResultAsQueryable<AllowanceListItem>();
            var data = allowanceRepository.GetAll(null,x=>x.MyPersonel);
            List<AllowanceListItem> list = new List<AllowanceListItem>();
            foreach (var item in data.Data)
            {
                list.Add(mapper.Map<AllowanceListItem>(item));
            }

            result.Data = list.AsQueryable();
            return result;
        }
    }
}
