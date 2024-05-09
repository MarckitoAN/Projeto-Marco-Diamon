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
            Dao dao = new Dao();

            try
            {
                dao.ConectarBancoDeDados();
                {
                    using (Users users = new Users())
                    {
                        users.CadatroUsuario();

                        dao.DefinirComandoSql("insert into User (nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)");
                        dao.comandoSql.Parameters.AddWithValue("@nome_loja", users.nomeDaLoja);
                        dao.comandoSql.Parameters.AddWithValue("@contato", users.contato);
                        dao.comandoSql.Parameters.AddWithValue("@email", users.email);
                        dao.comandoSql.Parameters.AddWithValue("@cnpj", users.cnpj);
                        dao.comandoSql.Parameters.AddWithValue("@senha_hash", users.Senha);
                        dao.VerificarLinhasAfetadas();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            dao.FecharConexao();
            Console.ReadKey();
        }
    }
}
