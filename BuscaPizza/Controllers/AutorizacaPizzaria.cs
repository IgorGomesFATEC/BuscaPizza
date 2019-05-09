using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaPizza.Controllers
{
    public class AutorizacaPizzaria : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["pizzaria"] == null)
            {
                filterContext.Result = new RedirectResult("/Pizzaria/LoginPizzaria");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}