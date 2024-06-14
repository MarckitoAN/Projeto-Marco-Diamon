using Estoque;
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

namespace ProjetoJeffersonADM { 

    public partial class Pedidos : Form
    {
        DataTable pedido;
        public Pedidos()
        {
            InitializeComponent();
        }


        private void ConfigurarColunas()
        {

            DataGridViewButtonColumn btnDeletar = new DataGridViewButtonColumn();
            btnDeletar.HeaderText = "Ações";
            btnDeletar.Name = "btnDeletar";
            btnDeletar.Text = "Deletar";
            btnDeletar.UseColumnTextForButtonValue = true;
            btnDeletar.DefaultCellStyle.Font = new Font("Raleway", 9, FontStyle.Bold, GraphicsUnit.Point);
            bunifuDataGridView1.Columns.Add(btnDeletar);

        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
            pedido = Dao.ObterPedidos();
            ConfigurarColunas();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pedido = Dao.ObterPedidos();
            bunifuDataGridView1.DataSource = pedido;

        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (bunifuDataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    string nomeDaColuna = bunifuDataGridView1.Columns[e.ColumnIndex].Name;
          

                    if (nomeDaColuna == "btnEditar")
                    {

                        try
                        {
        
                            pedido = Dao.ObterPedidos();
                            bunifuDataGridView1.DataSource = pedido;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao Editar Pedido: {ex.Message}");
                        }

                    }
                    else if (nomeDaColuna == "btnDeletar")
                    {

                        DialogResult result = MessageBox.Show($"Tem certeza que deseja deletar {bunifuDataGridView1.Rows[e.RowIndex].Cells["Nome"].Value}?", "Confirmação", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                MessageBox.Show($"{bunifuDataGridView1.Rows[e.RowIndex].Cells["nome"].Value} deletado com sucesso!");

                           
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao deletar pedido: {ex.Message}");
                            }
                        }


                    }

                }
            }
        }


        private void add_btn_Click(object sender, EventArgs e)
        {
            AdicionarPedido addPedido = new AdicionarPedido();
            addPedido.Show();
        }

        private void apagar_btn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
            string idString = bunifuDataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            int idCliente = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["ClienteID"].Value);
            string IdClienteString = bunifuDataGridView1.Rows[e.RowIndex].Cells["ClienteID"].Value.ToString();
            string nomeCliente = bunifuDataGridView1.Rows[e.RowIndex].Cells["Cliente"].Value.ToString();
            int produtoId = Convert.ToInt32(bunifuDataGridView1.Rows[e.RowIndex].Cells["ProdutoID"].Value);
            string produtoIdString = bunifuDataGridView1.Rows[e.RowIndex].Cells["ProdutoID"].Value.ToString();
            string nomeProduto = bunifuDataGridView1.Rows[e.RowIndex].Cells["Produto"].Value.ToString();
            string formaDePagamento = bunifuDataGridView1.Rows[e.RowIndex].Cells["forma_pagamento"].Value.ToString();
            string parcelas = bunifuDataGridView1.Rows[e.RowIndex].Cells["parcelas"].Value.ToString();
            double valorTotal = Convert.ToDouble(bunifuDataGridView1.Rows[e.RowIndex].Cells["parcelas"].Value);

            Pedidos peddos = new Pedidos();
            pedido = Dao.ObterEstoque();
            bunifuDataGridView1.DataSource = pedido;
        }
    }
}
