using ProdutoDLL;
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
    public partial class EditarProdutos : Form
    {
        Main main = new Main();
        Produto produto;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
      int nLeftRect,
      int nTopRect,
      int nRightRect,
      int nBottomRect,
      int nWidthEllipse,
      int nHeightEllipse
      );
        public EditarProdutos(string id,string nome,string descricao,string marca,string preco,string tipo,string tamanho)
        {
            
            InitializeComponent();
            idProd_txt.Text = id;
            nomeProd_txt.Text = nome;
            descriProd_txt.Text = descricao;
            marcaProd_txt.Text = marca;
            precoProd_txt.Text = preco;
            tipoProd_txt.Text = tipo;
            tamanhoProd_txt.Text = tamanho;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));


        }

        private void EditarProdutos_Load(object sender, EventArgs e)
        {

        }

        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void EditarProdutos_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void EditarProdutos_Click(object sender, EventArgs e)
        {
        }


        private void EditarProdutos_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void EditarProdutos_MouseMove(object sender, MouseEventArgs e)
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


        private void EditarProdutos_Load_1(object sender, EventArgs e)
        {
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            double valorConvertido = double.Parse(precoProd_txt.Text);
            decimal valorConvertidoDecmal = decimal.Parse(precoProd_txt.Text);
            int idConvertido = int.Parse(idProd_txt.Text);

            try
            {
                produto = new Produto(nomeProd_txt.Text,descriProd_txt.Text, marcaProd_txt.Text, valorConvertido, tipoProd_txt.Text, tamanhoProd_txt.Text,0);
                produto.AtualizarProdutos(idConvertido, nomeProd_txt.Text, descriProd_txt.Text, marcaProd_txt.Text, valorConvertidoDecmal, tipoProd_txt.Text, tamanhoProd_txt.Text);
                this.Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            main.Show();
        }
    }
}
