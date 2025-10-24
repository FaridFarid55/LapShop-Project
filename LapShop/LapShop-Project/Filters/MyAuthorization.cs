using Microsoft.AspNetCore.Mvc.Filters;


namespace LapShop.Filters
{
    // Form Farid Farid
    public class MyAuthorization : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string actionName = context.HttpContext.Request.RouteValues["action"].ToString();
            string controllerName = context.HttpContext.Request.RouteValues["controller"].ToString();

            // check in database if user has permission
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
