using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class EditarEstoque : Form
    {
        public EditarEstoque(string id, string idProduto, string nome,string marca,string tipo,string entrada,string saida,string quantidade)
        {
            InitializeComponent();
            idEstq_txt.Text = id;
            idProd_txt.Text = idProduto;
            nomeProd_txt.Text = nome;
            marcaProd_txt.Text = marca;
            entrada_txt.Text = entrada;
            saida_txt.Text = saida;
            tipoProd_txt.Text = tipo;
            quantidade_txt.Text = quantidade;

        }

        private void EditarEstoque_Load(object sender, EventArgs e)
        {
            
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            int idProduto = int.Parse(idProd_txt.Text);
            int quantidade = int.Parse(quantidade_txt.Text);
            DateTime entradaEHora = DateTime.Parse(entrada_txt.Text);
            DateTime saidaEHora = DateTime.Parse(saida_txt.Text);
            Dao.AtualizarEstoque(idProduto, nomeProd_txt.Text, quantidade, tipoProd_txt.Text, entradaEHora, saidaEHora, marcaProd_txt.Text);
            this.Hide();

        }
    }
}
