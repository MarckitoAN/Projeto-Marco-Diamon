using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoDLL
{
    public class Produtos
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Marca { get; set; }
        public double Preco { get; private set; }
        public string Tipo{ get; set; }
        public Tamanho tamanho { get; set; }
        public string cor { get; set; }

        public enum Tamanho
        {
            Pequeno,
            Medio,
            Grande,
            ExtraGrande
        }

        public void SetPreco(double novoPreco)
        {
            if (novoPreco >= 0)
            {
                Preco = novoPreco;
            }
            else
            {
                throw new ArgumentException("O preço nao deve ser um valor negativo.");
            }
        }



    }

    
    
}

