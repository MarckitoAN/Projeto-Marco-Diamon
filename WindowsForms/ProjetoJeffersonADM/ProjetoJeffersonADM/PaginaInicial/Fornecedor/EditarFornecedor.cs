using Fabricante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoJeffersonADM
{
    public partial class EditarFornecedor : Form
    {
        public EditarFornecedor(string id, string nome,string rua,string bairro,string cidade,string estado,string email,string cnpj)
        {
            InitializeComponent();
            idFornecedor_txt.Text = id;
            nomeFornecedor_txt.Text = nome;
            ruaFornecedor_txt.Text= rua;
            bairroFornecedor_txt.Text = bairro;
            cidadeFornecedor_txt.Text =cidade;
            estadoFornecedor_txt.Text = estado;
            emailFornecedor_txt.Text = email;
            cnpjFornecedor_txt.Text = cnpj;
        }

        private void EditarFornecedor_Load(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
           Fabricantes fabricante = new Fabricantes("","","","","","","");

            fabricante.AtualizarFornecedor(int.Parse(idFornecedor_txt.Text), nomeFornecedor_txt.Text, ruaFornecedor_txt.Text, bairroFornecedor_txt.Text, cidadeFornecedor_txt.Text, estadoFornecedor_txt.Text, emailFornecedor_txt.Text, cnpjFornecedor_txt.Text);
            this.Hide();
        }
    }
}
