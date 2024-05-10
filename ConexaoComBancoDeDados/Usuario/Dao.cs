﻿using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Usuario
{
    public static class Dao
    {
        public static MySqlConnection conexaoBancoDeDados;
        private static  string stringConexao = "server=localhost;port=3306;Database=GerenciamentoDeLojasADM;uid=root;";
        public static MySqlCommand comandoSql;

        public static void ConectarBancoDeDados()
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

        public static void DefinirComandoSql(string comandoSqlString) {
            comandoSql = new MySqlCommand(comandoSqlString, conexaoBancoDeDados);
        }


        public static void VerificarLinhasAfetadas()
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


        public static void FecharConexao()
        {
            conexaoBancoDeDados.Close();
        }

        public static void AdicionarDados(string parametro, string valor)
        {
            try
            {
            comandoSql.Parameters.AddWithValue(parametro, valor);
            }
            catch(Exception e)
            { 
                Console.WriteLine(e.Message); 
            }
        }
    }
}
