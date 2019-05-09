using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class ClienteModel:ModelBase //Model para a connection
    {
       // Metodo para Cadastrar Clintes
        public void cadCliente(Cliente cliente)
        {
            string add = @"exec cadCliente @email,@senha,@telefone,@CEP,@nome";
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = add;

                cmd.Parameters.AddWithValue("@email", cliente.email);
                cmd.Parameters.AddWithValue("@senha", cliente.senha);
                cmd.Parameters.AddWithValue("@telefone", cliente.telefone);
                cmd.Parameters.AddWithValue("@CEP", cliente.CEP);
                cmd.Parameters.AddWithValue("@nome", cliente.nome);


                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Metodo para fazer o Login
        public Cliente Login(string email, string senha)
        {
            SqlCommand cmd = new SqlCommand();  
            cmd.Connection = connection;
            cmd.CommandText = @"exec v_loginCliente @email, @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                Cliente Pessoa = new Cliente(); 
                Pessoa.id = (int)reader["id"];
                Pessoa.nome = (string)reader["nome"];
                Pessoa.email = (string)reader["email"];

                return Pessoa;
            }
            else
            {
                return null;
            }
        }

        //Metodo para poder Puxar os Valores do Banco para as Textbox do MinhaConta
        public Cliente Read(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "exec v_cliente @id";

            cmd.Parameters.AddWithValue("@id", id); 

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var cliente = new Cliente();
                cliente.id = (int)reader["id"];
                cliente.nome = (string)reader["nome"];
                cliente.email = (string)reader["email"];
                cliente.senha = (string)reader["senha"];
                cliente.CEP = (string)reader["numero_Cep"];
                cliente.telefone = (string)reader["numero"];

                return cliente;
            }
            return null;
        }

        public List<Cliente> readEnderecos(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec readEndereco @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            var lista = new List<Cliente>();

            while(reader.Read())
            {
                var end = new Cliente();
                end.id_endereco = (int)reader["id"];
                end.CEP = (string)reader["numero_Cep"];
                end.logradouro = (string)reader["logradouro"];
                end.numero = (string)reader["numero"];
                end.complemento = (string)reader["complemento"];
                end.bairro = (string)reader["bairro"];
                end.Cidade = (string)reader["nome"];
                end.UF = (string)reader["sigla_UF"];

                lista.Add(end);
            }
            return lista;
        }

        public Cliente updateEndereco(int id)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec updateEndereco @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                var cli = new Cliente();
                cli.id_endereco = (int)reader["id"];
                cli.CEP = (string)reader["numero_Cep"];
                cli.logradouro = (string)reader["logradouro"];
                cli.numero = (string)reader["numero"];
                cli.complemento = (string)reader["complemento"];
                cli.bairro = (string)reader["bairro"];
                cli.Cidade = (string)reader["nome"];
                cli.UF = (string)reader["sigla_UF"];

                return cli;
            }
            return null;
        }

        public void cadEnderecos(Cliente end)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"exec cadEndereco @id_pessoa, @cep";

                cmd.Parameters.AddWithValue("@id_pessoa", end.id);
                cmd.Parameters.AddWithValue("@cep", end.CEP);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteEndereco(int id)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"exec delEndereco @id";

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void updateEndereco(Cliente end)
        {
            var cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"exec altEndereco @id,@numero,@complemento";

            cmd.Parameters.AddWithValue("@id", end.id_endereco);
            cmd.Parameters.AddWithValue("@numero", end.numero);
            cmd.Parameters.AddWithValue("@complemento", end.complemento);

            cmd.ExecuteNonQuery();
        }

        //Metodo para alterar dados do Cliente
        public void Update(Cliente c)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"exec altCliente @id, @email, @senha, @telefone, @nome";

                cmd.Parameters.AddWithValue("@id", c.id);
                cmd.Parameters.AddWithValue("@email", c.email);
                cmd.Parameters.AddWithValue("@senha", c.senha);
                cmd.Parameters.AddWithValue("@telefone", c.telefone);
                cmd.Parameters.AddWithValue("@nome", c.nome);

                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteCliente(int id)
        {
            try
            {
                var cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"exec delCliente @id";

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}