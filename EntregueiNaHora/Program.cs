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
            string command;
            MySqlCommand cmd;
            string connection = "server=localhost;port=3306;Database=GerenciamentoDeLojasADM;uid=root;";
            MySqlConnection conn = new MySqlConnection(connection);


            try
            {
                conn.Open();
                {
                    using(Users users = new Users())
                    { 
                    users.CadatroUsuario();
                    if (conn.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Conexao Aberta com Sucesso:");
                    }

                    command = "insert into User (nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)";
                    cmd = new MySqlCommand(command, conn);

                    cmd.Parameters.AddWithValue("@nome_loja", users.nomeDaLoja);
                    cmd.Parameters.AddWithValue("@contato", users.contato);
                    cmd.Parameters.AddWithValue("@email", users.email);
                    cmd.Parameters.AddWithValue("@cnpj", users.cnpj);
                    cmd.Parameters.AddWithValue("@senha_hash", users.Senha);
                    int linhasafetadas = cmd.ExecuteNonQuery();

                    if (linhasafetadas == 0)
                    {
                        throw new Exception("Nenhuma linha foi afetada pela consulta.");
                    }
                    else
                    {
                        Console.WriteLine("Linhas afetadas:{0}", linhasafetadas);
                    }
                }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
