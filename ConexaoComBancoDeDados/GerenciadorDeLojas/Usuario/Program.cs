using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Usuario;
using ClientesDLL;
using GerenciadorDeLoja;

namespace GerenciadorDeLojas
{
    public class Program
    {
        static void Main(string[] args)
        {

            string op = Menu.MenuCadastro();

            while (op.ToLower() != "q") {
            
                switch (op)
                {
                    case "1":
                        Users users = new Users();
                        users.GetDados();
                        users.CadastrarUsuario();
                        Menu.MenuCadastro();
                        break;


                        case "2":

                        break;
                }
            
            
            }
        }
    }
}
