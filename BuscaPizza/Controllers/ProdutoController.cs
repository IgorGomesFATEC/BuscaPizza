using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaPizza.Models;

namespace BuscaPizza.Controllers
{
    [AutorizacaPizzaria]
    public class ProdutoController : Controller
    {
        // GET: Produto
        
       public ActionResult readProduto(int id)
        {
            using (var model = new ProdutoModel())
            {

                List<Produto> prod = model.readProduto(id);
                ViewBag.prod = prod;
                
            }
            return View();
        }


        public ActionResult cadProduto(int id)
        {
            ViewBag.CardapioId = id;

            return View();
        }

        [HttpPost]
        public ActionResult cadProduto(FormCollection form)
        {
            var prod = new Produto();
            prod.id_Cardapio = int.Parse(form["id_Cardapio"]);
            prod.NomeProduto = form["Nome"];
            prod.Preco = decimal.Parse(form["Preco"]);
            prod.descricao = form["Descricao"];

            using (ProdutoModel model = new ProdutoModel())
            {
                model.cadProduto(prod);
            }

            return RedirectToAction("readProduto",new { id = prod.id_Cardapio });
        }
    }
}