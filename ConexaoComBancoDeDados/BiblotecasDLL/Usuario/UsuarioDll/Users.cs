using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using Validacao;

namespace Usuario
{
    public class Users : IDisposable
    {
        Hash cripHash = new Hash(SHA512.Create());
        public string nomeDaLoja { get; set; }
        public string contato { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }
        private string senha { get; set; }

        public string Senha {set { senha = value; } }

        public Users(string nomeDaLoja, string contato, string email, string cnpj, string senha)
        {
            this.nomeDaLoja = nomeDaLoja;
            this.contato = contato;
            this.email = email;
            this.cnpj = cnpj;
            this.Senha = senha;
        }

        public void GetDados()
        {
            Console.WriteLine("Digite o Nome da Sua Loja");
            this.nomeDaLoja = Console.ReadLine();
            Console.WriteLine("Digite seu Telefone:");
            this.contato = Console.ReadLine();
            Console.WriteLine("Digite seu Email:");
            this.email = Console.ReadLine();
            Console.WriteLine("Digite seu CNPJ");
            this.cnpj = Console.ReadLine();

            while (!ValidaCNPJ.IsCnpj(cnpj))
            {
                Console.WriteLine("CNPJ invalido, digite novamente:");
                this.cnpj = Console.ReadLine();
            }

            Console.WriteLine("Digite sua Senha:");
            senha = Console.ReadLine();
        }

        public void Dispose()
        {
            Console.WriteLine("Excluindo Objeto...");
        }

        public void CadastrarUsuario()
        {
            Dao.ConectarBancoDeDados();
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Inserindo no Banco de Dados:");

            try
            {
                Dao.DefinirComandoSql("INSERT INTO User(nome_loja, contato, email, cnpj, senha_hash) VALUES(@nome_loja, @contato, @email, @cnpj, @senha_hash)");
                Dao.AdicionarDados("@nome_loja", this.nomeDaLoja);
                Dao.AdicionarDados("@contato", this.contato);
                Dao.AdicionarDados("@email", this.email);
                Dao.AdicionarDados("@cnpj", this.cnpj);
                Dao.AdicionarDados("@senha_hash", this.senha);
                Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir no banco de dados: " + ex.Message);
            }
            finally
            {
                Dao.FecharConexao();
            }
        }

        
        public void Login()
        {
            Dao.ConectarBancoDeDados();

            Console.WriteLine("Digite seu Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Digite sua Senha:");
            string senha1 = Console.ReadLine();
            string senha_hash = cripHash.CriptografarSenha(senha1);

            try
            {
                Dao.LeitorDeDados($"SELECT * FROM User WHERE email = '{email}' AND senha_hash = '{senha_hash}'", ProcessarDadosUser);
                Console.ReadKey();



            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao executar o comando de login: " + ex.Message);
            }

        }

        public void ProcessarDadosUser(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                    Console.WriteLine("Login Feito com Sucesso");
                Console.ReadKey();
                 
            }
            else
            {
                Console.WriteLine("Login Incorreto. Tente Novamente.");
                Console.ReadKey();
            }
        }
          
         
     
        

    }
}
