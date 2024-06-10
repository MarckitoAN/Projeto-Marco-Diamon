using ProdutoDLL;
using ProjetoJeffersonADM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
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


        Main main = new Main();
        public Login()
        {
            InitializeComponent();
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            this.Hide();
            registro.Show();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            TelaProdutos opa = new TelaProdutos();
            opa.Show();
            Estoque estoque = new Estoque();
            estoque.Show();
            /*
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


    }
}
