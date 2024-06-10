using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuario;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using Validacao;
using System.Threading;
using ProjetoJeffersonADM.Logins;
using Mysqlx;
using Bunifu.UI.WinForms;

namespace ProjetoJeffersonADM
{
    public partial class Registro : Form
    {
        private PictureBox pictureBox1;
        public Registro()
        {
            
            InitializeComponent();
        }

        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private const int SombraDaJanela = 0x0002000;

        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = SombraDaJanela;
                return cp;
            }

        }

       
        public void cnpj_field_TextChanged_1(object sender, EventArgs e)
        {

            FormatarCNPJ();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void contato_field_TextChanged(object sender, EventArgs e)
        {
            FormatarTelefone();
        }

        private void register_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (String.IsNullOrEmpty(nomeLoja_field.Text))
                {
                    bunifuSnackbar3.Show(this, "O Nome da Loja vazio.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }



                if (password_field.Text.Length < 8 || String.IsNullOrEmpty(password_field.Text))
                {
                    bunifuSnackbar1.Show(this, "A senha deve conter pelo menos 8 caracteres.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }


                if (String.IsNullOrEmpty(contato_field.Text))
                {
                    bunifuSnackbar5.Show(this, "Contato da Loja vazio.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);

                }

                if (String.IsNullOrEmpty(email_field.Text))
                {
                    bunifuSnackbar4.Show(this, "Email da Loja vazio.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);

                }

                if (!ValidaCNPJ.IsCnpj(cnpj_field.Text) || String.IsNullOrEmpty(cnpj_field.Text))
                {
                    bunifuSnackbar2.Show(this, "Insira um CNPJ Valido..", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);

                    return;
                }

                Dao.ConectarBancoDeDados();
                Users users = new Users(nomeLoja_field.Text, contato_field.Text, email_field.Text,cnpj_field.Text,password_field.Text);
                users.CadastrarUsuario();
                bunifuSnackbar6.Show(this, "Login Feito com Sucesso..", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Success);
                Thread.Sleep(1000);
                Login login = new Login();
                this.Hide();
                login.ShowDialog();

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

        private void bunifuGradientPanel1_Click(object sender, EventArgs e)
        {

        }

        private void login_button_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
        }



public void FormatarCNPJ()
{
            string text = cnpj_field.Text.Replace(".", "").Replace("/", "").Replace("-", "");
            if (text.Length > 2)
                text = text.Insert(2, ".");
            if (text.Length > 6)
                text = text.Insert(6, ".");
            if (text.Length > 10)
                text = text.Insert(10, "/");
            if (text.Length > 15)
                text = text.Insert(15, "-");
            cnpj_field.Text = text;
            cnpj_field.SelectionStart = cnpj_field.Text.Length;
        }


        public void FormatarTelefone()
        {
            string text = contato_field.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            if (text.Length > 2)
                text = text.Insert(2, "-");
            if (text.Length > 8)
                text = text.Insert(8, "-");


            contato_field.Text = text;
            contato_field.SelectionStart = contato_field.Text.Length;
        }


    }
}