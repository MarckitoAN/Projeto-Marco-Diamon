using ProdutoDLL;
using ProjetoJeffersonADM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class Main : Form
    {
        public Main()
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
        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            TelaProdutos produtos = new TelaProdutos();
            this.Hide();
            produtos.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

