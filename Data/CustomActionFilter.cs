using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.API.Data
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {   
            context.Result=new UnauthorizedResult();
            Console.WriteLine("This is request host : " + context.HttpContext.Request.Host);
            Console.WriteLine("This is request path : " + context.HttpContext.Request.Path);
            return;
        }
    }
}