using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Usuario
{
    public class Dao
    {
        public MySqlConnection conexaoBancoDeDados;
        private string stringConexao = "server=localhost;port=3306;Database=GerenciamentoDeLojasADM;uid=root;";
        public MySqlCommand comandoSql;
        private string comandoSqlString;

        public void ConectarBancoDeDados()
        {
            try
            {
                conexaoBancoDeDados = new MySqlConnection(stringConexao);
                conexaoBancoDeDados.Open();
                Console.WriteLine("Conexão estabelecida com sucesso!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
         
            }
        }

        public void DefinirComandoSql(string cmd) {

            comandoSql = new MySqlCommand(comandoSqlString, conexaoBancoDeDados);
        
        }


        public void VerificarLinhasAfetadas()
        {
            int linhasafetadas = comandoSql.ExecuteNonQuery();

            if (linhasafetadas == 0)
            {
                throw new Exception("Nenhuma linha foi afetada pela consulta.");
            }
            else
            {
                Console.WriteLine("Linhas afetadas:{0}", linhasafetadas);
            }
        }


        public void FecharConexao()
        {
            conexaoBancoDeDados.Close();
        }
    }
}
