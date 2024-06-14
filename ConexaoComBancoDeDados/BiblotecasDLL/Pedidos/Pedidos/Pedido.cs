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
        public DateTime dataPedido;
        public int idCliente { get; set; }
        public int idProduto { get; set; }
        public int idUser { get; set; }
        public int numerosDeParcelas { get; set; }
        public int quantidade { get; set; }
        public double valorTotal { get; set; }

        public string metodoDePagamento;


        public Pedido(DateTime dataPedido, int idCliente, int idProduto, int idUser, int numerosDeParcelas, int quantidade, double valorTotal, string metodoDePagamento)
        {
            this.dataPedido = dataPedido;
            this.idCliente = idCliente;
            this.idProduto = idProduto;
            this.idUser = idUser;
            this.numerosDeParcelas = numerosDeParcelas;
            this.quantidade = quantidade;
            this.valorTotal = valorTotal;
            this.metodoDePagamento = metodoDePagamento;

        }

        public void CriarPedido(int User)
        {
            try
            {
                Dao.ConectarBancoDeDados();
            string data = dataPedido.ToString("yyyy-MM-dd HH:mm:ss");
            Dao.DefinirComandoSql("INSERT INTO Pedido(data, id_cliente,id_user,id_produto, parcelas, valor_total, forma_pagamento) VALUES(@data, @id_cliente,@id_user,@id_produto,@parcelas,@valor_total, @forma_pagamento)");
            Dao.AdicionarDados("@data", data);
            Dao.AdicionarDados("@id_cliente", this.idCliente);
            Dao.AdicionarDados("@id_produto", this.idProduto);
            Dao.AdicionarDados("@id_user", User);
            Dao.AdicionarDados("@parcelas", this.numerosDeParcelas);
            Dao.AdicionarDados("@quantidade", this.quantidade);
            Dao.AdicionarDados("@valor_total", this.valorTotal);
            Dao.AdicionarDados("@forma_pagamento", this.metodoDePagamento);
            Dao.VerificarLinhasAfetadas();
            int idPedido = Dao.PegarUltimoID();
            Dao.DefinirComandoSql("INSERT INTO Pedido_Produto(id_pedido, id_user, id_produto, quantidade) VALUES (@id_pedido, @id_user, @id_produto, @quantidade)");
            Dao.AdicionarDados("@id_pedido", idPedido);
            Dao.AdicionarDados("@id_user", User);
            Dao.AdicionarDados("@id_produto", idProduto);
            Dao.AdicionarDados("@quantidade", quantidade);
            Dao.VerificarLinhasAfetadas();
            Dao.DefinirComandoSql($"update Estoque set Quantidade = Quantidade - {quantidade} where id_pedido = {this.idProduto};");
            Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Dao.FecharConexao();
            }

        }

        public void RemoverPedidos(int id, int quantidadePedida, int idProduto)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"delete from Pedido_Produto where id_pedido = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete from pedido where id = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"update Estoque set Quantidade = Quantidade + {quantidadePedida} where id_produto = {idProduto};");
                Dao.VerificarLinhasAfetadas();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dao.FecharConexao();

            }
        }




    }
    }

