using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BuscaPizza.Models
{
    public class ProdutoModel : ModelBase
    {
        public void cadProduto(Produto prod)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec cadProduto @id_Cardapio, @Preco, @Nome, @Descricao";

            cmd.Parameters.AddWithValue("@id_Cardapio", prod.id_Cardapio);
            cmd.Parameters.AddWithValue("@Preco", prod.Preco);
            cmd.Parameters.AddWithValue("@Nome", prod.NomeProduto);
            cmd.Parameters.AddWithValue("@Descricao", prod.descricao);

            cmd.ExecuteNonQuery();


        }

        public Produto readCarrinho(int id)
        {

            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec readCarrinho @id";

            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var p = new Produto();
                p.id = (int)reader["id"];
                p.id_Cardapio = (int)reader["id_Cardapios"];
                p.NomeProduto = (string)reader["nomeProduto"];
                p.nomePizzaria = (string)reader["nomePizzaria"];
                p.Preco = (decimal)reader["preco"];
                return p;
            }

            return null;
        }

        public List<Produto> readProduto(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec readProduto @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Produto>();

            while(reader.Read())
            {
                var prod = new Produto();
                prod.id = (int)reader["id"];
                prod.id_Cardapio = (int)reader["id_Cardapios"];
                prod.nomeCardapio = (string)reader["nomeCardapio"];
                prod.NomeProduto = (string)reader["nomeProduto"];
                prod.Preco = (decimal)reader["preco"];
                prod.descricao = (string)reader["descricao"];

                lista.Add(prod);
                
            }

            return lista;
        }
    }
}