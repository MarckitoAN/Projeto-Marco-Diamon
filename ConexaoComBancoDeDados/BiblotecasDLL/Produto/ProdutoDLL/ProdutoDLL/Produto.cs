using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public enum TamanhoCamiseta
        {
            Pequeno,
            Medio,
            Grande,
            ExtraGrande,

        }
        public enum TamanhoSapato
        {
            Size34 = 34,
            Size35 = 35,
            Size36 = 36,
            Size37 = 37,
            Size38 = 38,
            Size39 = 39,
            Size40 = 40,
            Size41 = 41,
            Size42 = 42,
            Size43 = 43,
            Size44 = 44,
            Size45 = 45,
            Size46 = 46,

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
            Console.WriteLine("Qual o Tamanho do P");
        }

    }

}

