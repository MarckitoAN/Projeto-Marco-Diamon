﻿using Estoque;
using ProdutoDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
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
        DataTable estoque;


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse
    );
        public Estoque()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }





        private void button1_Click(object sender, EventArgs e)
        {
            estoque = Dao.ObterEstoque();
            bunifuDataGridView1.DataSource = estoque;

        }

        private void Estoque_Load_1(object sender, EventArgs e)
        {
            estoque = Dao.ObterEstoque();
            bunifuDataGridView1.DataSource = estoque;
        }

        private void pedido_btn_Click(object sender, EventArgs e)
        {
            if (bunifuDataGridView1.SelectedCells.Count >= 0)
            {
                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;


                int idEstoque = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID_Estoque"].Value);
                int idProduto = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID_Produto"].Value);
                int idFornecedor = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID_Fornecedor"].Value);
                int quantidade = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["Quantidade"].Value);
                double precoDeCusto = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["Preco_De_Custo"].Value);
                string nomeDoProduto = bunifuDataGridView1.Rows[rowIndex].Cells["Nome_Produto"].Value.ToString();
                double totalDeCusto = precoDeCusto * quantidade;
                string marca = bunifuDataGridView1.Rows[rowIndex].Cells["Marca"].Value.ToString();
                string tipo = bunifuDataGridView1.Rows[rowIndex].Cells["Tipo"].Value.ToString();
                string nomeFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["Nome_Fornecedor"].Value.ToString();
                string ruaFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["Rua_Fornecedor"].Value.ToString();
                string cidadeFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["Cidade_Fornecedor"].Value.ToString();
                string estadoFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["Estado_Fornecedor"].Value.ToString();
                string emailFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["Email_Fornecedor"].Value.ToString();
                string cnpjFornecedor = bunifuDataGridView1.Rows[rowIndex].Cells["CNPJ_Fornecedor"].Value.ToString();
               
                NotaFiscal notaFiscal = new NotaFiscal();
                notaFiscal.EmitirNotaFiscalEstoque(nomeFornecedor, nomeFornecedor, cnpjFornecedor, ruaFornecedor, cidadeFornecedor, estadoFornecedor, "11-23213-5435", idProduto, nomeDoProduto, quantidade, precoDeCusto, totalDeCusto,"Credito");
       
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Main2 main = new Main2();
            this.Hide();
            main.Show();
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            Pedidos pedidos = new Pedidos();
            this.Hide();
            pedidos.Show();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            TelaProdutos telaProdutos = new TelaProdutos();
            this.Hide();
        telaProdutos.Show();
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            Clientes clientes = new Clientes();
            this.Hide();
            clientes.Show();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            Fornecedor fornecedor = new Fornecedor();
            this.Hide();
            fornecedor.Show();
        }
    }
    }
