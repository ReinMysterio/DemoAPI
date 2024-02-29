using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DemoAPI.Attributes
{
    public class RoleCheckAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _roleType;

        public RoleCheckAttribute(string roleType)
        {
            _roleType = roleType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasAccess = context.HttpContext.User.Claims.Any(r => r.Type == ClaimTypes.Role && r.Value == _roleType);

            if (!hasAccess)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }   
        }
    }
}
