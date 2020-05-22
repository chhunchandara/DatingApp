using System;
using System.Linq;
using System.Reflection;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatingApp.API.Data
{
    public class CustomActionFilter :  IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
            //throw new System.NotImplementedException();
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {   
            if (!IsProtectedAction(context))
                return;
            
            if (!IsUserAuthenticated(context))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            Console.WriteLine(ActionInfo(context));
            
        }

        private bool IsProtectedAction(ActionExecutingContext context)
        {
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
                return false;

            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var controllerTypeInfo = controllerActionDescriptor.ControllerTypeInfo;
            var actionMethodInfo = controllerActionDescriptor.MethodInfo;

            var authorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            authorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizeAttribute != null)
                return true;

            return false;
        }
        private bool IsUserAuthenticated(ActionExecutingContext context)
        {
            return context.HttpContext.User.Identity.IsAuthenticated;
        }
        private ActionInfo ActionInfo(ActionExecutingContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var area = controllerActionDescriptor.ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue;
            var controller = controllerActionDescriptor.ControllerName;
            var action = controllerActionDescriptor.ActionName;
            return new ActionInfo(controller,action,area);
        }
    }
}