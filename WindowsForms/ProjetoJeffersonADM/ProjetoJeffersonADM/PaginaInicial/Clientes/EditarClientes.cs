using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuario;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ProjetoJeffersonADM
{

    public partial class EditarClientes : Form
    {

        ClientesDLL.Clientes cli;
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
            cli = new ClientesDLL.Clientes(nome, rg, cpf, telefone, rua, bairro, cidade, estado, email, "0");

        }

        private void EditarClientes_Load(object sender, EventArgs e)
        {

        }

 

        private void EditarClientes_Load_1(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
           this.Hide();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            int IdConvertido = int.Parse(idCliente_txt.Text);
            try
            {

                cli.AtualizarCliente(IdConvertido, nomeCli_txt.Text, rgcli_txt.Text, cpfCli_txt.Text, telCli_txt.Text, ruaCli_txt.Text, bairroCli_txt.Text, cidadeCli_txt.Text, estadoCli_txt.Text, emailCli_txt.Text);
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
