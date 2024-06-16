using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace Fabricante
{
    public class Fabricantes
    {
        public Fabricantes(string nome, string rua, string bairro, string cidade, string estado, string email, string cnpj)
        {
            this.nome = nome;
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.estado = estado;
            this.email = email;
            this.cnpj = cnpj;
        }

        public string nome { get; set; }
        public string rua { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string email { get; set; }
        public string cnpj { get; set; }


        public void AdicionarFabricante()
        {
            try
            {

            Dao.ConectarBancoDeDados();
            Dao.DefinirComandoSql("insert into Fornecedor (nome, rua, bairro, cidade, estado, email, cnpj) VALUES (@nome,@rua,@bairro,@cidade,@estado,@email,@cnpj)");
            Dao.AdicionarDados("@nome", this.nome);
            Dao.AdicionarDados("@rua", this.rua);
            Dao.AdicionarDados("@bairro", this.bairro);
            Dao.AdicionarDados("@cidade", this.cidade);
            Dao.AdicionarDados("@estado", this.estado);
            Dao.AdicionarDados("@email", this.email);
            Dao.AdicionarDados("@cnpj", this.cnpj);
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

        public void AtualizarFornecedor(int id, string nome, string rua, string bairro, string cidade, string estado, string email, string cnpj)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"UPDATE Fornecedor set nome = '{nome}', rua = '{rua}', bairro = '{bairro}', cidade = '{cidade}', estado = '{estado}', email = '{email}', cnpj = '{cnpj}' WHERE id = '{id}'");
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

        public void RemoverFornecedor(int id)
        {
            try
            {
                Dao.ConectarBancoDeDados();
                Dao.DefinirComandoSql($"delete from Produto_Fornecedor where id_fornecedor = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete from Pedido_Produto where id_fornecedor = {id}");
                Dao.VerificarLinhasAfetadas();
                Dao.DefinirComandoSql($"delete from Fornecedor where id = {id}");
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
