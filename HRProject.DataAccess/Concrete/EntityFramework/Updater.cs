using HRProject.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework
{
    public class Updater
    {
        public static async Task AddUser(UserManager<Personel> userManager,HRProjectContext _dbContext, RoleManager<UserRole> roleManager)
        {
            await roleManager.CreateAsync(new UserRole() { Name = "admin", NormalizedName = "ADMIN" });
            await roleManager.CreateAsync(new UserRole() { Name = "personel", NormalizedName = "PERSONEL" });

            
            if (_dbContext.Users.FirstOrDefault(x => x.UserName == "OnurcanSk") == null)
            {
                Personel personel = new Personel()
                {
                    Email = "onurcan.sik@bilgeadamboost.com",
                    Id = new Guid(),
                    FirstName = "Onur",
                    SecondName = "Can",
                    Surname = "Şık",
                    Address = "Serdivan/Sakarya",
                    BirthDate = DateTime.ParseExact("1996-12-09 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                    BirthPlace = "Sakarya",
                    ReceiptDate = DateTime.Now.AddYears(-2),
                    IsActive = true,
                    Job = "Back-End Developer",
                    Department = "IT",
                    Salary = 10000,
                    TotalAllowanceAmount=30000,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 5,
                    TC = "45687915478",
                    UserName = "OnurcanSk",
                    PhoneNumber = "905532145785",
                };
                var result = await userManager.CreateAsync(personel, "Aa123456_");
                var roleResult = await userManager.AddToRoleAsync(personel, "personel");
                if (result.Succeeded && roleResult.Succeeded)
                {
                    _dbContext.SaveChanges();
                }
            }           

            if (_dbContext.Users.FirstOrDefault(x => x.UserName == "YavuzMert") == null)
            {
                Personel personel = new Personel()
                {
                    Email = "yavuz.mert@bilgeadamboost.com",
                    Id = new Guid(),
                    FirstName = "Yavuz",
                    Surname = "Mert",
                    Address = "Eyüpsultan/İstanbul",
                    BirthDate = DateTime.ParseExact("1992-03-03 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                    BirthPlace = "Sivas",
                    ReceiptDate = DateTime.Now.AddYears(-3),
                    IsActive = true,
                    Job = "Full Stack Developer",
                    Department = "IT",
                    Salary = 10000,
                    TotalAllowanceAmount = 30000,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 5,
                    IsPasswordResetted = false,
                    TC = "47314298560",
                    UserName = "YavuzMert",
                    PhoneNumber = "905417277198",
                };
                var result = await userManager.CreateAsync(personel, "Bb654321_");
                var roleResult = await userManager.AddToRoleAsync(personel, "personel");
                if (result.Succeeded && roleResult.Succeeded)
                {
                    _dbContext.SaveChanges();
                }
            }


            if (_dbContext.Users.FirstOrDefault(x => x.UserName == "MustafaOzer") == null)
            {
                Personel personel = new Personel()
                {
                    Email = "mustafa.ozer@bilgeadamboost.com",
                    Id = new Guid(),
                    FirstName = "Mustafa",
                    Surname = "Özer",
                    Address = "Çankaya/Ankara",
                    BirthDate = DateTime.ParseExact("1996-03-03 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                    BirthPlace = "Ankara",
                    ReceiptDate = DateTime.Now.AddYears(-4),
                    IsActive = true,
                    Job = "Frontend Developer",
                    Department = "IT",
                    Salary = 10000,
                    TotalAllowanceAmount = 30000,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 5,
                    TC = "48569826816",
                    UserName = "MustafaOzer",
                    PhoneNumber = "905379862578",
                };
                var result = await userManager.CreateAsync(personel, "Cc654321_");
                var roleResult = await userManager.AddToRoleAsync(personel, "personel");
                if (result.Succeeded && roleResult.Succeeded)
                {
                    _dbContext.SaveChanges();
                }
            }


            if (_dbContext.Users.FirstOrDefault(x => x.UserName == "MuhammedYusufKurt") == null)
            {
                Personel personel = new Personel()
                {
                    Email = "muhammedyusuf.kurt@bilgeadamboost.com",
                    Id = new Guid(),
                    FirstName = "Muhammed",
                    SecondName = "Yusuf",
                    Surname = "Kurt",
                    Address = "Maçka/Trabzon",
                    BirthDate = DateTime.ParseExact("1992-06-15 00:00:00,000", "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                    BirthPlace = "Trabzon",
                    ReceiptDate = DateTime.Now,
                    IsActive = true,
                    Job = "SQL Developer",
                    Salary = 10000,
                    TotalAllowanceAmount = 30000,
                    Department = "IT",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnabled = false,
                    AccessFailedCount = 5,
                    TC = "96878522685",
                    UserName = "MuhammedYusufKurt",
                    PhoneNumber = "905468975626",
                };
                var result = await userManager.CreateAsync(personel, "Dd654321_");

                var roleResult = await userManager.AddToRoleAsync(personel, "admin");

                if (result.Succeeded && roleResult.Succeeded)
                {
                    _dbContext.SaveChanges();
                }
            }

            
        }
    }
}
