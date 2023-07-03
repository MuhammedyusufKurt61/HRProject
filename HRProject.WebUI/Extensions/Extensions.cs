using System.Security.Claims;

namespace HRProject.WebUI.Extensions
{
    public static class Extensions
    {
        public static string GetFullNameOrEmail(this ClaimsPrincipal principal)
        {
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullName?.Value ?? principal.Identity?.Name;
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName");
            return fullName?.Value;
        }

    }
}
