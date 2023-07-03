using HRProject.Business.Abstract;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.Models.Enums;
using HRProject.Models.ViewModels.AllowanceVM;
using HRProject.Models.ViewModels.PersonelVM;
using HRProject.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using HRProject.Models.ViewModels.ExpenseVM;

namespace HRProject.WebUI.Areas.PersonelArea.Controllers
{
    [Area("Personel")]
    [Authorize(Roles ="personel")]
    public class PersonelController : Controller
    {
        private readonly IPersonelBLL _personelBLL;
        private readonly IFileProvider _provider;
        private readonly UserManager<Personel> userManager;
        private readonly SignInManager<Personel> signInManager;
        private readonly HRProjectContext _context;
        private readonly IAllowanceBLL allowanceBLL;
        private readonly IExpenseBLL expenseBLL;
        public PersonelController(IPersonelBLL personelBLL, IFileProvider provider, UserManager<Personel> userManager, SignInManager<Personel> signInManager, HRProjectContext context, IAllowanceBLL allowanceBLL, IExpenseBLL expenseBLL)
        {
            _personelBLL = personelBLL;
            _provider = provider;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this.allowanceBLL = allowanceBLL;
            this.expenseBLL = expenseBLL;
        }

        [Route("/personel/harcamaolustur")]
        public async Task<IActionResult> CreateExpense()
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            return View();
        }
        [HttpPost]
        [Route("/personel/harcamaolustur")]
        public async Task<IActionResult> CreateExpense(CreateExpenseVM model)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.file = personel.PhotoPath;         
            if (ModelState.IsValid)
            {
                try
                {
                    var root = _provider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "Files");
                    if (model.MyFile != null)
                    {
                        var randomImagewName = Guid.NewGuid() + Path.GetExtension(model.MyFile.FileName);
                        var path = Path.Combine(images.PhysicalPath, randomImagewName);
                        using var stream = new FileStream(path, FileMode.Create);
                        model.MyFile.CopyTo(stream);
                        model.FilePath = randomImagewName;
                    }

                    model.Id = Guid.NewGuid();
                    model.PersonelID = (await userManager.GetUserAsync(User)).Id;
                    var result = expenseBLL.CreateExpense(model);
                    if (result.Success)
                    {
                        ViewBag.Error = "";
                        return RedirectToAction(nameof(ListExpense));
                    }                    
                }
                catch (Exception)
                {
                    ViewBag.Error = "Beklenmeyen bir hata olustu";
                    return View(model);
                }
            }
            return View(model);
        }

        [Route("/personel/harcama/iptalet")]
        public async Task<IActionResult> CancelExpense([FromQuery] string id)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            var result = expenseBLL.CancelExpense(new ExpenseCancelVM() { Id = new Guid(id), PersonelId = (await userManager.GetUserAsync(User)).Id });
            return RedirectToAction(nameof(ListExpense));
        }

        [Route("/personel/harcamalar")]
        public async Task<IActionResult> ListExpense([FromQuery] int pageNo = 1)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;

            var expenseList = expenseBLL.GetExpenses(personel.Id).Data.OrderBy(x => x.DemandDate);
            var pagedList = expenseList.GetPaged(pageNo, 10);
            return View(pagedList);
        }


        [Route("/personel/avanslar")]
        public async Task<IActionResult> ListAllowance([FromQuery] int pageNo = 1)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            var allowanceList = allowanceBLL.GetAllowances(personel.Id).Data.OrderByDescending(x => x.DemandDate);
            var pagedList = allowanceList.GetPaged(pageNo, 5);
            return View(pagedList);
        }

        [Route("/personel/avansolustur")]
        public async Task<IActionResult> CreateAllowance()
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            return View();
        }
        [HttpPost]
        [Route("/personel/avansolustur")]
        public async Task<IActionResult> CreateAllowance(CreateAllowanceVM model)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            if (model.Type == AllowanceType.OutSource && model.CurrentUnit != Currency.TL)
            {
                ViewBag.Error = "Bireysel Avanslarda Yabancı Para Birimi Kullanılamaz.";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = Guid.NewGuid();
                    model.PersonelID = (await userManager.GetUserAsync(User)).Id;
                    var result = allowanceBLL.CreateAllowance(model);
                    if (result.Success)
                    {
                        ViewBag.Error = "";
                        return RedirectToAction(nameof(ListAllowance));
                    }
                    else
                    {
                        ViewBag.Error = "Bireysel Avans Limitiniz Yeterli Değildir.";
                        return View(model);
                    }
                }
                catch (Exception)
                {
                    ViewBag.Error = "Beklenmeyen bir hata olustu";
                    return View(model);
                }
            }
            return View(model);
        }
        [Route("/personel/avans/iptalet")]
        public async Task<IActionResult> CancelAllowance([FromQuery]string id)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            var result = allowanceBLL.CancelAllowance(new AllowanceCancelVM() { Id = new Guid(id), PersonelId = (await userManager.GetUserAsync(User)).Id });       
            return RedirectToAction(nameof(ListAllowance));
        }

        [Route("/personel/anasayfa")]
        public async Task<IActionResult> Index()
        {
            var personel = await userManager.GetUserAsync(User);
            var result = _personelBLL.GetPersonelSummaryInfos(personel.Id);
            ViewBag.Photo = result.Data.PhotoPath;
            ViewBag.FullName = result.Data.FullName;
            if (result.Success)
            {
                return View(result.Data);
            }
            return View(new PersonelSummaryViewModel());
        }
        [Route("/personel/detaylar")]
        public async Task<IActionResult> Details()
        {
            var result = await userManager.GetUserAsync(User);
            var personel = _personelBLL.GetPersonelDetailInfos(result.Id);
            ViewBag.Photo = personel.Data.PhotoPath;
            ViewBag.FullName = personel.Data.FullName;
            if (personel.Success)
            {
                return View(personel.Data);
            }
            return View(new PersonelDetailViewModel());
        }

        [HttpGet]
        [Route("/personel/guncelle")]
        public async Task<IActionResult> Edit()
        {
            var personel = await userManager.GetUserAsync(User);
            var result = _personelBLL.GetUpdatePersonel(personel.Id);
            ViewBag.Photo = result.Data.PhotoPath;
            ViewBag.FullName = result.Data.FullName;
            if (result.Success)
            {
                return View(result.Data);
            }
            return View(new PersonelUpdateViewModel());
        }
        [HttpPost]
        [Route("/personel/guncelle")]
        public async Task<ActionResult> Edit(PersonelUpdateViewModel model)
        {
            if (model == null)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var root = _provider.GetDirectoryContents("wwwroot");
                    var images = root.First(x => x.Name == "personelImages");
                    if (model.Photo != null)
                    {
                        var randomImagewName = Guid.NewGuid() + Path.GetExtension(model.Photo.FileName);
                        var path = Path.Combine(images.PhysicalPath, randomImagewName);
                        using var stream = new FileStream(path, FileMode.Create);
                        model.Photo.CopyTo(stream);
                        model.PhotoPath = randomImagewName;
                    }
                    model.Id = (await userManager.GetUserAsync(User)).Id;
                    _personelBLL.UpdatePersonel(model);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ViewBag.Error = "Beklenmeyen bir hata olustu";
                    return View(model);
                }

            }
            ViewBag.Photo = model.PhotoPath;
            ViewBag.FullName = model.FullName;
            return View(model);
        }

    }
}
