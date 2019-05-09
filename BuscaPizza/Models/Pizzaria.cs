using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Using para deixar o item Requerido
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    //POCO (Plain Old C# Object)
    public class Pizzaria
    {
        public int Id { get; set; }
        // [Required]Item Requerido
        public String Email { get; set; }

        public String Senha { get; set; }
        public String Telefone { get; set; }

        public String NomePizzaria { get; set; }
 
        public String logo { get; set; }

        public String CNPJ { get; set; }
        public int  Status { get; set; }

        public string CEP { get; set; }
        public int id_endereco { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
    }
}