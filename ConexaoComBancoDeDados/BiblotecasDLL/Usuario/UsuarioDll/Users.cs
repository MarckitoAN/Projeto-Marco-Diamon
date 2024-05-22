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
        private string Senha { get { return "*******"; } set { cripHash.CriptografarSenha(value); } }



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
            this.Senha = Console.ReadLine();
        }

        public void Dispose()
        {
            Console.WriteLine("Excluindo Objeto...");
        }


        public void CadastrarUsuario()
        {
            Dao.ConectarBancoDeDados();
            Dao.DefinirComandoSql("insert into Users(nome_loja,contato,email,cnpj,senha_hash) values (@nome_loja,@contato,@email,@cnpj,@senha_hash)");
            Dao.AdicionarDados("@nome_empresa", this.nomeDaLoja);
            Dao.AdicionarDados("@contato", this.contato);
            Dao.AdicionarDados("@email", this.email);
            Dao.AdicionarDados("@cnpj", this.cnpj);
            Dao.AdicionarDados("@senha_hash", this.Senha);
        }
    }
}
