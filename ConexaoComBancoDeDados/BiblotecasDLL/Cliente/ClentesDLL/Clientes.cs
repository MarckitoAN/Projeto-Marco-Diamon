using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClentesDLL
{
    public class Clientes
    {
        public string nome { get; set; }
        private string rg;
        private string cpf;
        public string telefone { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string email { get; set; }

        public String Rg { get { return "RG:xxxxxxxxxxx"; } set { this.rg = value; } }
        public String Cpf { get { return "CPF:xxxxxxxxxxx"; } set { this.cpf = value; } }
       
        
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
        }
    }
}
