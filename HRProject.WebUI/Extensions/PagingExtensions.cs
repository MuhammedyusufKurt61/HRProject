using HRProject.Models.ViewModels.Helpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Page = HRProject.Models.ViewModels.Helpers.Page;

namespace HRProject.WebUI.Extensions
{
    public static class PagingExtensions
    {
        public static PagedViewModel<T> GetPaged<T>(this IQueryable<T> query, int currentPage, int pageSize) where T : class
        {
            var count = query.Count();
            Page paging = new(currentPage, pageSize, count);
            var data = query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToList();

            var result = new PagedViewModel<T>(data, paging);
            return result;
        }
    }
}
