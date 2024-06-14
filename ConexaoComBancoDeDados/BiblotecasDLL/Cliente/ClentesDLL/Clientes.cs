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

        public Clientes(string nome, string rg, string cpf, string telefone, string rua, string bairro, string cidade, string estado, string email, string senha)
        {
            this.nome = nome;
            this.rg = rg;
            this.cpf = cpf;
            this.telefone = telefone;
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
            this.email = email;
            this.senha = senha;
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


        public  void RemoverClientes(int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"delete from Cliente where id = {id}");
                Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dao.FecharConexao();

            }
        }

        public  void AtualizarCliente(int id, string nome, string rg, string cpf, string telefone, string rua, string bairro, string cidade, string estado, string email)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"UPDATE Cliente SET nome = '{nome}', rg = '{rg}', cpf = '{cpf}', telefone = '{telefone}', rua = '{rua}',bairro = '{bairro}', cidade = '{cidade}', estado = '{estado}', email = '{email}' where id = {id}");
                Dao.VerificarLinhasAfetadas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Dao.FecharConexao();
            }
        }

    }
}
