using Fabricante;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoJeffersonADM.PaginaInicial.Fornecedor
{
    public partial class AdicionarFornecedor : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse
    );

        Fabricantes fabricantes;
        public AdicionarFornecedor()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }

        private void login_button_Click(object sender, EventArgs e)
        {
            fabricantes = new Fabricantes(nomeFornecedor_txt.Text,ruaFornecedor_txt.Text,bairroFornecedor_txt.Text,cidadeFornecedor_txt.Text,estadoFornecedor_txt.Text,emailFornecedor_txt.Text,cnpjFornecedor_txt.Text);
            fabricantes.AdicionarFabricante();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AdicionarFornecedor_Load(object sender, EventArgs e)
        {

        }
    }
}
