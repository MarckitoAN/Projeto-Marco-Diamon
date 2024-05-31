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
        public string Senha
        {
            get { return cripHash.CriptografarSenha(senha); }
            set { senha = value; }
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
            senha = Console.ReadLine(); // Armazene a senha sem hashear aqui
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
                Dao.AdicionarDados("@senha_hash", this.Senha);
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

        /*
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
                string comando = "SELECT * FROM User WHERE email = '@Email' AND senha_hash = '@Senha'";
                Dao.DefinirComandoSql(comando);
                Dao.AdicionarDados("@Email", email);
                Dao.AdicionarDados("@Senha", senha_hash);
                Dao.LeitorDeDados(comando, ProcessarDadosUser);
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
            
                    Console.WriteLine($"Email: {reader["email"]}, Senha:{reader["senha"]}");
                    Console.WriteLine("Login Feito com Sucesso");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Login Incorreto. Tente Novamente.");
                Console.ReadKey();
            }
        }
          
         */
     
        

    }
}
