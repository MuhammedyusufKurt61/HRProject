using AutoMapper;
using HRProject.Business.Abstract;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Abstract;
using HRProject.Entities;
using HRProject.Models.ViewModels.PersonelVM;
using HRProject.Business.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Models.ViewModels.AllowanceVM;

namespace HRProject.Business.Concrete
{
    public class PersonelBLL : IPersonelBLL
    {
        private readonly IMapper mapper;
        private readonly IPersonelRepository personelRepository;
        private readonly UserManager<Personel> userManager;

        public PersonelBLL(IMapper mapper, IPersonelRepository personelRepository, UserManager<Personel> userManager)
        {
            this.mapper = mapper;
            this.personelRepository = personelRepository;
            this.userManager = userManager;
        }       

        public async Task<GetOneResult<PersonelUpdateViewModel>> ChangePasswordAsync(string email, string password)
        {
            GetOneResult<PersonelUpdateViewModel> result = new GetOneResult<PersonelUpdateViewModel>();
            Personel personel = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(personel);
            var resetResult = await userManager.ResetPasswordAsync(personel, token, password);
            if (personel.IsPasswordResetted)
            {
                personel.IsPasswordResetted = false;
                personelRepository.Update(personel);
            }         
            result.Success = resetResult.Succeeded;
            return result;
        }

        public async Task<bool> CheckEmailAndPassword(string email, string password)
        {
            Personel personel = await userManager.FindByEmailAsync(email);
            if (personel == null)
            {
                return false;
            }
            var result = userManager.PasswordHasher.VerifyHashedPassword(personel, personel.PasswordHash, password);
            if (result != PasswordVerificationResult.Success)
            {
                return false;
            }
            return true;
        }

        public async Task<GetOneResult<PersonelUpdateViewModel>> ForgetPasswordAsync(string email, string password)
        {
            GetOneResult<PersonelUpdateViewModel> result = new GetOneResult<PersonelUpdateViewModel>();
            Personel personel = await userManager.FindByEmailAsync(email);
            var token = await userManager.GeneratePasswordResetTokenAsync(personel);
            var resetResult = await userManager.ResetPasswordAsync(personel, token, password);
            result.Success = resetResult.Succeeded;
            if (resetResult.Succeeded)
            {
                personel.IsPasswordResetted = true;
                var updateResult = personelRepository.Update(personel);
                if (updateResult.Success)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(email);
                    mailMessage.From = new MailAddress("myk361@hotmail.com");
                    mailMessage.Subject = "Şifre Sıfırlama";
                    mailMessage.Body = $"Şifreniz başarıyla sıfırlandı! <br>Yeni Şifreniz: {password} <br><a href='{MagicStrings.Url}'>Buraya tıklayarak Giriş Sayfasına gidebilirsiniz.</a>";
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("myk361@hotmail.com", "trabzon14611641");
                    smtp.Port = 587;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.EnableSsl = true;
                    smtp.Send(mailMessage);
                }
            }
            return result;

        }

        public GetOneResult<PersonelDetailViewModel> GetPersonelDetailInfos(Guid id)
        {
            GetOneResult<PersonelDetailViewModel> result = new GetOneResult<PersonelDetailViewModel>();

            Personel personel = personelRepository.Get(x => x.IsActive && x.Id == id).Data;

            result.Data = mapper.Map<PersonelDetailViewModel>(personel);

            return result;
        }

        public GetOneResult<PersonelSummaryViewModel> GetPersonelSummaryInfos(Guid id)
        {
            GetOneResult<PersonelSummaryViewModel> result = new GetOneResult<PersonelSummaryViewModel>();

            Personel personel = personelRepository.Get(x => x.Id.Equals(id)).Data;

            result.Data = mapper.Map<PersonelSummaryViewModel>(personel);

            return result;
        }

        public GetOneResult<PersonelUpdateViewModel> GetUpdatePersonel(Guid id)
        {
            GetOneResult<PersonelUpdateViewModel> result = new GetOneResult<PersonelUpdateViewModel>();

            Personel personel = personelRepository.Get(x => x.Id.Equals(id)).Data;

            result.Data = mapper.Map<PersonelUpdateViewModel>(personel);

            return result;
        }

        public GetOneResult<PersonelUpdateViewModel> UpdatePersonel(PersonelUpdateViewModel model)
        {
            Personel personel = personelRepository.Get(x => x.Id == model.Id).Data;
            personel.PhotoPath = model.PhotoPath;
            personel.Address = model.Address;
            personel.PhoneNumber = model.PhoneNumber;
            var data = personelRepository.Update(personel);
            return new GetOneResult<PersonelUpdateViewModel> { Data = mapper.Map<PersonelUpdateViewModel>(data.Data) };

        }
    }
}
