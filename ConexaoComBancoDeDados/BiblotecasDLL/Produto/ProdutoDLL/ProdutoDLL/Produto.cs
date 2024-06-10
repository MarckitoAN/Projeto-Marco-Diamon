﻿using System;
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
        public string cor { get; set; }
        public int quantidadeEmEstoque { get; set; }

        public Produto(string nome, string descricao, string marca, double Preco, string tipo, string tamanho, string cor, int quantidade)
        {
            Nome = nome;
            Descricao = descricao;
            Marca = marca;
            this.Preco = Preco;
            Tipo = tipo;
            this.tamanho = tamanho;
            this.cor = cor;
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

        public void AdicionarProdutos()
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql("INSERT INTO Produto (nome, descricao, marca, preco, tipo, tamanho, cor) VALUES (@nome, @descricao, @marca, @preco, @tipo, @tamanho, @cor)");
                Dao.AdicionarDados("@nome", Nome);
                Dao.AdicionarDados("@descricao", Descricao);
                Dao.AdicionarDados("@marca", Marca);
                Dao.AdicionarDados("@preco", Preco);
                Dao.AdicionarDados("@tipo", Tipo);
                Dao.AdicionarDados("@tamanho", tamanho);
                Dao.AdicionarDados("@cor", cor);
                Dao.VerificarLinhasAfetadas();
                int idProduto = Dao.ProdutoID(Nome);
                estoque.AdicionarAoEstoque(idProduto);
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
    }
}
