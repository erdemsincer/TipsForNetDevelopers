using DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authorization.Attributes
{
    public sealed class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public RoleAttribute(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            AppDbContext AppDbContext = new();
            var userIdClaim = context.HttpContext.User.FindFirst
                (ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Result=new UnauthorizedResult();
                return;
            }
            var userHasRole=AppDbContext.UserRoles
                .Where(x=>x.UserId==Convert.ToInt32(userIdClaim.Value))
                .Include(y=>y.Role)
                .Any(p=>p.Role.Name==_role);
            if (!userHasRole)
            {
                context.Result = new UnauthorizedResult();
                return;

            }


        }
    }
}
