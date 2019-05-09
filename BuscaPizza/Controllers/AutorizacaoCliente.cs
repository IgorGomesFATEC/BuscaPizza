using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaPizza.Controllers
{
    public class AutorizacaoCliente : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["cliente"] == null)
            {
                filterContext.Result = new RedirectResult("/Cliente/cadCliente");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}