using System;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Usuario
{
    public static class Dao
    {
        private static MySqlConnection conexaoBancoDeDados;
        private static MySqlCommand comandoSql;
        public static MySqlDataReader comandoSqlDataReader;
        private static string stringConexao = "server=localhost;port=3306;Database=GerenciadorLojasPraADM;uid=root;";

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
                if (conexaoBancoDeDados != null && conexaoBancoDeDados.State == ConnectionState.Open)
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

        public static bool LoginUser(string email, string password)
        {
            DefinirComandoSql($"select email,senha_hash from user where senha_hash = '{password}' and email = '{email}'");
            comandoSqlDataReader = comandoSql.ExecuteReader();
            if (comandoSqlDataReader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static int UserID(string email, string password)
        {
            int idUsuario = 0;

            try
            {
                Dao.ConectarBancoDeDados();

                Dao.DefinirComandoSql($"select id from user where senha_hash = '{password}' and email = '{email}'");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    idUsuario = Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Dao.FecharConexao();
            }

            return idUsuario;
        }
        public static int ProdutoID(string nomeProduto)
        {
            int idProduto = 0;

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select id from produto where nome = '{nomeProduto}'");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    idProduto = Convert.ToInt32(resultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Dao.FecharConexao();
            }

            return idProduto;
        }

        public static DataTable ObterProdutos()
        {
            DataTable tabelaProdutos = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "SELECT ID,Nome, Descricao, Marca, Preco, Tipo, Tamanho FROM Produto";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                    adaptador.Fill(tabelaProdutos);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ler produtos: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }

            return tabelaProdutos;
        }

        public static DataTable ObterClientes()
        {
            DataTable tabelaClientes = new DataTable();

            try
            {
                ConectarBancoDeDados();
               string comandoSql = "select Id,Nome, Rg, Cpf, Telefone,Estado,Cidade,Rua,Bairro,Email from cliente";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                adaptador.Fill(tabelaClientes);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler os clientes: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return tabelaClientes;
        }


        public static DataTable ObterEstoque()
        {
            DataTable tabelaEstoque = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "select Estoque.ID as ID_Estoque, Produto.ID as ID_Produto, Produto.nome as Nome, Produto.Marca as Marca, Produto.tipo as Tipo, Estoque.Quantidade, estoque.data_entrada as Entrada  ,estoque.data_saida as Saida from estoque join Produto on estoque.id_produto = Produto.id;";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                adaptador.Fill(tabelaEstoque);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler os estoques: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return tabelaEstoque;
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

        public static DataTable ObterPedidos()
        {
            DataTable tabelaPedidos = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "select Pedido.ID as ID,Cliente.ID as ClienteID,Cliente.nome as Cliente,Produto.Id as ProdutoID,Produto.nome as Produto,Produto.preco as PrecoUnitario,Pedido.forma_pagamento,Pedido.parcelas as Parcelas,Pedido_Produto.quantidade as Quantidade,Pedido.valor_total as ValorTotal from Pedido_Produto join Pedido on Pedido_Produto.id_pedido = Pedido.id join Cliente on Cliente.id = Pedido.id_cliente join Produto on Pedido_Produto.id_produto = Produto.id;";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                adaptador.Fill(tabelaPedidos);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler os estoques: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return tabelaPedidos;
        }


        public static double  AcharPrecoUnitario(int idProduto)
        {
            double PrecoUnitario = 0;

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select preco from produto where id = '{idProduto}' ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    PrecoUnitario = Convert.ToDouble(resultado);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                FecharConexao();
            }

            return PrecoUnitario;
        }


    }
}
