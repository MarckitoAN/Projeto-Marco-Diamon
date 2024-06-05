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

        private void cnpj_field_TextChanged_1(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void contato_field_TextChanged(object sender, EventArgs e)
        {
            string text = contato_field.Text.Replace(".", "").Replace("/", "").Replace("-", "");

            if (text.Length > 2)
                text = text.Insert(2, "-");
            if (text.Length > 8)
                text = text.Insert(8, "-");


            contato_field.Text = text;
            contato_field.SelectionStart = contato_field.Text.Length;
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            Hash hash = new Hash(SHA512.Create());
            Users users = new Users();

            try
            {
                if (password_field.Text.Length < 8)
                {
                    MessageBox.Show("A senha deve conter pelo menos 8 caracteres.", "Erro de senha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidaCNPJ.IsCnpj(cnpj_field.Text))
                {
                    MessageBox.Show("Insira um CNPJ Valido.", "CNPJ Invalido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Dao.ConectarBancoDeDados();
                users.nomeDaLoja = nomeLoja_field.Text;
                users.cnpj = cnpj_field.Text;
                users.contato = contato_field.Text;
                users.email = email_field.Text;
                users.Senha = hash.CriptografarSenha(password_field.Text);
                users.CadastrarUsuario();
                Thread.Sleep(2000);
                sucess sucesso = new sucess();
                sucesso.ShowDialog();

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
    }
}
