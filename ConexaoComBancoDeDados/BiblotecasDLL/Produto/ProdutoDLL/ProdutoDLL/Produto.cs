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
        public double Preco { get;  set; }
        public string Tipo { get; set; }
        public string tamanho { get; set; }
        public int quantidadeEmEstoque { get; set; }
        public double precoDeCusto { get; set; }
        public int IdFornecedor { get; set; }
        public byte[] Imagem { get; set; }

        public Produto(string nome, string descricao, string marca, double Preco, string tipo, string tamanho, int quantidade, int idfornecedor, double precoDeCusto, byte[] imagem)
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
            this.IdFornecedor = idfornecedor;
            this.precoDeCusto = precoDeCusto;
            Imagem = imagem;
        }

        public void AdicionarProdutos(int idUser)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql("INSERT INTO Produto (id_user, nome, descricao, marca, preco, tipo, tamanho, precoDeCusto,imagem) VALUES (@id_user, @nome, @descricao, @marca, @preco, @tipo, @tamanho, @precoDeCusto,@imagem)");
                Dao.AdicionarDados("@id_user", idUser);
                Dao.AdicionarDados("@nome", Nome);
                Dao.AdicionarDados("@descricao", Descricao);
                Dao.AdicionarDados("@marca", Marca);
                Dao.AdicionarDados("@preco", Preco);
                Dao.AdicionarDados("@tipo", Tipo);
                Dao.AdicionarDados("@tamanho", tamanho);
                Dao.AdicionarDados("@precoDeCusto", precoDeCusto);
                Dao.AdicionarDados("@imagem", Imagem);
                Dao.VerificarLinhasAfetadas();
                int idProduto = Dao.ProdutoID(Nome);
                estoque.AdicionarAoEstoque(idUser, idProduto);
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql("INSERT INTO Produto_Fornecedor (id_produto, id_fornecedor) VALUES (@id_produto, @id_fornecedor)");
                Dao.AdicionarDados("@id_produto", idProduto);
                Dao.AdicionarDados("@id_fornecedor", IdFornecedor);
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

        public void AtualizarProdutos(int id, int idFornecedor, string nome, string descricao, string marca, decimal preco, string tipo, string tamanho, double precoDeCusto)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"UPDATE Produto JOIN Produto_Fornecedor ON Produto.id = Produto_Fornecedor.id_produto SET nome = @nome, descricao = @descricao, marca = @marca, preco = @preco, tipo = @tipo, tamanho = @tamanho, precoDeCusto = @precoDeCusto WHERE Produto.id = @id AND id_fornecedor = @idFornecedor;");
                Dao.AdicionarDados("@id", id);
                Dao.AdicionarDados("@idFornecedor", idFornecedor);
                Dao.AdicionarDados("@nome", nome);
                Dao.AdicionarDados("@descricao", descricao);
                Dao.AdicionarDados("@marca", marca);
                Dao.AdicionarDados("@preco", preco);
                Dao.AdicionarDados("@tipo", tipo);
                Dao.AdicionarDados("@tamanho", tamanho);
                Dao.AdicionarDados("@precoDeCusto", precoDeCusto);
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

        public void RemoverProdutos(int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"DELETE FROM Produto_Fornecedor WHERE id_produto = @id");
                Dao.AdicionarDados("@id", id);
                Dao.VerificarLinhasAfetadas();

                Dao.DefinirComandoSql($"DELETE FROM Estoque WHERE id_produto = @id");
                Dao.AdicionarDados("@id", id);
                Dao.VerificarLinhasAfetadas();

                Dao.DefinirComandoSql($"DELETE FROM Produto WHERE id = @id");
                Dao.AdicionarDados("@id", id);
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
