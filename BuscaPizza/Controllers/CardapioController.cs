using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaPizza.Models;
/// <summary>
/// 
/// Menu (GET) = Action para a exibição do Menu.cshtml
/// 
/// </summary>
namespace BuscaPizza.Controllers
{
    public class CardapioController : Controller
    {
        // GET: Cardapio

        [AutorizacaPizzaria]
        public ActionResult cadCardapio(int id)
        {
            using (var model = new CardapioModel())
            {
                List<Cardapio> cardapios = model.readCardapio(id);
                ViewBag.cardapios = cardapios;
            }
            return View();
        }

        [AutorizacaPizzaria]
        [HttpPost]
        public ActionResult cadCardapio(FormCollection form)
        {
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            int id_Pizzaria = int.Parse(form["id_Pizzaria"]);
            string Nome = form["Nome"];

            var c = new Cardapio();

            c.id_Pizzaria = id_Pizzaria;
            c.nomeCardapio = Nome;

            using (var model = new CardapioModel())
            {
                model.cadCardapio(c);
                return RedirectToAction("cadCardapio", new { id = pizzaria.Id });
            }
        }
        [AutorizacaPizzaria]
        [HttpPost]
        public ActionResult updateCardapio(FormCollection form, int id)
        {
            ViewBag.Cardapio = new Cardapio();
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            //int id = int.Parse(form["Id"]);
            string nome = form["Nome"];

            var cardapio = new Cardapio();

            cardapio.id = id;
            cardapio.nomeCardapio = nome;

            using (var model = new CardapioModel())
            {
                model.updateCardapio(cardapio);
            }
            return RedirectToAction("cadCardapio",new { id = pizzaria.Id });
        }
        [AutorizacaPizzaria]
        public ActionResult deleteCardapio(int id)
        {
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            using (var model = new CardapioModel())
            {
                model.deleteCardapio(id);
            }
            return RedirectToAction("cadCardapio", new { id = pizzaria.Id });
        }
        [AutorizacaoCliente]
        public ActionResult CardapioCliente(int id)
        {
            Dictionary<string, List<Produto>> cardapios = new Dictionary<string,List<Produto>>();
            
            using (var modelC = new CardapioModel())
            using (var modelP = new ProdutoModel())
            {
                List<Cardapio> _cardapios = modelC.clienteCardapio(id);

                foreach (var c in _cardapios)
                {                   
                   List<Produto> produtos = modelP.readProduto(c.id);
                   cardapios.Add(c.nomeCardapio, produtos);
                }
            }
            return View(cardapios);
        }
    }
}