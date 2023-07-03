using AutoMapper;
using HRProject.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using HRProject.Models.ViewModels.ExpenseVM;
using HRProject.Models.ViewModels.PersonelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Business.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Personel, PersonelDetailViewModel>().ForMember(x => x.FullName,opt=>opt.MapFrom(s=>string.Concat(s.FirstName," ",!string.IsNullOrEmpty(s.SecondName) ? s.SecondName +" "  :"",s.Surname, !string.IsNullOrEmpty(s.SecondSurname) ? " "+s.SecondSurname:"")));
            CreateMap<Personel, PersonelUpdateViewModel>().ForMember(x => x.FullName, opt => opt.MapFrom(s => string.Concat(s.FirstName, " ", !string.IsNullOrEmpty(s.SecondName) ? s.SecondName + " " : "", s.Surname, !string.IsNullOrEmpty(s.SecondSurname) ? " " + s.SecondSurname : "")));

            CreateMap<Personel, PersonelSummaryViewModel>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(s => string.Concat(s.FirstName, " ", (!string.IsNullOrEmpty(s.SecondName) ? s.SecondName + " " : ""), s.Surname, (!string.IsNullOrEmpty(s.SecondSurname) ? " " + s.SecondSurname : ""))));

            CreateMap<PersonelCreateVm, Personel>()
                .ForMember(x => x.Email, opt => opt
                .MapFrom(s => string.Concat
                (s.FirstName.ToLower(), ".", 
                (!string.IsNullOrEmpty(s.SecondName) ? s.SecondName.ToLower() + "." : ""), 
                s.Surname.ToLower(), 
                (!string.IsNullOrEmpty(s.SecondSurname) ? "." + s.SecondSurname.ToLower() : "")+"@bilgeadamboost.com")))

                .ForMember(x=>x.UserName, opt=>opt.MapFrom(s =>
                
                string.Concat
                (s.FirstName.ToLower(), "",
                (!string.IsNullOrEmpty(s.SecondName) ? s.SecondName.ToLower() + "" : ""),
                s.Surname.ToLower(),
                (!string.IsNullOrEmpty(s.SecondSurname) ? "" + s.SecondSurname.ToLower() : ""))))
                
                .ForMember(x=>x.TotalAllowanceAmount , y=>y.MapFrom(z=>z.Salary * 3));


            //.ForMember(x => x.Email,opt=>opt.MapFrom(s=>s.Email))
            //.ForMember(x => x.Address, opt => opt.MapFrom(s => s.Address))
            //.ForMember(x => x.PhotoPath, opt => opt.MapFrom(s => s.PhotoPath))
            //.ForMember(x => x.Job, opt => opt.MapFrom(s => s.Job))
            //.ForMember(x => x.Department, opt => opt.MapFrom(s => s.Department))
            //.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber));
            CreateMap<Allowance, AllowanceListItem>()
                .ForMember(x => x.Username, y => y.MapFrom(s => string.Concat(s.MyPersonel.FirstName, " ", !string.IsNullOrEmpty(s.MyPersonel.SecondName) ? s.MyPersonel.SecondName + " " : "", s.MyPersonel.Surname, !string.IsNullOrEmpty(s.MyPersonel.SecondSurname) ? " " + s.MyPersonel.SecondSurname : "")));
            CreateMap<Allowance, CreateAllowanceVM>().ReverseMap();
            CreateMap<Allowance, AllowanceCancelVM>().ReverseMap();
            CreateMap<Expense, CreateExpenseVM>().ReverseMap();
            CreateMap<Expense, ExpenseListItem>()
                .ForMember(x=>x.Username, y=>y.MapFrom(s=> string.Concat(s.MyPersonel.FirstName, " ", !string.IsNullOrEmpty(s.MyPersonel.SecondName) ? s.MyPersonel.SecondName + " " : "", s.MyPersonel.Surname, !string.IsNullOrEmpty(s.MyPersonel.SecondSurname) ? " " + s.MyPersonel.SecondSurname : "")));

        }
    }
}
