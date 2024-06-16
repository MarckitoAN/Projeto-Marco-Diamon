using System;
using System.Data;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ProdutoDLL;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class TelaProdutos : Form
    {
        Produto produto;
        DataTable produtos;
        readonly EditarProdutos editarProdutos;
        readonly Main main = new Main();


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public TelaProdutos()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }

        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void TelaProdutos_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void TelaProdutos_Click(object sender, EventArgs e)
        {
        }


        private void TelaProdutos_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void TelaProdutos_MouseMove(object sender, MouseEventArgs e)
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

        private void ConfigurarColunas()
        {
            bunifuDataGridView1.Columns.Clear();
            bunifuDataGridView1.DataSource = produtos;



            bunifuDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void Produtos_Load(object sender, EventArgs e)
        {
            produtos = Dao.ObterProdutos();

            ConfigurarColunas();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            AdicionarProdutos adicionarProdutos = new AdicionarProdutos();
            adicionarProdutos.Show();
        }


        private void pesquisa_txt_TextChanged(object sender, EventArgs e)
        {
            string termoDePesquisa = pesquisa_txt.Text.Trim();

            if (!string.IsNullOrEmpty(termoDePesquisa))
            {
                string filtro = $"Convert(id, 'System.String') LIKE '%{termoDePesquisa}%' OR " +
                                $"nome LIKE '%{termoDePesquisa}%' OR " +
                                $"descricao LIKE '%{termoDePesquisa}%' OR " +
                                $"marca LIKE '%{termoDePesquisa}%' OR " +
                                $"Convert(preco, 'System.String') LIKE '%{termoDePesquisa}%' OR " +
                                $"tipo LIKE '%{termoDePesquisa}%' OR " +
                                $"tamanho LIKE '%{termoDePesquisa}%' OR ";

                DataView view = new DataView(produtos);
                view.RowFilter = filtro;
                bunifuDataGridView1.DataSource = view;
            }
            else
            {
                bunifuDataGridView1.DataSource = produtos;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            produtos = Dao.ObterProdutos();
            bunifuDataGridView1.DataSource = produtos;
        }



        private void button123_Click(object sender, EventArgs e)
        {
            Main2 main2 = new Main2();
            this.Hide();
            main2.Show();
        }
           
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.Hide();
            main.Show();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                int columnIndex = bunifuDataGridView1.SelectedCells[0].ColumnIndex;

                string idString = bunifuDataGridView1.Rows[rowIndex].Cells["id"].Value.ToString();
                string idFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["IdFornecedor"].Value.ToString();
                string nome = bunifuDataGridView1.Rows[rowIndex].Cells["nome"].Value.ToString();
                string descricao = bunifuDataGridView1.Rows[rowIndex].Cells["descricao"].Value.ToString();
                string marca = bunifuDataGridView1.Rows[rowIndex].Cells["marca"].Value.ToString();
                string preco = bunifuDataGridView1.Rows[rowIndex].Cells["PrecoVenda"].Value.ToString();
                double precoDouble = Convert.ToDouble(bunifuDataGridView1.Rows[rowIndex].Cells["PrecoVenda"].Value);
                string tipo = bunifuDataGridView1.Rows[rowIndex].Cells["tipo"].Value.ToString();
                string tamanho = bunifuDataGridView1.Rows[rowIndex].Cells["tamanho"].Value.ToString();
                string precoDeCusto = bunifuDataGridView1.Rows[rowIndex].Cells["precoDeCusto"].Value.ToString();
                EditarProdutos editarProdutos = new EditarProdutos(idString,idFornecedor, nome, descricao, marca, preco, tipo, tamanho,precoDeCusto);
                editarProdutos.ShowDialog();
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                int columnIndex = bunifuDataGridView1.SelectedCells[0].ColumnIndex;


                int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["id"].Value);
                string idFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["IdFornecedor"].Value.ToString();
                string nome = bunifuDataGridView1.Rows[rowIndex].Cells["nome"].Value.ToString();
                string descricao = bunifuDataGridView1.Rows[rowIndex].Cells["descricao"].Value.ToString();
                string marca = bunifuDataGridView1.Rows[rowIndex].Cells["marca"].Value.ToString();
                string preco = bunifuDataGridView1.Rows[rowIndex].Cells["PrecoVenda"].Value.ToString();
                double precoDouble = Convert.ToDouble(bunifuDataGridView1.Rows[rowIndex].Cells["PrecoVenda"].Value);
                string tipo = bunifuDataGridView1.Rows[rowIndex].Cells["tipo"].Value.ToString();
                string tamanho = bunifuDataGridView1.Rows[rowIndex].Cells["tamanho"].Value.ToString();
                string precoDeCusto = bunifuDataGridView1.Rows[rowIndex].Cells["precoDeCusto"].Value.ToString();
                byte[] test = { 1, 2, 2, 3, 4, 5, 6, };

                produto = new Produto(nome,descricao,marca,0, tipo, tamanho, 0,0,0, test);

                produto.RemoverProdutos(id);
                produtos = Dao.ObterProdutos();
                bunifuDataGridView1.DataSource = produtos;
            }
    }

        private void pedido_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                string id = bunifuDataGridView1.Rows[rowIndex].Cells["id"].Value.ToString();
                string idFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["IdFornecedor"].Value.ToString();

                AdicionarPedido adicionarPedido = new AdicionarPedido(id, idFornecedor);
                adicionarPedido.ShowDialog();
            }
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
    }
}
