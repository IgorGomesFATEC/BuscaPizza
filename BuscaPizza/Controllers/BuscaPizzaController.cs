using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 
/// Home (GET) = Metodo para exibir a home
/// 
/// </summary>

namespace BuscaPizza.Controllers
{
    public class BuscaPizzaController : Controller
    {
        // GET: BuscaPizza
        public ActionResult Home()
        {
            return View();
        }
    }
}