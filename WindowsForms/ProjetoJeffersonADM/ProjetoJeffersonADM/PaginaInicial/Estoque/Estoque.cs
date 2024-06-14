using Estoque;
using ProdutoDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Usuario;
using static Mysqlx.Datatypes.Scalar.Types;

namespace ProjetoJeffersonADM 
{
    public partial class Estoque : Form
    {
        Estoques estoques;
        DataTable estoque;
        Main main = new Main();
        
        public Estoque()
        {
            InitializeComponent();
        }

        private void Estoque_Load(object sender, EventArgs e)
        {
            estoque = Dao.ObterEstoque();
            ConfigurarColunas();
        }
        private void ConfigurarColunas()
        {
            bunifuDataGridView1.Columns.Clear();
            bunifuDataGridView1.DataSource = estoque;

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




        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                if(bunifuDataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    string nomeDaColuna = bunifuDataGridView1.Columns[e.ColumnIndex].Name;
                    int idEstoque = Convert.ToInt32((bunifuDataGridView1.Rows[e.RowIndex].Cells["ID_Estoque"].Value));
                    string idEstoqueString = bunifuDataGridView1.Rows[e.RowIndex].Cells["ID_Estoque"].Value.ToString();
                    int idProduto = Convert.ToInt32((bunifuDataGridView1.Rows[e.RowIndex].Cells["ID_Produto"].Value));
                    string idProdutoString = bunifuDataGridView1.Rows[e.RowIndex].Cells["ID_Produto"].Value.ToString();
                    string nomeDoProduto = bunifuDataGridView1.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
                    string marca  = bunifuDataGridView1.Rows[e.RowIndex].Cells["Marca"].Value.ToString();
                    string tipo  = bunifuDataGridView1.Rows[e.RowIndex].Cells["Tipo"].Value.ToString();
                    int quantidade  = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["Quantidade"].Value);
                    string quantidadeString  = bunifuDataGridView1.Rows[e.RowIndex].Cells["Quantidade"].Value.ToString();
                    string entrada  =bunifuDataGridView1.Rows[e.RowIndex].Cells["Entrada"].Value.ToString();
                    string saida  = bunifuDataGridView1.Rows[e.RowIndex].Cells["Saida"].Value.ToString();

                    if (nomeDaColuna == "btnEditar")
                    {

                        try
                        {
                            EditarEstoque editarEstoque = new EditarEstoque(idEstoqueString, idProdutoString, nomeDoProduto, marca, tipo, entrada,saida,quantidadeString);
                            editarEstoque.Show();
                            estoque = Dao.ObterEstoque();
                            bunifuDataGridView1.DataSource = estoque;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao Editar produto: {ex.Message}");
                        }

                    }
                    else if (nomeDaColuna == "btnDeletar")
                    {

                        DialogResult result = MessageBox.Show($"Tem certeza que deseja deletar {bunifuDataGridView1.Rows[e.RowIndex].Cells["Nome"].Value}?", "Confirmação", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                estoques.RemoverEstoque(idProduto);
                                MessageBox.Show($"{bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value} deletado com sucesso!");

                                estoque = Dao.ObterEstoque();
                                bunifuDataGridView1.DataSource = estoque;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao deletar estoque: {ex.Message}");
                            }
                        }


                    }

                } 
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            estoque = Dao.ObterEstoque();
            bunifuDataGridView1.DataSource = estoque;

        }

        private void bunifuDatavizBasic1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}