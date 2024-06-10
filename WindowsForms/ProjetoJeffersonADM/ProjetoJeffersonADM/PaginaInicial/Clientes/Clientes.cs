using ProdutoDLL;
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
    public partial class Clientes : Form
    {
        DataTable clientes;
        public Clientes()
        {
            InitializeComponent();
        }

        private void ConfigurarColunas()
        {
            bunifuDataGridView1.Columns.Clear();
            bunifuDataGridView1.DataSource = clientes;


            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.HeaderText = "Ações";
            btnEditar.Name = "btnEditar";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.DefaultCellStyle.Font = new Font("Raleway", 9, FontStyle.Bold, GraphicsUnit.Point);
            bunifuDataGridView1.Columns.Add(btnEditar);

            DataGridViewButtonColumn btnDeletar = new DataGridViewButtonColumn();
            btnDeletar.HeaderText = "Ações";
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Text = "Deletar";
            btnDeletar.UseColumnTextForButtonValue = true;
            bunifuDataGridView1.Columns.Add(btnDeletar);
            btnDeletar.DefaultCellStyle.Font = new Font("Raleway", 9,FontStyle.Bold,GraphicsUnit.Point);

        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            clientes = Dao.ObterClientes();
            ConfigurarColunas();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                if (bunifuDataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    {
                        string nomeDaColuna = bunifuDataGridView1.Columns[e.ColumnIndex].Name;
                        int id = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                        string idString = bunifuDataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                        string nome = bunifuDataGridView1.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
                        string rg = bunifuDataGridView1.Rows[e.RowIndex].Cells["Rg"].Value.ToString();
                        string cpf = bunifuDataGridView1.Rows[e.RowIndex].Cells["Cpf"].Value.ToString();
                        string telefone = bunifuDataGridView1.Rows[e.RowIndex].Cells["Telefone"].Value.ToString();
                        string rua = bunifuDataGridView1.Rows[e.RowIndex].Cells["Rua"].Value.ToString();
                        string bairro = bunifuDataGridView1.Rows[e.RowIndex].Cells["Bairro"].Value.ToString();
                        string cidade = bunifuDataGridView1.Rows[e.RowIndex].Cells["Cidade"].Value.ToString();
                        string estado = bunifuDataGridView1.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
                        string email = bunifuDataGridView1.Rows[e.RowIndex].Cells["Email"].Value.ToString();


                        if(nomeDaColuna == "btnEditar")
                        {
                            EditarClientes editarClientes = new EditarClientes(idString, nome, rg, cpf, telefone, rua, bairro,cidade,estado,email);
                            editarClientes.ShowDialog();
                            clientes = Dao.ObterClientes();
                            bunifuDataGridView1.DataSource = clientes;
                        }
                        else if(nomeDaColuna == "btnDeletar") {

                            DialogResult result = MessageBox.Show($"Tem certeza que deseja deletar {bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value}?", "Confirmação", MessageBoxButtons.YesNo);

                            if (result == DialogResult.Yes)
                            {
                                try
                                {
                                    Dao.RemoverClientes(id);
                                    MessageBox.Show($"{bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value} deletado com sucesso!");

                                    clientes = Dao.ObterClientes();
                                    bunifuDataGridView1.DataSource = clientes;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Erro ao deletar cliente: {ex.Message}");
                                }
                            }
                        }
                    }
                }
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dao.ConectarBancoDeDados();
            clientes = Dao.ObterClientes();
            bunifuDataGridView1.DataSource = clientes;
            Dao.FecharConexao();
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

                    DataView view = new DataView(clientes);
                    view.RowFilter = filtro;
                    bunifuDataGridView1.DataSource = view;
                }
                else
                {
                    bunifuDataGridView1.DataSource = clientes;
                }
            
        }
    }
}
