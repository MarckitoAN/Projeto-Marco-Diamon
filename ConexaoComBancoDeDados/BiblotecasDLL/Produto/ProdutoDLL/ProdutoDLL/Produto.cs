using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;
using Estoque;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ProdutoDLL
{
    public class Produto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public double Preco { get; private set; }
        public string Tipo { get; set; }
        public TamanhoCamiseta tamanho { get; set; }
        public string cor { get; set; }
        public Estoques estoque { get; set; }
        public int quantidadeEmEstoque { get; set; }

        public enum TamanhoCamiseta
        {
            Pequeno,
            Medio,
            Grande,
            MuitoGrande,
            G2,
            G3,
            G4,
            G5,
        }


        public void SetPreco(double novoPreco)
        {
            if (novoPreco >= 0)
            {
                Preco = novoPreco;
            }
            else
            {
                Console.WriteLine("Preco Invalido,Tente Novamente;");
                while (novoPreco < 0)
                {
                    Console.WriteLine("Valor:");
                    novoPreco = double.Parse(Console.ReadLine());
                }
                Preco = novoPreco;
            }
        }



        public void CadastroDeProdutos()
        {
            Console.WriteLine("Digite o Nome do Produto");
            this.Nome = Console.ReadLine();
            Console.WriteLine("Descreva seu Produto:");
            this.Descricao = Console.ReadLine();
            Console.WriteLine("Qual a Marca do Produto:");
            this.Marca = Console.ReadLine();
            Console.WriteLine("Preco do Produto:");
            double novoPreco = double.Parse(Console.ReadLine());
            SetPreco(novoPreco);
            Console.WriteLine("Tipo do Produto:");
            this.Tipo = Console.ReadLine();
            Console.WriteLine("Qual o Tamanho da Camiseta");
            Console.WriteLine("Pequena (P)- 0");
            Console.WriteLine("Media (M)- 1");
            Console.WriteLine("Grande (G)- 2");
            Console.WriteLine("ExtraGrande (GG) - 3");
            Console.WriteLine("G2 - 4");
            Console.WriteLine("G3 - 5");
            Console.WriteLine("G4 - 6");
            Console.WriteLine("G5 - 7");
            int tipos = int.Parse(Console.ReadLine());

            switch (tipos)
            {
                case 0: this.tamanho = TamanhoCamiseta.Pequeno; break;
                case 1: this.tamanho = TamanhoCamiseta.Medio; break;
                case 2: this.tamanho = TamanhoCamiseta.Grande; break;
                case 3: this.tamanho = TamanhoCamiseta.MuitoGrande; break;
                case 4: this.tamanho = TamanhoCamiseta.G2; break;
                case 5: this.tamanho = TamanhoCamiseta.G3; break;
                case 6: this.tamanho = TamanhoCamiseta.G4; break;
                case 7: this.tamanho = TamanhoCamiseta.G5; break;
            }

            Console.WriteLine("Cor do Produto:");
            this.cor = Console.ReadLine();

            Console.WriteLine("Qual a Quantidade em Estoque:");
            quantidadeEmEstoque = int.Parse(Console.ReadLine());
            estoque = new Estoques
            {
                quantidade = quantidadeEmEstoque,
                dataDeEntrada = DateTime.Today,
                dataDeSaida = DateTime.Today
            };

        }




        public void AdicionarProdutos()
        {
            try
            {

                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql("insert into Produto (nome,descricao,marca,preco,tipo,tamanho,cor) values (@nome,@descricao,@marca,@preco,@tipo,@tamanho,@cor)");
                Dao.AdicionarDados("@nome", this.Nome);
                Dao.AdicionarDados("@descricao", this.Descricao);
                Dao.AdicionarDados("@marca", this.Marca);
                Dao.AdicionarDados("@preco", this.Preco);
                Dao.AdicionarDados("@tipo", this.Tipo);
                Dao.AdicionarDados("@tamanho", this.tamanho.ToString());
                Dao.AdicionarDados("@cor", this.cor);
                Dao.VerificarLinhasAfetadas();
                estoque.AdicionarAoEstoque();


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

        public void ExibirProdutos()
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.LeitorDeDados("Select *from Produto", ProcessarDadosProdutos);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ProcessarDadosProdutos(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {

                Console.WriteLine($"\nID Do Produto:{reader["id"]}");
                Console.WriteLine($"Nome Do Produto:{reader["nome"]}");
                Console.WriteLine($"Descricao Do Produto:{reader["descricao"]}");
                Console.WriteLine($"Marca Do Produto:{reader["marca"]}");
                Console.WriteLine($"Preco Do Produto:R${reader["preco"]}");
                Console.WriteLine($"Tipo Do Produto:{reader["tipo"]}");
                Console.WriteLine($"Tamanho Do Produto:{reader["tamanho"]}");
                Console.WriteLine($"Cor Do Produto:{reader["cor"]}");
            }
            else
            {
                Console.WriteLine("Nao existem colunas.");
            }
        }


        public void RemoverPedido()
        {
            try
            {

                Dao.ConectarBancoDeDados();
                ExibirProdutos();
                Console.WriteLine("\nInsira o Id do Produto que deseja Remover:");
                int idProduto = int.Parse(Console.ReadLine());

                Dao.DefinirComandoSql($"delete FROM Estoque WHERE id_produto = {idProduto}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete FROM Produto WHERE id = {idProduto}");
                Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }
       /* public void AtualizarProduto()
        {
            try
            {
                Dao.ConectarBancoDeDados();
                ExibirProdutos();
                Console.WriteLine("\nInsira o Id do Produto que deseja Remover:");
                int idProduto = int.Parse(Console.ReadLine());

                Dao.DefinirComandoSql("UPDATE Produto SET nome = @nome, descricao = @descricao, marca = @marca, preco = @preco, tipo = @tipo, tamanho = @tamanho, cor = @cor WHERE id = @id");
                Dao.AdicionarDados("@id", idProduto);
                Dao.AdicionarDados("@nome", this.Nome);
                Dao.AdicionarDados("@descricao", this.Descricao);
                Dao.AdicionarDados("@marca", this.Marca);
                Dao.AdicionarDados("@preco", this.Preco);
                Dao.AdicionarDados("@tipo", this.Tipo);
                Dao.AdicionarDados("@tamanho", this.tamanho.ToString());
                Dao.AdicionarDados("@cor", this.cor);
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
       */
        
    }

}

