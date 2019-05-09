using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BuscaPizza.Models
{
    public class PedidoModel : ModelBase
    {
        public bool Finalizar(Pedido pedido, List<DetalheCompra> carrinho)
        {
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.CommandText = @"INSERT INTO Pedidos VALUES (@clienteId,@Endereco, @data,@formaPagamento, 1); SELECT @@IDENTITY FROM Pedidos";

                cmd.Parameters.AddWithValue("@data", pedido.dataHora);
                cmd.Parameters.AddWithValue("@clienteId", pedido.id_Cliente);
                cmd.Parameters.AddWithValue("@Endereco", pedido.id_Endereco);
                cmd.Parameters.AddWithValue("@formaPagamento", pedido.Pagamento);

                pedido.id = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in carrinho)
                {
                    SqlCommand cmdI = new SqlCommand();
                    cmdI.Connection = connection;
                    cmdI.Transaction = transaction;
                    cmdI.CommandText = @"INSERT INTO detalheCompras VALUES (@pedidoId, @produtoId,@cardapioId, @qtde, @valor)";

                    cmdI.Parameters.AddWithValue("@pedidoId", pedido.id);
                    cmdI.Parameters.AddWithValue("@produtoId", item.id_Produto);
                    cmdI.Parameters.AddWithValue("@cardapioId", item.id_Cardapio);
                    cmdI.Parameters.AddWithValue("@qtde", item.Quantidade);
                    cmdI.Parameters.AddWithValue("@valor", item.preco);

                    cmdI.ExecuteNonQuery();

                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return false;
            }
        }

        public List<Pedido> readPedidos(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "exec readPedidosPizzaria @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Pedido>();

            while(reader.Read())
            {
                var ped = new Pedido();
                ped.id = (int)reader["id"];
                ped.NomeCliente = (string)reader["nome"];
                ped.dataHora = (DateTime)reader["dataHora"];
                ped.formaPagamento = (string)reader["formaPagamento"];
                ped.StatusPedido = (string)reader["status"];

                lista.Add(ped);
            }
            return lista;
        }

        public Pedido resumoCompra(int id_pedido,int id_endereco)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select p.id id_pedido, p.dataHora,
                              case(p.formaPagamento)
                                when 0 then 'credito'
                                when 1 then 'debito' 
                                when 2 then 'Dinheiro' 
                            end formaPagamento,e.id,e.numero_Cep,e.logradouro,e.numero,e.complemento,e.bairro,c.nome,c.sigla_UF
                            from Pedidos p, Enderecos e,Cidades c, CEP ce
                            where p.Endereco = e.id and  e.numero_Cep = ce.numero and c.id = ce.id_cidade and e.id = @id_endereco and p.id = @id_pedido";

            cmd.Parameters.AddWithValue("@id_pedido", id_pedido);
            cmd.Parameters.AddWithValue("@id_endereco", id_endereco);

            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                var pedido = new Pedido();
                pedido.id = (int)reader["id_pedido"];
                pedido.dataHora = (DateTime)reader["dataHora"];
                pedido.formaPagamento = (string)reader["formaPagamento"];
                pedido.id_Endereco = (int)reader["id"];
                pedido.CEP = (string)reader["numero_Cep"];
                pedido.logradouro = (string)reader["logradouro"];
                pedido.bairro = (string)reader["bairro"];
                pedido.numero = (string)reader["numero"];
                pedido.complemento = (string)reader["complemento"];
                pedido.Cidade = (string)reader["nome"];
                pedido.UF = (string)reader["sigla_UF"];

                return pedido;
            }

            return null;
        }

        public List<Pedido> readDetalhePedidosCarrinho(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select p.id id_pedido,prod.nomeProduto,d.qtdVendida,prod.preco
                                from Pedidos p,detalheCompras d,Produtos prod
                                where prod.id = d.id_Produto and p.id = d.id_Pedido and p.id = @id";

            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            var Lista = new List<Pedido>();

            while (reader.Read())
            {
                var pedido = new Pedido();
                pedido.id = (int)reader["id_pedido"];
                pedido.NomeProduto = (string)reader["nomeProduto"];
                pedido.QTD = (int)reader["qtdVendida"];
                pedido.preco = (decimal)reader["preco"];

                Lista.Add(pedido);

            }

            return Lista;
        }

        public Pedido readDetalhePedidos(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select p.dataHora,
                              case(p.formaPagamento)
                                when 0 then 'credito'
                                when 1 then 'debito' 
                                when 2 then 'Dinheiro' 
                            end formaPagamento,e.id,e.numero_Cep,e.logradouro,e.numero,e.complemento,e.bairro,c.nome,c.sigla_UF
                            from Pedidos p, Enderecos e,Cidades c, CEP ce
                            where p.Endereco = e.id and  e.numero_Cep = ce.numero and c.id = ce.id_cidade and p.id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var pedido = new Pedido();
                
                pedido.dataHora = (DateTime)reader["dataHora"];
                pedido.formaPagamento = (string)reader["formaPagamento"];
                pedido.id_Endereco = (int)reader["id"];
                pedido.CEP = (string)reader["numero_Cep"];
                pedido.logradouro = (string)reader["logradouro"];
                pedido.bairro = (string)reader["bairro"];
                pedido.numero = (string)reader["numero"];
                pedido.complemento = (string)reader["complemento"];
                pedido.Cidade = (string)reader["nome"];
                pedido.UF = (string)reader["sigla_UF"];

                return pedido;
            }
            return null;
        }
    }
}