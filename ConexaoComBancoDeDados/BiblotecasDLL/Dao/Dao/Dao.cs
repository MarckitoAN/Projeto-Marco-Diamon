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
                Dao.ConectarBancoDeDados();

                Dao.DefinirComandoSql($"select id from produto where nome = '{nomeProduto}' ");
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

        /*
         Funcoes Relacionadas a Tabela Produto
         */
        public static DataTable ObterProdutos()
        {
            DataTable tabelaProdutos = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "SELECT ID,Nome, Descricao, Marca, Preco, Tipo, Tamanho, Cor FROM Produto";
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

        public static void RemoverProdutos(int id)
        {
            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"delete from estoque where id_produto = {id}");
                VerificarLinhasAfetadas();
                DefinirComandoSql($"delete from Produto where id = {id}");
                VerificarLinhasAfetadas();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();

            }
        }

        public static void AtualizarProdutos(int id, string nome, string descricao, string marca, decimal preco, string tipo, string tamanho, string cor)
        {
            try
            {
            ConectarBancoDeDados();
            DefinirComandoSql($"UPDATE Produto SET nome = '{nome}', descricao ='{descricao}', marca = '{marca}',  preco = '{preco}', tipo = '{tipo}', tamanho = '{tamanho}', cor = '{cor}' WHERE id = '{id}'");
            VerificarLinhasAfetadas();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();
            }

        }

        /*
         Funcoes Relacionadas a Tabela Cliente
         */
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

        public static void RemoverClientes(int id)
        {
            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"delete from Cliente where id = {id}");
                VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();

            }
        }

        public static void AtualizarCliente(int id, string nome, string rg, string cpf, string telefone, string rua, string bairro, string cidade,string estado,string email)
        {
            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"UPDATE Cliente SET nome = '{nome}', rg = '{rg}', cpf = '{cpf}', telefone = '{telefone}', rua = '{rua}',bairro = '{bairro}', cidade = '{cidade}', estado = '{estado}', email = '{email}' where id = {id}");
                VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();
            }
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

        public static void RemoverEstoque(int id)
        {
            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"delete from estoque where id_produto = {id}");
                VerificarLinhasAfetadas();
                DefinirComandoSql($"delete from produto where id = {id}");
                VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();

            }
        }

        public static void AtualizarEstoque(int idProduto ,string nome, int quantidade, string tipo, DateTime data, DateTime saida, string marca)
        {
            try
            {
                string dataFormatada = data.ToString("yyyy-MM-dd HH:mm:ss");
                string saidaFormatada = saida.ToString("yyyy-MM-dd HH:mm:ss");
                ConectarBancoDeDados();
                DefinirComandoSql($"UPDATE Estoque JOIN Produto ON Produto.id = Estoque.id_produto SET Estoque.data_entrada = '{dataFormatada}', Estoque.data_saida = '{saidaFormatada}',Estoque.quantidade = {quantidade}, Produto.nome = '{nome}', Produto.tipo = '{tipo}', Produto.Marca = '{marca}' WHERE Estoque.id_produto ='{idProduto}' ;");
                VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
