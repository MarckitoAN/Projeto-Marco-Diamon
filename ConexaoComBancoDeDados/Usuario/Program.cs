using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Usuario
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserDao.ConectarBancoDeDados();
            using (Users users = new Users())
            {
                try
                {
                    users.CadatroUsuario();
                    UserDao.DefinirComandoSql("insert into User (nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)");
                    UserDao.AdicionarDados("@nome_loja", users.nomeDaLoja);
                    UserDao.AdicionarDados("@contato", users.contato);
                    UserDao.AdicionarDados("@email", users.email);
                    UserDao.AdicionarDados("@cnpj", users.cnpj);
                    UserDao.AdicionarDados("@senha_hash", users.Senha);
                    UserDao.VerificarLinhasAfetadas();
                    UserDao.ListarUsuarios();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    UserDao.FecharConexao();
                    Console.ReadKey();
                }
            }
        }
    }
}
