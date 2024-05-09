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

            try
            {
                Dao.ConectarBancoDeDados();
                {
                    using (Users users = new Users())
                    {
                        users.CadatroUsuario();

                        Dao.DefinirComandoSql("insert into User (nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)");
                        Dao.comandoSql.Parameters.AddWithValue("@nome_loja", users.nomeDaLoja);
                        Dao.comandoSql.Parameters.AddWithValue("@contato", users.contato);
                        Dao.comandoSql.Parameters.AddWithValue("@email", users.email);
                        Dao.comandoSql.Parameters.AddWithValue("@cnpj", users.cnpj);
                        Dao.comandoSql.Parameters.AddWithValue("@senha_hash", users.Senha);
                        Dao.VerificarLinhasAfetadas();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Dao.FecharConexao();
            Console.ReadKey();
        }
    }
}
