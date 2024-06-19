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

namespace ProjetoJeffersonADM
{
    public partial class EditarFornecedor : Form
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
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }

        private void EditarFornecedor_Load(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
           Fabricantes fabricante = new Fabricantes("","","","","","","");
            if (String.IsNullOrEmpty(nomeFornecedor_txt.Text))
            {
                nome.Show(this, "Nome do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(cnpjFornecedor_txt.Text) || !Validacao.ValidaCNPJ.IsCnpj(cnpjFornecedor_txt.Text))
            {
                nome.Show(this, "Insira um Cnpj valido:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(ruaFornecedor_txt.Text))
            {
                nome.Show(this, "Rua do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(bairroFornecedor_txt.Text))
            {
                nome.Show(this, "Bairro do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(cidadeFornecedor_txt.Text))
            {
                nome.Show(this, "Cidade do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(estadoFornecedor_txt.Text))
            {
                nome.Show(this, "Estado do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            else if (String.IsNullOrEmpty(emailFornecedor_txt.Text))
            {
                nome.Show(this, "E-mail do fornecedor esta vazio:", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }


                fabricante.AtualizarFornecedor(int.Parse(idFornecedor_txt.Text), nomeFornecedor_txt.Text, ruaFornecedor_txt.Text, bairroFornecedor_txt.Text, cidadeFornecedor_txt.Text, estadoFornecedor_txt.Text, emailFornecedor_txt.Text, cnpjFornecedor_txt.Text);
            this.Hide();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cnpjFornecedor_txt_TextChanged(object sender, EventArgs e)
        {
                string text = cnpjFornecedor_txt.Text.Replace(".", "").Replace("/", "").Replace("-", "");
                if (text.Length > 2)
                    text = text.Insert(2, ".");
                if (text.Length > 6)
                    text = text.Insert(6, ".");
                if (text.Length > 10)
                    text = text.Insert(10, "/");
                if (text.Length > 15)
                    text = text.Insert(15, "-");
            cnpjFornecedor_txt.Text = text;
            cnpjFornecedor_txt.SelectionStart = cnpjFornecedor_txt.Text.Length;
            

        }
    }
}
