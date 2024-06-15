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

namespace ProjetoJeffersonADM.PaginaInicial.Fornecedor
{
    public partial class AdicionarFornecedor : Form
    {
        Fabricantes fabricantes;
        public AdicionarFornecedor()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            fabricantes = new Fabricantes(nomeFornecedor_txt.Text,ruaFornecedor_txt.Text,bairroFornecedor_txt.Text,cidadeFornecedor_txt.Text,estadoFornecedor_txt.Text,emailFornecedor_txt.Text,cnpjFornecedor_txt.Text);
            fabricantes.AdicionarFabricante();
            this.Hide();
        }
    }
}
