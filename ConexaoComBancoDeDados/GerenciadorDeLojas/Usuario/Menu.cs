using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace GerenciadorDeLoja
{
    public static class Menu
    {

        public static string MenuCadastro()
        {
            string op = "0";
            bool login = false;
            while (op != "3") {
                Console.Clear();
            Console.WriteLine("1-Faca seu Cadastro no Nosso Gerenciador de Loja.");
            Console.WriteLine("2-Fazer Login.");
            Console.WriteLine("3-Sair do Software.");

                switch (op)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Insira seus Dados:\n");
                        Users usuario = new Users();
                        usuario.CadatroUsuario();
                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                }

            op = Console.In.ReadLine();
            }
            return op;
        }
    }
}
