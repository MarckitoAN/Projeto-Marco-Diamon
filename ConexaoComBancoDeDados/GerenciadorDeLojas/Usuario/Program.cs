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

namespace GerenciadorDeLojas
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dao.ConectarBancoDeDados();
            using (Users users = new Users())
            {
                try
                {
                    users.CadatroUsuario();
                    Dao.DefinirComandoSql("insert into User (nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)");
                    Dao.AdicionarDados("@nome_loja", users.nomeDaLoja);
                    Dao.AdicionarDados("@contato", users.contato);
                    Dao.AdicionarDados("@email", users.email);
                    Dao.AdicionarDados("@cnpj", users.cnpj);
                    Dao.AdicionarDados("@senha_hash", users.Senha);
                    Dao.VerificarLinhasAfetadas();
                    Dao.ListarDados("select *from user");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Dao.FecharConexao();
                    Console.ReadKey();
                }

                Clientes clientes = new Clientes();
                clientes.CadastrarCliente();
            }
        }
    }
}
