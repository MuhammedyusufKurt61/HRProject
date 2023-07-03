using AutoMapper;
using HRProject.Business.Abstract;
using HRProject.Business.Helpers;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Abstract;
using HRProject.Entities;
using HRProject.Models.ViewModels.PersonelVM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using HRProject.Models.ViewModels.Helpers;

namespace HRProject.Business.Concrete
{
    public class ManagerBLL : IManagerBLL
    {
        private readonly UserManager<Personel> userManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly IPersonelRepository personelRepository;
        private readonly IMapper mapper;
        public ManagerBLL(UserManager<Personel> userManager, RoleManager<UserRole> roleManager, IMapper mapper, IPersonelRepository personelRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.personelRepository = personelRepository;
        }

        public GetManyResultAsQueryable<PersonelSummaryViewModel> GetPersonelist(Personel model)
        {
            GetManyResultAsQueryable<PersonelSummaryViewModel> result = new GetManyResultAsQueryable<PersonelSummaryViewModel>();
            var data = personelRepository.GetAll(x => x.CompanyId == model.CompanyId);
            List<PersonelSummaryViewModel> list = new List<PersonelSummaryViewModel>();
            foreach (var item in data.Data)
            {
                list.Add(mapper.Map<PersonelSummaryViewModel>(item));
            }

            result.Data = list.AsQueryable();
            return result;
        }

        public async Task<GetOneResult<PersonelCreateVm>> CreateNewPersonel(PersonelCreateVm personel, params string[] role)
        {
            GetOneResult<PersonelCreateVm> oneResult = new GetOneResult<PersonelCreateVm>(); 
            oneResult.Data = personel;
            Personel newPersonel = mapper.Map<Personel>(personel);
            var newPassword = ProvideRandomPassword.CreateRandomPassword(10);
            newPersonel.UserName = ProvideRandomPassword.ReplaceTurkishCharacters(newPersonel.UserName);
            newPersonel.Email = ProvideRandomPassword.ReplaceTurkishCharacters(newPersonel.Email);
            var result = await userManager.CreateAsync(newPersonel, newPassword);           
            if (result.Succeeded)
            {
                string email = newPersonel.Email;
                foreach (var item in role)
                {
                   var roleResult = await userManager.AddToRoleAsync(newPersonel, item);
                    if (!roleResult.Succeeded)
                    {
                        oneResult.Success = false;
                        return oneResult;
                    }
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(email);
                    mailMessage.From = new MailAddress("myk361@hotmail.com");
                    mailMessage.Subject = "Kayıt Oluşturma";
                    mailMessage.Priority = MailPriority.High;
                    mailMessage.Body = $"Kaydınız oluşturuldu! <br>İlk Giriş Şifreniz: {newPassword} <br><a href='{MagicStrings.Url}'>Buraya tıklayarak Giriş Sayfasına gidebilirsiniz.</a>";
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("myk361@hotmail.com", "trabzon14611641");
                    smtp.Port = 587;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.EnableSsl = true;
                    var t = new Thread(new ThreadStart(() => smtp.Send(mailMessage)));
                    t.IsBackground = true;
                    t.Start();
                }
                return oneResult;
            }
            oneResult.Success = false;
            return oneResult;
        }
        public GetOneResult<Expense> ApproveExpense(Expense model)
        {
            GetOneResult<Expense> result = new GetOneResult<Expense>();
            GetOneResult<Personel> personel = new GetOneResult<Personel>();
            personel.Data = personelRepository.Get(x => x.Id == model.PersonelId).Data;
            result.Data = model;
            personel.Data.Expenses.Add(result.Data);
            result.Success = true;

            return result;
        }
        public GetOneResult<Expense> RejectExpense(Expense model)
        {
            GetOneResult<Expense> result = new GetOneResult<Expense>();
            GetOneResult<Personel> personel = new GetOneResult<Personel>();
            personel.Data = personelRepository.Get(x => x.Id == model.PersonelId).Data;
            result.Data = model;
            personel.Data.Expenses.Remove(result.Data);
            result.Success = true;

            return result;
        }
    }
}
