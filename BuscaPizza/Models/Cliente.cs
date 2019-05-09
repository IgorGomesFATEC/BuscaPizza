using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Using para deixar o item Requerido
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    //POCO (Plain Old C# Object)
    public class Cliente
    {
        public int id           { get; set; }
        //[Required] Item Requerido
        public String email     { get; set; }
       
        public String senha     { get; set; }
        public String telefone  { get; set; }

        public String nome { get; set; }



        public string CEP { get; set; }
        public int id_endereco { get; set; }
        public string logradouro { get; set; }
        public string numero  { get; set; }
        public string complemento  { get; set; }
        public string bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

    }
}