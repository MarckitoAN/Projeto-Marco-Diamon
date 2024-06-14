using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoJeffersonADM.PaginaInicial.Clientes
{
    public partial class AdicionarCliente : Form
    {
        public AdicionarCliente()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            try
            {

            ClientesDLL.Clientes clientes = new ClientesDLL.Clientes(nomeCliente_txt.Text,rgcli_txt.Text,cpfCli_txt.Text,telCli_txt.Text,ruaCli_txt.Text,bairroCli_txt.Text,cidadeCli_txt.Text,estadoCli_txt.Text,emailCli_txt.Text,"indefinido");
            clientes.AdicionarCliente();
                this.Hide();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
