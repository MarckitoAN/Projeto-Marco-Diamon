using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace Pedidos
{
    public class Pedido
    {
        public int id { get; set; }
        public DateTime dataPedido;
        public int idCliente { get; set; }
        public int numerosDeParcelas { get; set; }
        public double valorTotal { get; set; }

        public Pagamentos metodoDePagamento;


        public Pedido()
        {
            dataPedido = DateTime.Now;
        }

        public enum Pagamentos
        {
            CartaoDeCredito,
            CartaoDeDebito,
            Pix,
            Boleto,
        }


        public void CriarPedido()
        {
            Dao.DefinirComandoSql("INSERT INTO Pedidos(data_pedido, id_cliente, numeros_de_parcelas, valor_total, metodo_de_pagamento) VALUES(@data_pedido, @id_cliente, @numeros_de_parcelas, @valor_total, @metodo_de_pagamento)");
            Dao.AdicionarDados("@data_pedido", this.dataPedido);
            Dao.AdicionarDados("@id_cliente", this.idCliente);
            Dao.AdicionarDados("@numeros_de_parcelas", this.numerosDeParcelas);
            Dao.AdicionarDados("@valor_total", this.valorTotal);
            Dao.AdicionarDados("@metodo_de_pagamento", this.metodoDePagamento.ToString());
            Dao.VerificarLinhasAfetadas();
        }
    }
}
