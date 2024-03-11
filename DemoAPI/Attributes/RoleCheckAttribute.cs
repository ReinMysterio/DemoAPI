using DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DemoAPI.Attributes
{
    public class RoleCheckAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly RoleType[] _roleTypes;

        public RoleCheckAttribute(params RoleType[] roleType)
        {
            _roleTypes = roleType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAccess = _roleTypes.Any(r => context.HttpContext.User.HasClaim(ClaimTypes.Role,$"{(int)r}"));          

            if (!hasAccess)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }   
        }
    }
}
