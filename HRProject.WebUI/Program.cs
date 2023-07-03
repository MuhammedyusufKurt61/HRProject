using AutoMapper;
using HRProject.DataAccess.Concrete.EntityFramework;
using HRProject.Entities;
using HRProject.WebUI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HRProjectContext>();
builder.Services.AddIdentity<Personel, UserRole>().AddEntityFrameworkStores<HRProjectContext>().AddSignInManager().AddTokenProvider<DataProtectorTokenProvider<Personel>>(TokenOptions.DefaultProvider);
builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromHours(12);
    option.LoginPath = "/";
    option.SlidingExpiration = true;
});
var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddAutoMapper(assembly);
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddServiceInjections();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
var options = ((IApplicationBuilder)app).ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseEndpoints(end =>
{

    end.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
    end.MapControllerRoute(
    name: "default",
    pattern: "/{personel}/{anasayfa}");
    
});

app.UseHttpsRedirection();
app.UseStaticFiles();




app.Run();


