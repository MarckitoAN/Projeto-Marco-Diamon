using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Validacao;

namespace Usuario
{

    public class Users : IDisposable
    {
        Hash cripHash = new Hash(SHA512.Create());
        public string nomeDaLoja { get; set; }
        public string contato { get; set; }
        public string email{ get; set; }
        public string cnpj { get; set; }
        private string senha{ get; set; }

        public string Senha { get { return cripHash.CriptografarSenha(senha); } set => senha = cripHash.CriptografarSenha(value); }



        public void CadatroUsuario()
        {
            Console.WriteLine("Digite o Nome da Sua Loja");
            nomeDaLoja = Console.ReadLine();
            Console.WriteLine("Digite seu Telefone:");
            contato = Console.ReadLine();
            Console.WriteLine("Digite seu Email:");
            email = Console.ReadLine();
            Console.WriteLine("Digite seu CNPJ");
            cnpj = Console.ReadLine();
            while (!ValidaCNPJ.IsCnpj(cnpj))
            {
                Console.WriteLine("CNPJ invalido digite novamente:");
                cnpj = Console.ReadLine();

            }

            Console.WriteLine("Digite sua Senha:");
            Senha = Console.ReadLine();
            cripHash.CriptografarSenha(Senha);
        }

        public void Dispose()
        {
            Console.WriteLine("Excluindo Objeto...");
        }
    }
}
