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
    }
}
