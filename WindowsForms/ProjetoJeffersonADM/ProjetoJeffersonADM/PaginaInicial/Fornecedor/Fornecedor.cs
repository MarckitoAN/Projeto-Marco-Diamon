using Fabricante;
using Mysqlx;
using Pedidos;
using ProjetoJeffersonADM.PaginaInicial.Fornecedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class Fornecedor : Form
    {
       readonly Fabricantes fabricantes = new Fabricantes("","", "", "", "", "","");
        DataTable fornecedor = new DataTable();

        public Fornecedor()
        {
            InitializeComponent();

        }

        private void titulo_label_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Fornecedor_Load(object sender, EventArgs e)
        {
            fornecedor = Dao.ObterFornecedor();
            bunifuDataGridView1.DataSource = fornecedor;
        }



        private void add_btn_Click_1(object sender, EventArgs e)
        {
            AdicionarFornecedor adicionarFornecedor = new AdicionarFornecedor();
            adicionarFornecedor.Show();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            fornecedor = Dao.ObterFornecedor();
            bunifuDataGridView1.DataSource = fornecedor;
        }

        private void apagar_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID"].Value);
                fabricantes.RemoverFornecedor(id);
            }
            else
            {
                MessageBox.Show("Nenhuma Coluna selecionada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            fornecedor = Dao.ObterFornecedor();
            bunifuDataGridView1.DataSource = fornecedor;
        }

        private void editar_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count > 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID"].Value);
                string idString = bunifuDataGridView1.Rows[rowIndex].Cells["ID"].Value.ToString();
                string nome = bunifuDataGridView1.Rows[rowIndex].Cells["Nome"].Value.ToString();
                string rua = bunifuDataGridView1.Rows[rowIndex].Cells["Rua"].Value.ToString();
                string bairro = bunifuDataGridView1.Rows[rowIndex].Cells["Bairro"].Value.ToString();
                string cidade = bunifuDataGridView1.Rows[rowIndex].Cells["Cidade"].Value.ToString();
                string estado = bunifuDataGridView1.Rows[rowIndex].Cells["Estado"].Value.ToString();
                string email = bunifuDataGridView1.Rows[rowIndex].Cells["Email"].Value.ToString();
                string cnpj = bunifuDataGridView1.Rows[rowIndex].Cells["Cnpj"].Value.ToString();
                EditarFornecedor editarFornecedor = new EditarFornecedor(idString, nome, rua, bairro, cidade, estado, email, cnpj);
                editarFornecedor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhuma Coluna selecionada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

