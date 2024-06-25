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
using Usuario;

namespace ProjetoJeffersonADM
{

    public partial class Entrada_saida : Form
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
        public Entrada_saida()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }

        private void Entrada_saida_Load(object sender, EventArgs e)
        {
            DataTable tabelaEntrada = Dao.ObterEntradas();
            entrada.DataSource = tabelaEntrada;
            DataTable tabelaSaida = Dao.ObterSaida();
            saida.DataSource = tabelaSaida;


        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
            Financas financas =  new Financas();
            financas.Show();
        }
    }
}