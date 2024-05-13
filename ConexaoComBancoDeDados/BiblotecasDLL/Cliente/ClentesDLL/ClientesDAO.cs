using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClentesDLL
{
    public static class ClientesDAO
    {
        private static MySqlCommand _comandoSQL;
        private static MySqlDataReader _comandoSQLDataReader;
        private static MySqlConnection _conexaoMySQL;
        private static string conexaoMySqlString = "server=localhost;port=3308;Database=GerenciamentoDeLojasADM;uid=root;";



        public static void ConectarComBancoDeDados()
        {
            try
            {

            _conexaoMySQL = new MySqlConnection(conexaoMySqlString);
            _conexaoMySQL.Open();
            }catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void FecharConexao()
        {
            _conexaoMySQL.Close(); 
        }


        public static void ComandoSQL(string comando)
        {
            _comandoSQL = new MySqlCommand(comando, _conexaoMySQL);
        }

        public static void AdicionarDados(string parametro, string valor) {
        
        _comandoSQL.Parameters.AddWithValue(parametro, valor);
        
        }

        public static void VerificarLinhasAfetadas()
        {
            int linhasafetadas = _comandoSQL.ExecuteNonQuery();

            if (linhasafetadas == 0)
            {
                throw new Exception("Nenhuma linha foi afetada pela consulta.");
            }
            else
            {
                Console.WriteLine("Linhas afetadas:{0}", linhasafetadas);
            }
        }

            public static void ListarClientes()
            {
                try
                {
                    _comandoSQL = new MySqlCommand("select *from user", _conexaoMySQL);
                    _comandoSQLDataReader = _comandoSQL.ExecuteReader();

                    while (_comandoSQLDataReader.Read())
                    {
                        Console.WriteLine($"ID do CLiente {_comandoSQLDataReader["id"]}\nNome do Cliente:{_comandoSQLDataReader["nome"]}\nCPF:{_comandoSQLDataReader["cpf"]}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    _comandoSQLDataReader.Close();
                }
            }
        }
    }
    
