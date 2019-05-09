using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuscaPizza.Models;

/// <summary>
/// 
/// cadCliente (GET)    = Mostra a Pagina cadCliente.cshtml e passa a ViewBag
/// cadCliente (POST)   = Puxa oque está no Formulario de cadastro e manda para ModelCliente
/// MinhaConta (GET)    = Mostra a Pagina MinhaConta.cshtml e passa a ViewBag
/// MinhaConta (POST)   = Puxa oque está no Formulario de Update e manda para ModelCliente
/// Login      (POST)   = Faz a autenticação e a sessão do cliente 
/// 
/// </summary>

namespace BuscaPizza.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        [HttpGet]
        public ActionResult cadCliente()
        {
            ViewBag.NaoExiste = false; // Aviso que nao existe 
            return View();
        }
        [HttpPost]
        public ActionResult cadCliente(FormCollection form)
        {
            ViewBag.NaoExiste = false;
            Cliente cliente = new Cliente();
            cliente.email = form["Email"];
            cliente.senha = form["Senha"];
            cliente.telefone = form["Telefone"];
            cliente.CEP = form["CEP"];
            cliente.nome = form["Nome"];

            try
            {
                using (ClienteModel model = new ClienteModel())
                {
                    model.cadCliente(cliente);
                    return View();//Mostrando a  View cadCliente
                }
            }
            catch(Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View();
            }
        }
        [AutorizacaoCliente]
        public ActionResult MinhaConta(int id) // Passando o id por Session
        {
            using (var model = new ClienteModel())
            {
                ViewBag.cliente = model.Read(id);
                return View();
            }
        }
        [AutorizacaoCliente]
        [HttpPost]
        public ActionResult MinhaConta(FormCollection form)
        {
            Cliente cliente = (Cliente)Session["cliente"];
            ViewBag.Cliente = new Cliente();

            int id = int.Parse(form["Id"]);
            string nome = form["Nome"];
            string email = form["Email"];
            string senha = form["Senha"];
            string Telefone = form["Telefone"];

            var c = new Cliente();
            c.id = id;
            c.nome = nome;
            c.email = email;
            c.senha = senha;
            c.telefone = Telefone;
            try
            {
                using (var model = new ClienteModel())
                {
                    model.Update(c);
                }
                return RedirectToAction("MinhaConta", new { cliente.id });
            }
            catch(Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View();
            }
        }
        [AutorizacaoCliente]
        public ActionResult meuEndereco(int id)
        {
            using (var model = new ClienteModel())
            {
                List<Cliente> endereco = model.readEnderecos(id);
                ViewBag.Endereco = endereco;
            }
            return View();
        }
        [AutorizacaoCliente]
        public ActionResult updateEndereco(int id)
        {
            using (var model = new ClienteModel())
            {
                ViewBag.end = model.updateEndereco(id);
            }
            return View();
        }
        [AutorizacaoCliente]
        [HttpPost]
        public ActionResult updateEndereco(FormCollection form)
        {
            ViewBag.end = new Cliente();

            int id_end = int.Parse(form["id_end"]);
            string numero = form["numero"];
            string complemento = form["complemento"];

            var end = new Cliente();

            end.id_endereco = id_end;
            end.numero = numero;
            end.complemento = complemento;

            using (var model = new ClienteModel())
            {
                model.updateEndereco(end);
            }
            return RedirectToAction("Menu", "Pizzaria");
        }
        [AutorizacaoCliente]
        [HttpPost]
        public ActionResult cadEndereco(FormCollection form)
        {
            Cliente end = new Cliente();
            end.id = int.Parse(form["id_cli"]); 
            end.CEP = form["CEP"];
            try
            {
                using (ClienteModel model = new ClienteModel())
                {
                    model.cadEnderecos(end);
                    return RedirectToAction("meuEndereco", new { id = end.id } );
                }
            }
            catch (Exception ex)
            {
                ViewBag.erro = ex.Message;
                using (var model = new ClienteModel())
                {
                    List<Cliente> endereco = model.readEnderecos(end.id);
                    ViewBag.Endereco = endereco;
                }
                return View("meuEndereco");
            }
        }
        [AutorizacaoCliente]
        public ActionResult delEndereco(int id)
        {
            Cliente cliente = (Cliente)Session["cliente"];
            try
            {
                using (var model = new ClienteModel())
                {
                    model.deleteEndereco(id);
                }
                return RedirectToAction("meuEndereco", new {id = cliente.id });
            }
            catch(Exception ex)
            {
                ViewBag.erro = ex.Message;
                return View("meuEndereco");
            }
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string email = form["EmailLog"];
            string senha = form["SenhaLog"];

            using (var model = new ClienteModel())
            {
                Cliente cliente = model.Login(email, senha);
                if (cliente != null)
                {
                    Session["cliente"] = cliente; //nome da Session
                    return RedirectToAction("Menu", "Pizzaria");
                }
                else
                {
                    ViewBag.NaoExiste = true;
                    return View("cadCliente");
                }
            }
        }
        [AutorizacaoCliente]
        public ActionResult deleteCliente(int id)
        {
            try
            {
                using (var model = new ClienteModel())
                {
                    model.deleteCliente(id);
                }
                return RedirectToAction("Home", "BuscaPizza");
            }
            catch (Exception ex)
            {
                ViewBag.erro = ex.Message; 
                return View("MinhaConta");
            }

        }
    }
}