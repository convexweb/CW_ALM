using CW_ALM.Domain.Entities;
using CW_ALM.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;

namespace CW_ALM.WebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (Usuario)context.HttpContext.Items["Usuario"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Acesso não autorizado" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
