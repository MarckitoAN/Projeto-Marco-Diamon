using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;

namespace Despesas
{
    public class Despesas
    {
        public Despesas(string descricao, DateTime data, double valor, string tipo_despesa)
        {
            Descricao = descricao;
            this.data = data;
            this.valor = valor;
            this.tipo_despesa = tipo_despesa;
        }

        public string Descricao { get; set; }
        public DateTime data{ get; set; }
        public double valor{ get; set; }
        public string tipo_despesa{ get; set; }


    public void AdicionarDespesas()
    {

            string dataFormatada = data.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {

            Dao.ConectarBancoDeDados();
            Dao.DefinirComandoSql("insert into Despesas (descricao,data,valor,tipo_despesa) values (@descricao,@data,@valor,@tipo_despesa)");
            Dao.AdicionarDados("@descricao", this.Descricao);
            Dao.AdicionarDados("@data", dataFormatada);
            Dao.AdicionarDados("@valor", this.valor);
            Dao.AdicionarDados("@tipo_despesa", this.tipo_despesa);
                Dao.VerificarLinhasAfetadas();
            
            }catch (Exception ex)
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
