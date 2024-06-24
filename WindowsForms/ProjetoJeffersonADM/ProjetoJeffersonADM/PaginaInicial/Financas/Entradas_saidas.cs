using Bunifu.UI.WinForms.Helpers.Transitions;
using MySql.Data.MySqlClient;
using ProdutoDLL;
using ProjetoJeffersonADM.PaginaInicial.Financas;
using System;
using System.Data;
using System.Windows.Forms;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class TelaProdutos : Form
    {
        DataTable produtos;

        public TelaProdutos()
        {
            InitializeComponent();
        }

        private void ConfigurarColunas()
        {
            bunifuDataGridView1.Columns.Clear();
            bunifuDataGridView1.DataSource = produtos;

            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.HeaderText = "Ações";
            btnEditar.Name = "btnEditar";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            bunifuDataGridView1.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnDeletar = new DataGridViewButtonColumn();
            btnDeletar.HeaderText = "Ações";
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Text = "Deletar";
            btnDeletar.UseColumnTextForButtonValue = true;
            bunifuDataGridView1.Columns.Add(btnDeletar);

            bunifuDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void Produtos_Load(object sender, EventArgs e)
        {
            produtos = Dao.ObterProdutos();

            ConfigurarColunas();

            bunifuDataGridView1.CellContentClick += bunifuDataGridView1_CellContentClick;
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (bunifuDataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    string columnName = bunifuDataGridView1.Columns[e.ColumnIndex].Name;
                    int id = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                    string idString = bunifuDataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                    string nome = bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                    string descricao = bunifuDataGridView1.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                    string marca = bunifuDataGridView1.Rows[e.RowIndex].Cells["marca"].Value.ToString();
                    string preco = bunifuDataGridView1.Rows[e.RowIndex].Cells["preco"].Value.ToString();
                    string tipo = bunifuDataGridView1.Rows[e.RowIndex].Cells["tipo"].Value.ToString();
                    string tamanho = bunifuDataGridView1.Rows[e.RowIndex].Cells["tamanho"].Value.ToString();
                    string cor = bunifuDataGridView1.Rows[e.RowIndex].Cells["cor"].Value.ToString();

                    if (columnName == "btnEditar")
                    {
                        DialogResult resultado = MessageBox.Show($"Tem certeza que deseja editar {bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value}?", "Confirmação", MessageBoxButtons.YesNo);
                        if (resultado == DialogResult.Yes)
                        {
                            try
                            {
                                EditarProdutos editarProdutos = new EditarProdutos(idString, nome, descricao, marca, preco, tipo, tamanho, cor);
                                editarProdutos.ShowDialog();
                                produtos = Dao.ObterProdutos();
                                bunifuDataGridView1.DataSource = produtos;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao Editar produto: {ex.Message}");
                            }
                        }

                    }
                    else if (columnName == "btnDeletar")
                    {

                        DialogResult result = MessageBox.Show($"Tem certeza que deseja deletar {bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value}?", "Confirmação", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                Dao.RemoverProdutos(id);
                                MessageBox.Show($"{bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value} deletado com sucesso!");

                                produtos = Dao.ObterProdutos();
                                bunifuDataGridView1.DataSource = produtos;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao deletar produto: {ex.Message}");
                            }
                        }


                    }
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            AdicionarProdutos adicionarProdutos = new AdicionarProdutos();
            adicionarProdutos.ShowDialog();
        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

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
                                $"tamanho LIKE '%{termoDePesquisa}%' OR " +
                                $"cor LIKE '%{termoDePesquisa}%'";

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
    }
}
