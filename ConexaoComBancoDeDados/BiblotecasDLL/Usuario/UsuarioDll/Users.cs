using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public string Senha { get { return cripHash.CriptografarSenha(senha); } set { senha = cripHash.CriptografarSenha(value); } }



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
                Console.WriteLine("CNPJ invalido digite novamente:");
                this.cnpj = Console.ReadLine();

            }

            Console.WriteLine("Digite sua Senha:");
            Senha = Console.ReadLine();
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

            Dao.DefinirComandoSql("insert into User(nome_loja, contato, email, cnpj, senha_hash) values(@nome_loja, @contato, @email, @cnpj, @senha_hash)");
            Dao.AdicionarDados("@nome_loja", this.nomeDaLoja);
            Dao.AdicionarDados("@contato", this.contato);
            Dao.AdicionarDados("@email", this.email);
            Dao.AdicionarDados("@cnpj", this.cnpj);
            Dao.AdicionarDados("@senha_hash", this.Senha);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Dao.VerificarLinhasAfetadas();
            Console.ReadKey();
            Dao.FecharConexao();
        }
    }
}
