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
                Console.ForegroundColor = ConsoleColor.Gray;
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
                string comandoSql = "SELECT Produto.ID as ID,Produto.Nome as Nome, Produto.Descricao as Descricao, Produto.Marca as Marca, Produto.Preco as PrecoVenda, Produto.Tipo as Tipo, Produto.Tamanho as Tamanho, Produto.precoDeCusto as PrecoDeCusto, Fornecedor.id as IdFornecedor, Fornecedor.nome as Fornecedor from Produto_Fornecedor join Produto on Produto_Fornecedor.id_produto = Produto.id join Fornecedor on Fornecedor.id = Produto_Fornecedor.id_fornecedor;";
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
                string comandoSql = "SELECT Estoque.ID AS ID_Estoque,produto.ID AS ID_Produto,produto.nome AS Nome_Produto,produto.marca AS Marca,produto.tipo AS Tipo,produto.precoDeCusto AS Preco_De_Custo,Estoque.Quantidade,Estoque.Data_Entrada AS Entrada,Fornecedor.ID AS ID_Fornecedor,Fornecedor.nome AS Nome_Fornecedor,Fornecedor.rua AS Rua_Fornecedor,Fornecedor.bairro AS Bairro_Fornecedor,Fornecedor.cidade AS Cidade_Fornecedor,Fornecedor.estado AS Estado_Fornecedor,Fornecedor.email AS Email_Fornecedor, Fornecedor.cnpj AS CNPJ_Fornecedor FROM Produto_Fornecedor join Fornecedor  on Fornecedor.id = Produto_Fornecedor.id_fornecedor join Estoque  on Produto_Fornecedor.id_produto = Estoque.id_produto join produto  on Produto_Fornecedor.id_produto = produto.id";
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
                string comandoSql = "select Pedido.ID as ID,Cliente.ID as ClienteID,Cliente.nome as Cliente,Produto.Id as ProdutoID,Produto.nome as Produto,Produto.preco as PrecoUnitario,Fornecedor.id as IDFabricante, Fornecedor.nome as Fabricante, Fornecedor.cnpj as Cnpj,Pedido.forma_pagamento,Pedido.parcelas as Parcelas,Pedido_Produto.quantidade as Quantidade,Pedido.valor_total as ValorTotal from Pedido_Produto join Pedido on Pedido_Produto.id_pedido = Pedido.id join Cliente on Cliente.id = Pedido.id_cliente join Produto on Pedido_Produto.id_produto = Produto.id join Fornecedor on Fornecedor.id = Pedido_Produto.id_fornecedor;";
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


        public static double AcharPrecoUnitario(int idProduto)
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

        public static string AcharCPF(int idCliente)
        {
            string cpf = "";

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select cpf from cliente where id = {idCliente} ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    cpf = Convert.ToString(resultado);
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

            return cpf;
        }

        public static string AcharEndereco(int idCliente)
        {
            string rua = "";

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select rua from cliente where id = {idCliente} ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    rua = Convert.ToString(resultado);
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

            return rua;
        }

        public static string AcharCidade(int idCliente)
        {
            string cidade = "";

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select cidade from cliente where id = {idCliente} ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    cidade = Convert.ToString(resultado);
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

            return cidade;
        }

        public static string AcharTelefone(int idCliente)
        {
            string tel = "";

            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"select telefone from cliente where id = {idCliente} ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    tel = Convert.ToString(resultado);
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

            return tel;
        }


        public static DataTable ObterFornecedor()
        {
            DataTable tabelaFornecedor = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "select Id,Nome,Rua,Bairro,Cidade,Estado,Email,Cnpj from Fornecedor";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                adaptador.Fill(tabelaFornecedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                FecharConexao();
            }

            return tabelaFornecedor;
     
        
        }


        public static int ObterIDFornecedor(string id_produto)
        {
            int idFornecedor = 0;

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql($"select Produto_Fornecedor.id_fornecedor from Produto_Fornecedor where id_produto = {id_produto} and ;");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    idFornecedor = Convert.ToInt32(resultado);
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

            return idFornecedor;
        }

        public static double ValorTotal()
        {
            double valor = 0;

            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql("select sum(Pedido.valor_total) from Pedido_Produto join Pedido on Pedido_Produto.id_pedido = Pedido.id; ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    valor = Convert.ToDouble(resultado);
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

            return valor;
        }

        public static string ProdutoMaisVendido()
        {
            string nomeProduto = "";

            try
            {
                ConectarBancoDeDados();

                DefinirComandoSql("SELECT Produto.nome FROM Pedido_Produto JOIN Produto ON Pedido_Produto.id_produto = Produto.id GROUP BY Produto.nome  ORDER BY COUNT(Pedido_Produto.id_produto) DESC");

                var reader = comandoSql.ExecuteReader();
                    if (reader.Read())
                    {
                        nomeProduto = reader["nome"].ToString();
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

            return nomeProduto;
        }

        public static int NumeroPedido()
        {

            int valor = 0; 
            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql("select count(Pedido_Produto.id_pedido_produto) from Pedido_Produto;");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    valor = Convert.ToInt32(resultado);
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

            return valor;
        }


        public static DataTable UltimosPedidos()
        {
            DataTable dataTable = new DataTable();

            try
            {
                ConectarBancoDeDados();
                string comandoSql = "select Pedido.ID as ID,Cliente.ID as ClienteID,Cliente.nome as Cliente,Produto.Id as ProdutoID,Produto.nome as Produto,Produto.preco as PrecoUnitario,Fornecedor.id as IDFabricante, Fornecedor.nome as Fabricante, Fornecedor.cnpj as Cnpj,Pedido.forma_pagamento,Pedido.parcelas as Parcelas,Pedido_Produto.quantidade as Quantidade,Pedido.valor_total as ValorTotal from Pedido_Produto join Pedido on Pedido_Produto.id_pedido = Pedido.id join Cliente on Cliente.id = Pedido.id_cliente join Produto on Pedido_Produto.id_produto = Produto.id join Fornecedor on Fornecedor.id = Pedido_Produto.id_fornecedor ORDER BY Pedido.data DESC LIMIT 5;";
                MySqlDataAdapter adaptador = new MySqlDataAdapter(comandoSql, conexaoBancoDeDados);
                adaptador.Fill(dataTable);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler os pedido: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return dataTable;
        }

        public static string AcharNomeLoja(int id)
        {
           string nomeLoja = "";

            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"select nome_loja from user where id = {id}; ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    nomeLoja = Convert.ToString(resultado);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler o nome: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return nomeLoja;
        }

        public static string AchaEmail(int id)
        {
            string email = "";

            try
            {
                ConectarBancoDeDados();
                DefinirComandoSql($"select email from user where id = {id}; ");
                object resultado = comandoSql.ExecuteScalar();
                if (resultado != null)
                {
                    email = Convert.ToString(resultado);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao ler o email: " + ex.Message);
            }
            finally
            {
                FecharConexao();
            }
            return email;
        }
    }



}
