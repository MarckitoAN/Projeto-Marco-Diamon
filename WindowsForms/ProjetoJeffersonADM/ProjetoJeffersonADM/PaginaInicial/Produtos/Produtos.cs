using System;
using System.Data;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Bunifu.UI.WinForms.Helpers.Transitions;
using MySql.Data.MySqlClient;
using ProdutoDLL;
using ProjetoJeffersonADM;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Usuario;
using static Mysqlx.Datatypes.Scalar.Types;

namespace ProjetoJeffersonADM
{
    public partial class TelaProdutos : Form
    {
        Produto produto;
        DataTable produtos;
        EditarProdutos editarProdutos;
        Main main = new Main();


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
            adicionarProdutos.ShowDialog();
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
                string nome = bunifuDataGridView1.Rows[rowIndex].Cells["nome"].Value.ToString();
                string descricao = bunifuDataGridView1.Rows[rowIndex].Cells["descricao"].Value.ToString();
                string marca = bunifuDataGridView1.Rows[rowIndex].Cells["marca"].Value.ToString();
                string preco = bunifuDataGridView1.Rows[rowIndex].Cells["preco"].Value.ToString();
                double precoDouble = Convert.ToDouble(bunifuDataGridView1.Rows[rowIndex].Cells["preco"].Value);
                string tipo = bunifuDataGridView1.Rows[rowIndex].Cells["tipo"].Value.ToString();
                string tamanho = bunifuDataGridView1.Rows[rowIndex].Cells["tamanho"].Value.ToString();
                EditarProdutos editarProdutos = new EditarProdutos(idString, nome, descricao, marca, preco, tipo, tamanho);
                editarProdutos.ShowDialog();
            }
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                int columnIndex = bunifuDataGridView1.SelectedCells[0].ColumnIndex;

                string columnName = bunifuDataGridView1.Columns[columnIndex].Name;
                int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["id"].Value);
                string idString = bunifuDataGridView1.Rows[rowIndex].Cells["id"].Value.ToString();
                string nome = bunifuDataGridView1.Rows[rowIndex].Cells["nome"].Value.ToString();
                string descricao = bunifuDataGridView1.Rows[rowIndex].Cells["descricao"].Value.ToString();
                string marca = bunifuDataGridView1.Rows[rowIndex].Cells["marca"].Value.ToString();
                string preco = bunifuDataGridView1.Rows[rowIndex].Cells["preco"].Value.ToString();
                double precoDouble = Convert.ToDouble(bunifuDataGridView1.Rows[rowIndex].Cells["preco"].Value);
                string tipo = bunifuDataGridView1.Rows[rowIndex].Cells["tipo"].Value.ToString();
                string tamanho = bunifuDataGridView1.Rows[rowIndex].Cells["tamanho"].Value.ToString();

                produto = new Produto(nome,descricao,marca,0, tipo, tamanho, 0 );

                produto.RemoverProdutos(id);
                produtos = Dao.ObterProdutos();
                bunifuDataGridView1.DataSource = produtos;
            }
    }
    }
}
