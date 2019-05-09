using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class Produto
    {
        public int id { get; set; }
        public int id_Cardapio { get; set; }
        public string NomeProduto { get; set; }
        public decimal Preco { get; set; }
        public string descricao { get; set; }
        public string foto { get; set; }


        public string nomeCardapio { get; set; }
        public string nomePizzaria { get; set; }
    }
}