using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaPizza.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public int id_Cliente{ get; set; }
        public int id_Endereco { get; set; }
        public int Pagamento { get; set; }
        public DateTime dataHora { get; set; }
        public int status { get; set; }


        public string formaPagamento { get; set; }
        public string NomeCliente { get; set; }
        public string StatusPedido { get; set; }


        //Resumo Carrinho
        public string CEP { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }

        public string NomeProduto { get; set; }
        public int QTD { get; set; }
        public decimal preco { get; set; }


        public decimal Total
        {
            get
            {
                return QTD * preco;
            }
        }

    }
}