using System;
using Estoque;
using Usuario;

namespace ProdutoDLL
{
    public class Produto
    {
        private Estoques estoque;

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public double Preco { get; private set; }
        public string Tipo { get; set; }
        public string tamanho { get; set; }
        public int quantidadeEmEstoque { get; set; }

        public Produto(string nome, string descricao, string marca, double Preco, string tipo, string tamanho, int quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Marca = marca;
            this.Preco = Preco;
            Tipo = tipo;
            this.tamanho = tamanho;
            quantidadeEmEstoque = quantidade;
            estoque = new Estoques
            {
                quantidade = quantidadeEmEstoque,
                dataDeEntrada = DateTime.Now,
                dataDeSaida = DateTime.Now
            };
        }

        public void SetPreco(double novoPreco)
        {
            if (novoPreco >= 0.0)
            {
                Preco = novoPreco;
                return;
            }

            Console.WriteLine("Preco Invalido, Tente Novamente;");
            while (novoPreco < 0.0)
            {
                Console.WriteLine("Valor:");
                novoPreco = double.Parse(Console.ReadLine());
            }

            Preco = novoPreco;
        }

        public void AdicionarProdutos(int idUser)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql("INSERT INTO Produto (id_user, nome, descricao, marca, preco, tipo, tamanho) VALUES (@id_user, @nome, @descricao, @marca, @preco, @tipo, @tamanho)");
                Dao.AdicionarDados("@id_user", idUser);
                Dao.AdicionarDados("@nome", Nome);
                Dao.AdicionarDados("@descricao", Descricao);
                Dao.AdicionarDados("@marca", Marca);
                Dao.AdicionarDados("@preco", Preco);
                Dao.AdicionarDados("@tipo", Tipo);
                Dao.AdicionarDados("@tamanho", tamanho);
                Dao.VerificarLinhasAfetadas();
                int idProduto = Dao.ProdutoID(Nome);
                estoque.AdicionarAoEstoque(idUser,idProduto);
                Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Dao.FecharConexao();
            }
        }

        public  void AtualizarProdutos(int id, string nome, string descricao, string marca, decimal preco, string tipo, string tamanho)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"UPDATE Produto SET nome = '{nome}', descricao ='{descricao}', marca = '{marca}',  preco = '{preco}', tipo = '{tipo}', tamanho = '{tamanho}' WHERE id = '{id}'");
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

        public  void RemoverProdutos(int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"delete from estoque where id_produto = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete from Produto where id = {id}");
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
