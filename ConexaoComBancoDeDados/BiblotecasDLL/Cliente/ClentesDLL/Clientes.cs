using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace ClientesDLL
{
    public class Clientes
    {
        Hash hash = new Hash(SHA512.Create());
        public string nome { get; set; }
        private string rg;
        private string cpf;
        public string telefone { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string email { get; set; }
        private string senha;

        public string Senha
        {
            get { return hash.CriptografarSenha(senha); }
            set { senha = hash.CriptografarSenha(value); }
        }

        private String Rg { set { this.rg = value; } }
        private String Cpf { set { this.cpf = value; } }



        public void CadastrarCliente()
        {
            Console.WriteLine("Digite o Nome do Cliente:");
            this.nome = Console.ReadLine();
            Console.WriteLine("RG do Cliente:");
            Rg = Console.ReadLine();
            Console.WriteLine("CPF do Cliente:");
            Cpf = Console.ReadLine();
            Console.WriteLine("Telfone do Cliente:");
            this.telefone = Console.ReadLine();
            Console.WriteLine("Rua do Cliente:");
            this.rua = Console.ReadLine();
            Console.WriteLine("Bairro do Cliente:");
            this.bairro = Console.ReadLine();
            Console.WriteLine("Cidade do Cliente:");
            this.cidade = Console.ReadLine();
            Console.WriteLine("Estado do Cliente:");
            this.estado = Console.ReadLine();
            Console.WriteLine("Email do Cliente:");
            this.email = Console.ReadLine();
            Console.WriteLine("Senha do Cliente:");
            this.Senha = Console.ReadLine();
        }


        public void AdicionarCliente()
        {
            Dao.ConectarBancoDeDados();
            Dao.DefinirComandoSql("insert into Cliente (nome,rg,cpf,telefone,rua,bairro,cidade,estado,email,senha) values (@nome,@rg,@cpf,@telefone,@rua,@bairro,@cidade,@estado,@email,@senha)");
            Dao.AdicionarDados("@nome", this.nome);
            Dao.AdicionarDados("@rg", this.rg);
            Dao.AdicionarDados("@cpf", this.cpf);
            Dao.AdicionarDados("@telefone", this.telefone);
            Dao.AdicionarDados("@rua", this.rua);
            Dao.AdicionarDados("@bairro", this.bairro);
            Dao.AdicionarDados("@cidade", this.cidade);
            Dao.AdicionarDados("@estado", this.estado);
            Dao.AdicionarDados("@email", this.email);
            Dao.AdicionarDados("@senha", this.senha);
            Dao.VerificarLinhasAfetadas();
            Dao.FecharConexao();
        }

        public void ExibirCliente()
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.LeitorDeDados("select *from Cliente", ProcessarDadosCliente);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Dao.FecharConexao();
            }
        }


        public void ProcessarDadosCliente(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                Console.WriteLine($"\nId do Cliente:{reader["id"]}\n" +
                    $"Nome do Cliente:{reader["nome"]}\n" +
                    $"RG do Cliente:{reader["rg"]} \n" +
                    $"CPF do Cliente:{reader["cpf"]} \n" +
                    $"Telefone do Cliente{reader["telefone"]} \n" +
                    $"Endereco do Cliente:\n" +
                    $"\nRua do Cliente:{reader["rua"]}  \n" +
                    $"Bairro do Cliente:{reader["bairro"]}  \n" +
                    $"Cidade do Cliente:{reader["cidade"]}  \n" +
                    $"Estado do Cliente:{reader["estado"]}  \n" +
                    $"Email do Cliente:{reader["email"]}  \n");

            }
            else
            {
                Console.WriteLine("Nao ha colunas a exibir");
            }
        }
    }
}
