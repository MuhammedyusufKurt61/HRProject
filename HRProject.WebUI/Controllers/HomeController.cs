using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.Models.Enums;
using HRProject.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

namespace HRProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Personel> _userManager;
        private readonly HRProjectContext _context;
        private readonly IFileProvider _provider;
        private readonly RoleManager<UserRole> roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Personel> userManager, HRProjectContext context, IFileProvider provider, RoleManager<UserRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _provider = provider;
            this.roleManager = roleManager;
        }
        private List<SelectListItem> EnumToSelectList<T>()
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
            return enumValues.Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = Convert.ToInt32(e).ToString()
            }).ToList();
        }
        [Route("/GetSecondSelectData")]
        public IActionResult GetSecondSelectData(int type) 
        { 
            if(type == (int)AllowanceType.InSource)
            {
                //List<SelectListItem> select = Enum.GetValues(typeof(Currency)).Cast<Currency>().Select(v => new SelectListItem() { Text = v.ToString(), Value = v.ToString() }).ToList();
                return Json(EnumToSelectList<Currency>());
            }
            else 
            {
                List<SelectListItem> select = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text=Currency.TL.ToString(),
                        Value=Convert.ToInt32(Currency.TL).ToString()
                    }
                };
                return Json(select);
            }
        }
        public async Task<IActionResult> Index()
        {
            await Updater.AddUser(_userManager, _context,roleManager);

            return View();
        }
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle( Personel personel)
        {
            IActionResult result = null;

            if (ModelState.IsValid)
            {
                try
                {
                    //var root = _provider.GetDirectoryContents("wwwroot");
                    //var images = root.First(x => x.Name == "images");
                    //var path = Path.Combine(images.PhysicalPath, personel.Photo.FileName);
                    //using var stream = new FileStream(path, FileMode.Create);
                    //personel.Photo.CopyTo(stream);
                    //personel.PhotoPath=personel.Photo.FileName;
                    //result = RedirectToAction("Index");
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            else
            {
                result = View();
            }
            return result;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}