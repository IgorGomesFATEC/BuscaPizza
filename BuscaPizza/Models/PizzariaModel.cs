using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class PizzariaModel : ModelBase
    {
        //Metodo para o Cadastro de Pizzaria
        public void cadPizzaria(Pizzaria adm)
        {
            string add = "exec cadPizzaria @email,@senha,@telefone,@CEP,@NomePizzaria,@CNPJ";

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = add;

                cmd.Parameters.AddWithValue("@email", adm.Email);
                cmd.Parameters.AddWithValue("@senha", adm.Senha);
                cmd.Parameters.AddWithValue("@telefone", adm.Telefone);
                cmd.Parameters.AddWithValue("@CEP", adm.CEP);
                cmd.Parameters.AddWithValue("@NomePizzaria", adm.NomePizzaria);
                cmd.Parameters.AddWithValue("@CNPJ", adm.CNPJ);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //TODO Login da Pizzaria
        public Pizzaria LoginPizzaria(string email, string senha)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec v_loginPizzaria @email,@senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                var pizzaria = new Pizzaria();
                pizzaria.Id = (int)reader["id"];
                pizzaria.Email = (string)reader["email"];
                pizzaria.NomePizzaria = (string)reader["nomePizzaria"];

                return pizzaria;
            }
            else
            {
                return null;
            }
        }
        //TODO MinhaPizzaria
        public Pizzaria ReadMinhaPizzaria(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec v_pizzaria @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            
            if(reader.Read())
            {
                var pizzaria = new Pizzaria();
                pizzaria.Id = (int)reader["id"];
                pizzaria.NomePizzaria = (string)reader["nomePizzaria"];
                pizzaria.Email = (string)reader["email"];
                pizzaria.Senha = (string)reader["senha"];
                pizzaria.CEP = (string)reader["numero_Cep"];
                pizzaria.Telefone = (string)reader["numero"];
                pizzaria.CNPJ = (string)reader["cnpj"];

                return pizzaria;
            }
            return null;
        }
        public void UpdateMinhaPizzaria(Pizzaria pizzaria)
        {
            //TODO logo da pizzaria
            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"exec altPizzaria @id,@email, @senha, @telefone,@nome,'', @cnpj";

                cmd.Parameters.AddWithValue("@id", pizzaria.Id);
                cmd.Parameters.AddWithValue("@email", pizzaria.Email);
                cmd.Parameters.AddWithValue("@senha", pizzaria.Senha);
                cmd.Parameters.AddWithValue("@telefone", pizzaria.Telefone);
                cmd.Parameters.AddWithValue("@nome", pizzaria.NomePizzaria);
                //cmd.Parameters.AddWithValue("@logo", pizzaria.logo);
                cmd.Parameters.AddWithValue("@cnpj", pizzaria.CNPJ);

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ativarPizzaria(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec ativarPizzaria @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public void desativarPizzaria(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec desativarPizzaria @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public List<Pizzaria> menuCliente()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_MenuCliente";

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Pizzaria>();
            while (reader.Read())
            {
                var piz = new Pizzaria();
                piz.Id = (int)reader["id_Pessoas"];
                piz.logo = (string)reader["logo"];
                piz.NomePizzaria = (string)reader["nomePizzaria"];
                piz.logradouro = (string)reader["logradouro"];
                piz.bairro = (string)reader["bairro"];
                piz.CEP = (string)reader["numero_Cep"];
                piz.numero = (string)reader["numero"];

                lista.Add(piz);
            }
            return lista;
        }

        public void PedidoCaminho(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec PedidoCaminho @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public void PedidoEntregue(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec PedidoEntregue @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public void PedidoRecusado(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec PedidoRecusado @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public List<DetalheCompra> historico(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select d.id_Pedido, prod.nomeProduto, c.nomeCardapio, d.qtdVendida,d.precoVendido,p.dataHora
from detalheCompras d, Pedidos p,Produtos prod, Cardapios c,Pizzarias piz
where d.id_Pedido = p.id and prod.id = d.id_Produto and prod.id_Cardapios = c.id and c.id_Pizzaria = piz.id_Pessoas and piz.id_Pessoas = @id and p.status = 3";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<DetalheCompra>();
            while (reader.Read())
            {
                var det = new DetalheCompra();
                det.id_Pedido= (int)reader["id_Pedido"];
                det.nomeproduto = (string)reader["nomeProduto"];
                det.nomecardapio = (string)reader["nomeCardapio"];
                det.Quantidade = (int)reader["qtdVendida"];
                det.preco = (decimal)reader["precoVendido"];
                det.datahora = (DateTime)reader["dataHora"];
                

                lista.Add(det);
            }
            return lista;
        }
    }
}