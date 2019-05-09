using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaPizza.Models;

/// <summary>
/// 
/// cadPizzaria (GET) = Retorna para a View cadPizzaria
/// cadPizzaria (POST) = Puxa oque esta no Form e Manda para PizzariaModel e Volta para a Propria Pagina
/// dashBoard   (GET) = Retorna para a View dasBoard
/// cadCardapio (GET) = Retorna para a View cadCardapio
/// 
/// </summary>

namespace BuscaPizza.Controllers
{
    public class PizzariaController : Controller
    {
        // GET: Pizzaria
        [HttpGet]
        public ActionResult cadPizzaria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadPizzaria(FormCollection form)
        {
            var pizzaria = new Pizzaria();
            pizzaria.Email = form["Email"];
            pizzaria.Senha = form["Senha"];
            pizzaria.Telefone = form["Telefone"];
            pizzaria.CEP = form["CEP"];
            pizzaria.NomePizzaria = form["NomePizzaria"];
            pizzaria.CNPJ = form["CNPJ"];

            try
            {
                using (var model = new PizzariaModel())
                {
                    model.cadPizzaria(pizzaria);
                    return RedirectToAction("LoginPizzaria");
                }
            }
            catch(Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View();
            }

        }
        public ActionResult LoginPizzaria()
        {
            ViewBag.NaoExiste = false;
            return View();
        }
        [HttpPost]
        public ActionResult LoginPizzaria(FormCollection form)
        {
            string email = form["email"];
            string senha = form["senha"];
            using (var model = new PizzariaModel())
            {
                Pizzaria pizzaria = model.LoginPizzaria(email, senha);
                if(pizzaria!=null)
                {
                    Session["pizzaria"] = pizzaria;
                    return RedirectToAction ("dashboard");
                }
                else
                {
                    ViewBag.NaoExiste = true;
                    return View();
                }
            }
        }
        [AutorizacaoCliente]
        public ActionResult Menu()
        {
            using (var model = new PizzariaModel())
            {
                List<Pizzaria> piz = model.menuCliente();
                ViewBag.piz = piz;
            }
            return View();
        }
        [AutorizacaPizzaria]
        public ActionResult dashBoard()
        {

            return View();
        }
        [AutorizacaPizzaria]
        public ActionResult minhaPizzaria(int id)
        {
            using (var model = new PizzariaModel())
            {
                ViewBag.pizzaria = model.ReadMinhaPizzaria(id);
                return View();
            }
        }
        [HttpPost]
        [AutorizacaPizzaria]
        public ActionResult minhaPizzaria(FormCollection form)
        {
            ViewBag.Pizzaria = new Pizzaria();

            int id = int.Parse(form["Id"]);
            string nome = form["Nome"];
            string email = form["Email"];
            string senha = form["Senha"];
            string Telefone = form["Telefone"];
            string CNPJ = form["Cnpj"];

            var pizzaria = new Pizzaria();
            pizzaria.Id = id;
            pizzaria.NomePizzaria = nome;
            pizzaria.Email = email;
            pizzaria.Senha = senha;
            pizzaria.Telefone = Telefone;
            pizzaria.CNPJ = CNPJ;

            try
            {
                using (var model = new PizzariaModel())
                {
                    model.UpdateMinhaPizzaria(pizzaria);
                }
                return RedirectToAction("dashBoard");
            }
            catch(Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View();
            }

        }
        public ActionResult ativarPizzaria(int id)
        {
            using (var model = new PizzariaModel())
            {
                model.ativarPizzaria(id);
            }
            return RedirectToAction("dashBoard");
        }
        public ActionResult desativarPizzaria(int id)
        {
            using (var model = new PizzariaModel())
            {
                model.desativarPizzaria(id);
            }
            return RedirectToAction("dashBoard");
        }
        [AutorizacaPizzaria]
        public ActionResult readPedidos (int id)
        {
            using (var model = new PedidoModel())
            {
                List<Pedido> ped = model.readPedidos(id);
                ViewBag.Pedido = ped;
            }
            return View();
        }

        [AutorizacaPizzaria]
        public ActionResult DetalhePedido (int id)
        {
            ViewBag.id_Pedido = id;
            using (var model = new PedidoModel())
            {
                List<Pedido> ped = model.readDetalhePedidosCarrinho(id);
                ViewBag.Carrinho = ped;
                Pedido ped2 = model.readDetalhePedidos(id);
                ViewBag.resumo = ped2;
            }
            return View();
        }

        public ActionResult PedidoCaminho(int id)
        {
            //TODO Verificar Return
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            using (var model = new PizzariaModel())
            {
                model.PedidoCaminho(id);
            }
            return RedirectToAction("readPedidos", new { id = pizzaria.Id });
        }

        public ActionResult PedidoEntregue(int id)
        {
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            using (var model = new PizzariaModel())
            {
                model.PedidoEntregue(id);
            }
            return RedirectToAction("readPedidos", new { id = pizzaria.Id });
        }
        public ActionResult PedidoRecusado(int id)
        {
            Pizzaria pizzaria = (Pizzaria)Session["pizzaria"];
            using (var model = new PizzariaModel())
            {
                model.PedidoRecusado(id);
            }
            return RedirectToAction("readPedidos", new { id = pizzaria.Id });
        }

        public ActionResult historico(int id)
        {
            using (var model = new PizzariaModel())
            {
                List<DetalheCompra> detalhes = model.historico(id);
                ViewBag.historico = detalhes;
            }
            return View();
        }
    }
}