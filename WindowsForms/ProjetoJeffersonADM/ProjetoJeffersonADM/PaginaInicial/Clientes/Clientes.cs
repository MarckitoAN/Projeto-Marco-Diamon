using Bunifu.UI.WinForms;
using ClientesDLL;
using ProdutoDLL;
using ProjetoJeffersonADM.PaginaInicial.Clientes;
using ProjetoJeffersonADM;
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
    public partial class Clientes : Form
    {
        ClientesDLL.Clientes cli;
        DataTable clientes;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
           int nLeftRect,
           int nTopRect,
           int nRightRect,
           int nBottomRect,
           int nWidthEllipse,
           int nHeightEllipse
           );
        public Clientes()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }


        private void Clientes_Load(object sender, EventArgs e)
        {
            clientes = Dao.ObterClientes();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            clientes = Dao.ObterClientes();
            bunifuDataGridView1.DataSource = clientes;
        }

        private void pesquisa_txt_TextChanged(object sender, EventArgs e)
        {

            string termoDePesquisa = pesquisa_txt.Text.Trim();

            if (!string.IsNullOrEmpty(termoDePesquisa))
            {
                string filtro = $"Convert(id, 'System.String') LIKE '%{termoDePesquisa}%' OR " +
                                $"Nome LIKE '%{termoDePesquisa}%' OR " +
                                $"Rg LIKE '%{termoDePesquisa}%' OR " +
                                $"Cpf LIKE '%{termoDePesquisa}%' OR " +
                                $"Telefone LIKE '%{termoDePesquisa}%' OR " +
                                $"Rua LIKE '%{termoDePesquisa}%' OR " +
                                $"Bairro LIKE '%{termoDePesquisa}%' OR " +
                                $"Cidade LIKE '%{termoDePesquisa}%' OR " +
                                $"Estado LIKE '%{termoDePesquisa}%' OR " +
                                $"Email LIKE '%{termoDePesquisa}%'";

                DataView filtrar = new DataView(clientes);
                filtrar.RowFilter = filtro;
                bunifuDataGridView1.DataSource = filtrar;
            }
            else
            {
                bunifuDataGridView1.DataSource = clientes;
            }

        }

        private void editar_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int indexDaLinha = bunifuDataGridView1.SelectedCells[0].RowIndex;

                int id = Convert.ToInt32(bunifuDataGridView1.Rows[indexDaLinha].Cells["Id"].Value);
                string idString = bunifuDataGridView1.Rows[indexDaLinha].Cells["Id"].Value.ToString();
                string nome = bunifuDataGridView1.Rows[indexDaLinha].Cells["Nome"].Value.ToString();
                string rg = bunifuDataGridView1.Rows[indexDaLinha].Cells["Rg"].Value.ToString();
                string cpf = bunifuDataGridView1.Rows[indexDaLinha].Cells["Cpf"].Value.ToString();
                string telefone = bunifuDataGridView1.Rows[indexDaLinha].Cells["Telefone"].Value.ToString();
                string rua = bunifuDataGridView1.Rows[indexDaLinha].Cells["Rua"].Value.ToString();
                string bairro = bunifuDataGridView1.Rows[indexDaLinha].Cells["Bairro"].Value.ToString();
                string cidade = bunifuDataGridView1.Rows[indexDaLinha].Cells["Cidade"].Value.ToString();
                string estado = bunifuDataGridView1.Rows[indexDaLinha].Cells["Estado"].Value.ToString();
                string email = bunifuDataGridView1.Rows[indexDaLinha].Cells["Email"].Value.ToString();

                EditarClientes editarClientes = new EditarClientes(idString, nome, rg, cpf, telefone, rua, bairro, cidade, estado, email);
                editarClientes.ShowDialog();
                clientes = Dao.ObterClientes();
                bunifuDataGridView1.DataSource = clientes;
            }
        }

        private void apagar_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int indexDaLinha = bunifuDataGridView1.SelectedCells[0].RowIndex;

                int id = Convert.ToInt32(bunifuDataGridView1.Rows[indexDaLinha].Cells["Id"].Value);
                string nome = bunifuDataGridView1.Rows[indexDaLinha].Cells["Nome"].Value.ToString();
                string rg = bunifuDataGridView1.Rows[indexDaLinha].Cells["Rg"].Value.ToString();
                string cpf = bunifuDataGridView1.Rows[indexDaLinha].Cells["Cpf"].Value.ToString();
                string telefone = bunifuDataGridView1.Rows[indexDaLinha].Cells["Telefone"].Value.ToString();
                string rua = bunifuDataGridView1.Rows[indexDaLinha].Cells["Rua"].Value.ToString();
                string bairro = bunifuDataGridView1.Rows[indexDaLinha].Cells["Bairro"].Value.ToString();
                string cidade = bunifuDataGridView1.Rows[indexDaLinha].Cells["Cidade"].Value.ToString();
                string estado = bunifuDataGridView1.Rows[indexDaLinha].Cells["Estado"].Value.ToString();
                string email = bunifuDataGridView1.Rows[indexDaLinha].Cells["Email"].Value.ToString();

                cli = new ClientesDLL.Clientes(nome,rg,cpf,telefone,rua,bairro,cidade,estado,email,"0");

                cli.RemoverClientes(id);
                clientes = Dao.ObterClientes();
                bunifuDataGridView1.DataSource = clientes;
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            AdicionarCliente adicionarCliente = new AdicionarCliente();
            adicionarCliente.ShowDialog();
        }

        private void Clientes_Load_1(object sender, EventArgs e)
        {
            clientes = Dao.ObterClientes();
            bunifuDataGridView1.DataSource = clientes;
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Main2 main = new Main2();
            this.Hide();
            main.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            TelaProdutos telaProdutos = new TelaProdutos();
            this.Hide();
            telaProdutos.Show();
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            Estoque estoque = new Estoque();
            this.Hide();
            estoque.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Pedidos pedidos = new Pedidos();
            this.Hide();
            pedidos.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            this.Hide();
            fornecedor.Show();
        }

        private void pesquisa_txt_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            Financas financas = new Financas();
            this.Hide();
            financas.Show();
        }
    }
}
