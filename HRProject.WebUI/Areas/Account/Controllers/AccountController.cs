using HRProject.Business.Abstract;
using HRProject.Business.Helpers;
using HRProject.DataAccess.Abstract;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.Models.ViewModels.Helpers;
using HRProject.Models.ViewModels.PersonelVM;
using HRProject.WebUI.Areas.PersonelArea.Controllers;
using HRProject.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace HRProject.WebUI.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {

        private readonly UserManager<Personel> userManager;
        private readonly SignInManager<Personel> signInManager;
        private readonly HRProjectContext _context;
        private readonly IPersonelBLL _personelBll;
        private readonly RoleManager<UserRole> roleManager;
        public AccountController(UserManager<Personel> userManager, SignInManager<Personel> signInManager, HRProjectContext context, IPersonelBLL personelBll, RoleManager<UserRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _personelBll = personelBll;
            this.roleManager = roleManager;
        }



        [Route("/")]
        public async Task<IActionResult> Login()
        {
            ViewBag.Title = "Giriş";
           //await Updater.AddUser(userManager,_context,roleManager);
            return View(new PersonelLoginVM());
        }
        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Login(PersonelLoginVM model)
        {
            ViewBag.Title = "Giriş";
            var verify = await _personelBll.CheckEmailAndPassword(model.Email, model.Password);
            if (!verify)
            {
                ViewBag.ErrorMessage = "Email adresiniz veya şifreniz hatalı!";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var result = await signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (user.IsPasswordResetted)
                    {
                        return RedirectToAction(nameof(ChangePassword));
                    }
                    else
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        if (roles.Contains("personel"))
                            return Redirect("~/personel/anasayfa");
                        else if(roles.Contains("admin"))
                            return Redirect("~/manager/anasayfa");
                    }
                }
            }
            return View(model);
        }
        [Route("/sifremidegistir")]
        public async Task<IActionResult> ChangePassword()
        {
            ViewBag.Title = "Şifre Değiştirme";
            return View(new PersonelChangePasswordViewModel());
        }
        [HttpPost]
        [Route("/sifremidegistir")]
        public async Task<IActionResult> ChangePassword(PersonelChangePasswordViewModel model)
        {
            ViewBag.Title = "Şifre Değiştirme";
            var verify = await _personelBll.CheckEmailAndPassword(model.Email,model.OldPassword);
            if (!verify)
            {
                ViewBag.ErrorMessage = "Email veya şifreniz hatalı";
                return View(model);
            }
            Regex re = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_+-])[A-Za-z\d@$!%*?&_+-]{6,20}$");
            if (!re.IsMatch(model.NewPassword))
            {
                ViewBag.PasswordError = "Şifreniz en az 6 en fazla 20 karakterden oluşmalı, büyük küçük harf özel\n karakter ve rakam içermelidir.";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                if (model.NewPassword == model.OldPassword)
                {
                    ViewBag.ErrorMessageNewPassword = "Lütfen önceki şifrenizden farklı bir şifre giriniz.";
                    return View(model);
                }              
                var result = await _personelBll.ChangePasswordAsync(model.Email, model.NewPassword);
                if (result.Success)
                {
                    TempData["SuccessPasswordChange"] = "Şifreniz değiştirilmiştir, giriş yapabilirsiniz.";
                    return RedirectToAction(nameof(Login));
                }            
            }
            return View(model);
        }
        [Route("/sifremiunuttum")]
        public async Task<IActionResult> ForgetPassword()
        {
            ViewBag.Title = "Şifremi Unuttum";
            return View(new PersonelForgetPasswordViewModel());
        }
        [HttpPost]
        [Route("/sifremiunuttum")]
        public async Task<IActionResult> ForgetPassword(PersonelForgetPasswordViewModel model)
        {
            ViewBag.Title = "Şifremi Unuttum";
            if (ModelState.IsValid)
            {
                Personel user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var newPassword = ProvideRandomPassword.CreateRandomPassword(10);
                    var result = await _personelBll.ForgetPasswordAsync(model.Email, newPassword);
                    if (result.Success)
                    {
                        ViewBag.Success = "Şifreniz değiştirildi, lütfen emailinizi kontrol ediniz.";
                        TempData["SuccessForgetPassword"] = "Şifreniz değiştirildi, lütfen emailinizi kontrol ediniz."; 
                        return RedirectToAction(nameof(Login));
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Girilen Email adresine ait bir kayıt bulunamadı!";
                }
            }            
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            ViewBag.Title = "Giriş";
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
