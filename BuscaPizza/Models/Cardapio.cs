using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class Cardapio
    {
        public int id { get; set; }
        public int id_Pizzaria { get; set; }
        public string nomeCardapio { get; set; }
        public int status { get; set; }


        public string Pizzaria { get; set; }
    }
}