using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Usuario;
using ClientesDLL;
using ProdutoDLL;
using GerenciadorDeLoja;

namespace GerenciadorDeLojas
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Dao.ConectarBancoDeDados();
            Menu.MenuCadastro();
            Console.ReadKey();
        }
    }
}
