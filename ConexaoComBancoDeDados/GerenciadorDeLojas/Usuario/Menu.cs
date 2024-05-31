using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;
using Estoque;

namespace GerenciadorDeLoja
{
    public static class Menu
    {

        public static string MenuCadastro()
        {
                string op = "s";
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("\t\t\t\t\tBEM VINDO AO NOSSO GERENCIADOR DE LOJA");
                Console.WriteLine("1-Faca seu Cadastro");
                Console.WriteLine("2-Fazer Login.");
                Console.WriteLine("3-Sair do Software.");
                op = Console.ReadLine();
                return op;
            }


            public static string MenuLogado()
            {

            string op = "s";
            Console.Clear();
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("\t\t\t\t\tGerenciamento de Produtos");
            Console.WriteLine("1-Produto");
            Console.WriteLine("'Q'-Sair do Software.");
            op = Console.ReadLine();
            return op;
            }
        }
    }
