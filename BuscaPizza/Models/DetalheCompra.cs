using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class DetalheCompra
    {
        public int id { get; set; }
        public int id_Produto { get; set; }
        public int id_Cardapio { get; set; }
        public int id_Pedido { get; set; }
        public int Quantidade { get; set; }
        public decimal preco { get; set; }


        
        public string nomePizzaria { get; set; }
        public string nomeproduto { get; set; }
        public DateTime datahora { get; set; }
        public string nomecardapio { get; set; }

        public decimal Total
        {
            get
            {
                return Quantidade * preco;
            }
        }
    }
}