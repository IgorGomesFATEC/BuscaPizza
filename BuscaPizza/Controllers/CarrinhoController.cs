using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaPizza.Models;

namespace BuscaPizza.Controllers
{
    [AutorizacaoCliente]
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Carrinho()
        {
            List<DetalheCompra> carrinho = Session["carrinho"] as List<DetalheCompra>;

            if (carrinho == null)
            {
                carrinho = new List<DetalheCompra>();
            }
            return View(carrinho);
        }
        public ActionResult Add(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                Produto p = model.readCarrinho(id);

                if (p != null)
                {
                    List<DetalheCompra> carrinho = Session["carrinho"] as List<DetalheCompra>;

                    if (carrinho == null)
                    {
                        carrinho = new List<DetalheCompra>();
                    }

                    DetalheCompra item = carrinho.SingleOrDefault(i => i.id_Produto == id);

                    if (item == null)
                    {
                        item = new DetalheCompra();
                        item.id_Produto = id;
                        item.id_Cardapio = p.id_Cardapio;
                        item.nomeproduto = p.NomeProduto;
                        item.nomePizzaria = p.nomePizzaria;
                        item.preco = p.Preco;
                        item.Quantidade = 1;

                        carrinho.Add(item);
                    }
                    else
                    {
                        item.Quantidade++;
                    }

                    Session["carrinho"] = carrinho;
                }
            }

            return RedirectToAction("carrinho");
        }

        public ActionResult Endereco(int id)
        {
            using (var model = new ClienteModel())
            {
                List<Cliente> endereco = model.readEnderecos(id);
                ViewBag.Endereco = endereco;
            }
            return View();
        }

        public ActionResult Pagamento(int id)
        {
            Session["endereco"] = id;
            return View();
        }

        public ActionResult FinalizarPagamento(int id)
        {
            Pedido pedido = new Pedido();
            pedido.dataHora = DateTime.Now;

            Cliente cliente = (Cliente)Session["cliente"];
            pedido.id_Cliente = cliente.id;

            pedido.id_Endereco = (int)Session["endereco"];

            pedido.Pagamento = id;

            var carrinho = Session["carrinho"] as List<DetalheCompra>;


            if (carrinho != null)
            {
                using (PedidoModel model = new PedidoModel())
                {
                    if (model.Finalizar(pedido, carrinho))
                    {
                        ViewBag.Erro = false;
                        return RedirectToAction("resumoCompra",new { id_pedido = pedido.id , id_endereco = pedido.id_Endereco });
                    }
                    else
                    {
                        ViewBag.Erro = true;
                        return RedirectToAction("carrinho");
                    }
                }
            }
            else
            {
                ViewBag.Nulo = true;
                return RedirectToAction("carrinho");
            }

        }
        public ActionResult resumoCompra(int id_pedido, int id_endereco)
        {
            var carrinho = Session["carrinho"] as List<DetalheCompra>;
            if(carrinho!=null)
            {
                using (var model = new PedidoModel())
                {
                    ViewBag.resumo = model.resumoCompra(id_pedido, id_endereco);
                    ViewBag.Compra = true;
                }
            }
            else
            {   
                ViewBag.Nulo = true;
            }
            return View(carrinho);
        }

        public ActionResult voltar()
        {
            Session.Remove("carrinho");
            Session.Remove("endereco"); 
            return RedirectToAction("Menu", "Pizzaria");
        }
    }
}