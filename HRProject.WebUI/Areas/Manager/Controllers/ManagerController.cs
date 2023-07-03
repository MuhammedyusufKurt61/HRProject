using HRProject.Business.Abstract;
using HRProject.Core.Utilities;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.Models.ViewModels.AllowanceVM;
using HRProject.Models.ViewModels.ExpenseVM;
using HRProject.Models.ViewModels.PersonelVM;
using HRProject.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace HRProject.WebUI.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles ="admin")]
    public class ManagerController : Controller
    {
        private readonly IPersonelBLL _personelBLL;
        private readonly IFileProvider _provider;
        private readonly UserManager<Personel> userManager;
        private readonly SignInManager<Personel> signInManager;
        private readonly HRProjectContext _context;
        private readonly IAllowanceBLL allowanceBLL;
        private readonly IExpenseBLL expenseBLL;
        private readonly IManagerBLL managerBLL;

        public ManagerController(IPersonelBLL personelBLL, IFileProvider provider, UserManager<Personel> userManager, SignInManager<Personel> signInManager, HRProjectContext context, IAllowanceBLL allowanceBLL, IExpenseBLL expenseBLL, IManagerBLL managerBLL)
        {
            _personelBLL = personelBLL;
            _provider = provider;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            this.allowanceBLL = allowanceBLL;
            this.expenseBLL = expenseBLL;
            this.managerBLL = managerBLL;
        }

        [Route("/manager/personeller")]
        public async Task<IActionResult> GetPersonelList([FromQuery] int pageNo = 1)
        {
            
            var result = await userManager.GetUserAsync(User);
            var personelList =  managerBLL.GetPersonelist(result);
            var personel = _personelBLL.GetPersonelDetailInfos(result.Id);
            ViewBag.Photo = personel.Data.PhotoPath;
            ViewBag.FullName = personel.Data.FullName;
            if (personelList.Success)
            {
                var pagedList = personelList.Data.GetPaged(pageNo, 10);
                return View(pagedList);
            }
            return View(new PersonelSummaryViewModel());
        }

        [HttpGet("/manager/yenipersonel")]
        public async Task<IActionResult> CreatePersonel()
        {
            var personel = await userManager.GetUserAsync(User);
            var result = _personelBLL.GetPersonelSummaryInfos(personel.Id);
            ViewBag.Photo = result.Data.PhotoPath;
            ViewBag.FullName = result.Data.FullName;
            ViewBag.Photo = personel.PhotoPath;
            return View();  
        }

        [HttpPost]
        [Route("/manager/yenipersonel")]
        public async Task<ActionResult> CreatePersonel(PersonelCreateVm model)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.Photo = personel.PhotoPath;
           
            if (ModelState.IsValid)
            {
                try
                {                    
                    var result =await managerBLL.CreateNewPersonel(model,"personel");   
                    if (result.Success)
                    {
                        ViewBag.success = "İşlem başarılı";
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.Error = "İşlem başarısız";
                    return View(model);
                }
                catch (Exception)
                {
                    ViewBag.Error = "Beklenmeyen bir hata olustu";
                    return View(model);
                }

            }
            return View();
        }

        [Route("/manager/anasayfa")]
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

        [Route("/manager/detaylar")]
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
        [Route("/manager/guncelle")]
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
        [Route("/manager/guncelle")]
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

       

        [Route("manager/harcama/listele")]
        public async Task<IActionResult> ExpenseList([FromQuery] int pageNo = 1)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            var expenseList = expenseBLL.GetAllExpenses().Data.OrderBy(x => x.DemandDate).Where(x=>x.State == HRProject.Models.Enums.ExpenseState.Pending);
            var pagedList = expenseList.GetPaged(pageNo, 10);
            return View(pagedList);
        }
        [Route("manager/avans/listele")]
        public async Task<IActionResult> AllowanceList([FromQuery] int pageNo = 1)
        {
            var personel = await userManager.GetUserAsync(User);
            ViewBag.FullName = string.Concat(personel.FirstName, " ", (!string.IsNullOrEmpty(personel.SecondName) ? personel.SecondName + " " : ""), personel.Surname, (!string.IsNullOrEmpty(personel.SecondSurname) ? " " + personel.SecondSurname : ""));
            ViewBag.Photo = personel.PhotoPath;
            var allowanceList = allowanceBLL.GetAllAllowances().Data.OrderBy(x => x.DemandDate).Where(x => x.State == HRProject.Models.Enums.AllowanceState.Pending);
            var pagedList = allowanceList.GetPaged(pageNo, 10);
            return View(pagedList);
        }

        [Route("manager/avans/onayla")]
        public async Task<IActionResult> ApproveAllowance([FromQuery]string id)
        {
            if (ModelState.IsValid)
            {
                allowanceBLL.AllowanceApprove(new AllowanceApproveVm() { Id = Guid.Parse(id)});
                return RedirectToAction(nameof(AllowanceList));
            };
            ViewBag.Error = "Beklenmeyen bir hata oluştu";
            return RedirectToAction(nameof(AllowanceList));
        }

        [Route("manager/avans/reddet")]
        public async Task<IActionResult> RejectAllowance([FromQuery] string id)
        {
            if (ModelState.IsValid)
            {
                allowanceBLL.AllowanceReject(new AllowanceRejectVm() { Id= Guid.Parse(id) });
                return RedirectToAction(nameof(AllowanceList));
            };
            ViewBag.Error = "Beklenmeyen bir hata oluştu";
            return RedirectToAction(nameof(AllowanceList));
        }
        [Route("manager/harcama/onayla")]
        public async Task<IActionResult> ApproveExpense([FromQuery] string id)
        {
            if (ModelState.IsValid)
            {
                expenseBLL.ExpenseApprove(new ExpenseApproveVm() { Id= Guid.Parse(id) });
                return RedirectToAction(nameof(ExpenseList));
            };
            ViewBag.Error = "Beklenmeyen bir hata oluştu";
            return RedirectToAction(nameof(ExpenseList));
        }
        [Route("manager/harcama/reddet")]
        public async Task<IActionResult> RejectExpense([FromQuery]string id)
        {
            if (ModelState.IsValid)
            {
                expenseBLL.ExpenseReject(new ExpenseRejectVm() { Id= Guid.Parse(id) });
                return RedirectToAction(nameof(ExpenseList));
            };
            ViewBag.Error = "Beklenmeyen bir hata oluştu";
            return RedirectToAction(nameof(ExpenseList));
        }

    }
}
