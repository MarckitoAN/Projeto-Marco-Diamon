using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Usuario;
using ProdutoDLL;
using ClientesDLL;
using GerenciadorDeLoja;
using Estoque;

namespace GerenciadorDeLojas
{
    public class Program
    {
        static void Main(string[] args)
        {
            Produto produtos = new Produto();
            Users users = new Users();
            Estoques estoques = new Estoques();
            Clientes clientes = new Clientes();

            //string op = Menu.MenuCadastro();
            string op = Menu.MenuLogado();


            while (op.ToLower() != "q") {
            
                switch (op)
                {
                    case "1":
                        users.GetDados();
                        users.CadastrarUsuario();
                        op = Menu.MenuCadastro();
                         
                        /*  produtos.CadastroDeProdutos();
                        produtos.AdicionarProdutos();
                        op = Menu.MenuLogado();
                        */
                        break;


                        case "2":
                        users.Login();
                        /*
                        produtos.ExibirProdutos();
                        op = Menu.MenuLogado();
                        
                        */
                        break;

                        case "3":
                        estoques.ExibirEstoque();
                        break;

                    case "4":
                        clientes.CadastrarCliente();
                        clientes.AdicionarCliente();
                        break;

                    case "5":
                        clientes.ExibirCliente();
                        break;
                }
            
            
            }
        }
    }
}
