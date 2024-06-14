using ProdutoDLL;
using ProjetoJeffersonADM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuario;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ProjetoJeffersonADM.Logins
{
    public partial class Login : Form
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


        readonly Main maizn = new Main();
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
        }
        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Login_Click(object sender, EventArgs e)
        {
        }


        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
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


        private void login_button_Click(object sender, EventArgs e)
        {
           Pedidos pedidos = new Pedidos();
            pedidos.Show();
            Clientes clientes = new Clientes();
            clientes.Show();
            TelaProdutos opa = new TelaProdutos();
            opa.Show();

            /*
            Estoque estoque = new Estoque();
            estoque.Show();
             if(String.IsNullOrEmpty(loginEmail_txt.Text) )
            {
                bunifuSnackbar2.Show(this, "Campo de Email vazio!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            if (String.IsNullOrEmpty(loginSenha_txt.Text))
            {
                bunifuSnackbar3.Show(this, "Campo de Senha vazio!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }

            Dao.ConectarBancoDeDados();
            if (Dao.LoginUser(loginEmail_txt.Text, loginSenha_txt.Text) == true)
            {
                LoginID.IdUser = Dao.UserID(loginEmail_txt.Text, loginSenha_txt.Text);
                
                bunifuTransition1.HideSync(this);
                main.Show();
                Console.WriteLine(LoginID.IdUser);
            }
            else
            {
                bunifuSnackbar1.Show(this, "Login Incorreto, Tente novamente!", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                return;
            }
            
            Dao.FecharConexao();
             */

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registro registro = new Registro();
            this.Hide();
            registro.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            loginEmail_txt.PasswordChar = '*';
        }
    }
}
