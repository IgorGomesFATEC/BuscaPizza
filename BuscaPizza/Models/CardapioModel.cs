using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace BuscaPizza.Models
{
    public class CardapioModel : ModelBase
    {
        public List<Cardapio> readCardapio(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec v_Cardapio @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Cardapio>();

            while (reader.Read())
            {
                var cardapio = new Cardapio();
                cardapio.id = (int)reader["id"];
                cardapio.id_Pizzaria = (int)reader["id_Pizzaria"];
                cardapio.nomeCardapio = (string)reader["nomeCardapio"];
                cardapio.status = (int)reader["status"];

                lista.Add(cardapio);
            }

            return lista;
        }

        //TODO Filtragem de pesquisa


        public void updateCardapio(Cardapio cardapio)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec altCardapio @id, @nomeCardapio";

            cmd.Parameters.AddWithValue("@id", cardapio.id);
            cmd.Parameters.AddWithValue("@nomeCardapio", cardapio.nomeCardapio);
            cmd.ExecuteNonQuery();
        }

        public void cadCardapio(Cardapio c)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec cadCardapio @id_Pizzaria, @NomeCardapio";

            cmd.Parameters.AddWithValue("@id_Pizzaria", c.id_Pizzaria);
            cmd.Parameters.AddWithValue("@NomeCardapio", c.nomeCardapio);

            cmd.ExecuteNonQuery();
        }

        public void deleteCardapio(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec delCardapio @id ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public List<Cardapio> clienteCardapio(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec v_CardapioCliente @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Cardapio>();

            while (reader.Read())
            {
                var cardapio = new Cardapio();
                cardapio.id = (int)reader["id"];
                cardapio.id_Pizzaria = (int)reader["id_Pizzaria"];
                cardapio.nomeCardapio = (string)reader["nomeCardapio"];
                cardapio.status = (int)reader["status"];

                lista.Add(cardapio);
            }

            return lista;
        }
    }
}