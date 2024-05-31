using System;
using MySql.Data.MySqlClient;

namespace Usuario
{
    public static class Dao
    {
        private static MySqlConnection conexaoBancoDeDados;
        private static MySqlCommand comandoSql;
        public static MySqlDataReader comandoSqlDataReader;
        private static string stringConexao = "server=localhost;port=3306;Database=GerenciamentoDeLojasADM;uid=root;";

        public static void ConectarBancoDeDados()
        {
            try
            {
                conexaoBancoDeDados = new MySqlConnection(stringConexao);
                conexaoBancoDeDados.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Conexão estabelecida com sucesso!");
                Console.ForegroundColor= ConsoleColor.Gray;
            }
            catch (MySqlException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }

        public static void DefinirComandoSql(string comandoSqlString)
        {
            comandoSql = new MySqlCommand(comandoSqlString, conexaoBancoDeDados);
            comandoSql.Parameters.Clear();
        }

        public static void VerificarLinhasAfetadas()
        {
            try
            {
                int linhasAfetadas = comandoSql.ExecuteNonQuery();
                if (linhasAfetadas == 0)
                {
                    throw new Exception("Nenhuma linha foi afetada pela consulta.");
                }
                else
                {
                    Console.WriteLine("Linhas afetadas: {0}", linhasAfetadas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao verificar linhas afetadas: " + ex.Message);
            }
        }

        public static void FecharConexao()
        {
            try
            {
                if (conexaoBancoDeDados != null && conexaoBancoDeDados.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Fechando Conexao:");
                    conexaoBancoDeDados.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao fechar a conexão: " + ex.Message);
            }
        }

        public static void AdicionarDados(string parametro, object valor)
        {
            try
            {
                comandoSql.Parameters.AddWithValue(parametro, valor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao adicionar dados ao comando SQL: " + ex.Message);
            }
        }

        public static void LeitorDeDados(string comandoSelect, Action<MySqlDataReader> ProcessarDados)
        {
            try
            {
                comandoSql = new MySqlCommand(comandoSelect, conexaoBancoDeDados);
                comandoSqlDataReader = comandoSql.ExecuteReader();

                
                    while (comandoSqlDataReader.Read())
                    {
                        ProcessarDados(comandoSqlDataReader);
                    }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar leitor de dados: " + ex.Message);
            }
            finally
            {
                if (comandoSqlDataReader != null && !comandoSqlDataReader.IsClosed)
                {
                    comandoSqlDataReader.Close();
                }

            }
        }

        public static int PegarUltimoID()
        {
            try
            {
                comandoSql = new MySqlCommand("SELECT LAST_INSERT_ID()", conexaoBancoDeDados); ;
                object resultadosDoID = comandoSql.ExecuteScalar();

                int IdObtido = Convert.ToInt32(resultadosDoID);

                return IdObtido;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao obter o último ID inserido: " + ex.ToString());
                return -1;
            }
        }
    }
}
