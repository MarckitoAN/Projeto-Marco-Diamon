using System;
using System.Data;
using MySql.Data.MySqlClient;
using Usuario;

namespace Estoque
{
    public class Estoques
    {
        public int quantidade { get; set; }
        public DateTime dataDeEntrada { get; set; }
        public DateTime dataDeSaida { get; set; }

        public void AdicionarAoEstoque(int idUser,int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                string valor = "Entrada";
                Dao.DefinirComandoSql("INSERT INTO Estoque (id_user,id_produto, quantidade, data_entrada, data_saida, motivo_saida) VALUES (@id_user,@id_produto, @quantidade, @data_entrada, @data_saida, @motivo_saida)");
                Dao.AdicionarDados("@id_user", idUser);
                Dao.AdicionarDados("@id_produto", id);
                Dao.AdicionarDados("@quantidade", quantidade);
                Dao.AdicionarDados("@data_entrada", dataDeEntrada);
                Dao.AdicionarDados("@data_saida", dataDeSaida);
                Dao.AdicionarDados("@motivo_saida", valor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }

        public  void RemoverEstoque(int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"delete from estoque where id_produto = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete from produto where id = {id}");
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

        public  void AtualizarEstoque(int idProduto, string nome, int quantidade, string tipo, DateTime data, DateTime saida, string marca)
        {
            try
            {
                string dataFormatada = data.ToString("yyyy-MM-dd HH:mm:ss");
                string saidaFormatada = saida.ToString("yyyy-MM-dd HH:mm:ss");
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"UPDATE Estoque JOIN Produto ON Produto.id = Estoque.id_produto SET Estoque.data_entrada = '{dataFormatada}', Estoque.data_saida = '{saidaFormatada}',Estoque.quantidade = {quantidade}, Produto.nome = '{nome}', Produto.tipo = '{tipo}', Produto.Marca = '{marca}' WHERE Estoque.id_produto ='{idProduto}' ;");
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
