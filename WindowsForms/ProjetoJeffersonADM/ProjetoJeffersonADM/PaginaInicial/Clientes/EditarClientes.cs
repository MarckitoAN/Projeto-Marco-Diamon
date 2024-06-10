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

    public partial class EditarClientes : Form
    {
        public EditarClientes(string id, string nome, string rg, string cpf, string telefone, string rua, string bairro, string cidade, string estado, string email)
        {
            InitializeComponent();
            idCliente_txt.Text = id;
            nomeCli_txt.Text = nome;
            rgcli_txt.Text = rg;
            cpfCli_txt.Text = cpf;
            telCli_txt.Text = telefone;
            ruaCli_txt.Text = rua;
            bairroCli_txt.Text = bairro;
            cidadeCli_txt.Text = cidade;
            estadoCli_txt.Text = estado;
            emailCli_txt.Text = email;
        }

        private void EditarClientes_Load(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void corProd_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void idCliente_label_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void marcaProd_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void tamanhoProd_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            int IdConvertido = int.Parse(idCliente_txt.Text);

            try
            {

                Dao.AtualizarCliente(IdConvertido, nomeCli_txt.Text, rgcli_txt.Text, cpfCli_txt.Text, telCli_txt.Text, ruaCli_txt.Text, bairroCli_txt.Text, cidadeCli_txt.Text, estadoCli_txt.Text, emailCli_txt.Text);
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
