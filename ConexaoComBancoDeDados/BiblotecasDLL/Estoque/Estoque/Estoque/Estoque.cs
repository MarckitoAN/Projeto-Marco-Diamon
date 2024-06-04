using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace Estoque
{
    public class Estoques
    {
        public int quantidade { get; set; }
        public DateTime dataDeEntrada { get ; set ; }
        public DateTime dataDeSaida { get; set; }


        public void ExibirEstoque()
        {
            Dao.ConectarBancoDeDados();
            Dao.LeitorDeDados("select id_produto,produto.nome,quantidade,data_entrada from Estoque join Produto on Produto.id = estoque.id_produto", ProcessarDadosEstoque);
        }

        public void ProcessarDadosEstoque(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                Console.WriteLine($"\nID do Produto:{reader["id_produto"]}");
                Console.WriteLine($"Nome do Produto:{reader["nome"]}");
                Console.WriteLine($"Quantidade em Estoque:{reader["quantidade"]}");
                Console.WriteLine($"Data de Entrada do Produto no Estoque:{reader["data_entrada"]}");
            }else
            {
                Console.WriteLine("Nao ha colunas pra exibir.");
            }
        }


        public void AdicionarAoEstoque()
        {
            int idObtido = Dao.PegarUltimoID();
            string opps = "";
            Dao.DefinirComandoSql("INSERT INTO Estoque (id_produto, quantidade, data_entrada, data_saida, motivo_saida) VALUES (@id_produto, @quantidade, @data_entrada, @data_saida, @motivo_saida)");
            Dao.AdicionarDados("@id_produto", idObtido);
            Dao.AdicionarDados("@quantidade", quantidade);
            Dao.AdicionarDados("@data_entrada",dataDeEntrada);
            Dao.AdicionarDados("@data_saida", dataDeSaida);
            Dao.AdicionarDados("@motivo_saida", opps);
            Dao.VerificarLinhasAfetadas();
        }

    }
}
