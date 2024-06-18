using Bunifu.Dataviz.WinForms;
using Pedidos;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class Main2 : Form
    {

        DataTable ultimosPedidos = new DataTable();

        public Main2()
        {

            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            CriarGrafico();
            produto.Text = Dao.ProdutoMaisVendido();
            pedido.Text = $"{Dao.NumeroPedido().ToString()} Pedidos";
            preco.Text = $"R$:{Dao.ValorTotal().ToString()}";

        }


        private BunifuDatavizBasic.Canvas canvas;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse
    );

        private void CriarGrafico()
        {
            canvas = new BunifuDatavizBasic.Canvas();
            var dataPoint = new BunifuDatavizBasic.DataPoint(BunifuDatavizBasic._type.Bunifu_line);
            dataPoint.addLabely("Janeiro", "15");
            dataPoint.addLabely("Fevereiro", "35");
            dataPoint.addLabely("Março", "66");
            dataPoint.addLabely("Abril", "70");
            dataPoint.addLabely("Maio", "30");
            dataPoint.addLabely("Junho", "80");

            canvas.addData(dataPoint);

            bunifuDatavizBasic1.Render(canvas);
        }


        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void Main_Load_1(object sender, EventArgs e)
        {
            CriarGrafico();
        }

        private void Main_Activated(object sender, EventArgs e)
        {
            CriarGrafico();
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
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

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            TelaProdutos produtos = new TelaProdutos();
            this.Hide();
            produtos.Show();
        }


        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Estoque estoque = new Estoque();
            this.Hide();
            estoque.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            this.Hide();
            clientes.Show();
        }

        private void bunifuButton2_Click_1(object sender, EventArgs e)
        {
            TelaProdutos telaProdutos = new TelaProdutos();
            this.Hide();
            telaProdutos.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Pedidos pedido = new Pedidos();
            this.Hide();
            pedido.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            this.Hide();
            fornecedor.Show();
        }

        private void bunifuDatavizBasic1_Load(object sender, EventArgs e)
        {
            CriarGrafico();

        }

        private void Main2_Load(object sender, EventArgs e)
        {
            ultimosPedidos = Dao.UltimosPedidos();

            bunifuDataGridView1.DataSource = ultimosPedidos;

            bunifuDataGridView1.ColumnHeadersVisible = false;
        }

        private void bunifuButton2_Click_2(object sender, EventArgs e)
        {
            TelaProdutos produtos = new TelaProdutos();
            this.Hide();
            produtos.Show();
        }

        private void bunifuButton3_Click_1(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            this.Hide();
            clientes.Show();
        }

        private void bunifuButton5_Click_1(object sender, EventArgs e)
        {
            Estoque estoque = new Estoque();
            this.Hide();
            estoque.Show();
        }

        private void bunifuButton6_Click_1(object sender, EventArgs e)
        {
            Pedidos pedidos = new Pedidos();
            this.Hide();
            pedidos.Show();
        }

        private void bunifuButton4_Click_1(object sender, EventArgs e)
        {
            Fornecedor fornecedor
                = new Fornecedor();
            this.Hide();
            fornecedor.Show();
        }

        private void produto_Click(object sender, EventArgs e)
        {

        }

 
        private void bunifuDatavizBasic1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
