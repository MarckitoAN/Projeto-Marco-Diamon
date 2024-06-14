using Estoque;
using Pedidos;
using ProdutoDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Usuario;

namespace ProjetoJeffersonADM
{

    public partial class Pedidos : Form
    {
        readonly Pedido pedidos = new Pedido(DateTime.Now, 0, 0, 0, 0, 0, 0, "");
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse
          );
        DataTable pedido;
        public Pedidos()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }



        private void Pedidos_Load(object sender, EventArgs e)
        {

            pedido = Dao.ObterPedidos();
            bunifuDataGridView1.DataSource = pedido;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pedido = Dao.ObterPedidos();
            bunifuDataGridView1.DataSource = pedido;

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            AdicionarPedido addPedido = new AdicionarPedido();
            addPedido.Show();
        }

        private void apagar_btn_Click(object sender, EventArgs e)
        {

            if (bunifuDataGridView1.SelectedCells.Count > 0) {

                int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;

                int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID"].Value);
                int quantidade = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["Quantidade"].Value);
                int idProduto = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ProdutoID"].Value);

                pedidos.RemoverPedidos(id, quantidade, idProduto);

            }

            Pedidos peddos = new Pedidos();
            pedido = Dao.ObterPedidos();
            bunifuDataGridView1.DataSource = pedido;
        }

        private void Pedidos_Load_1(object sender, EventArgs e)
        {
            pedido = Dao.ObterPedidos();
            bunifuDataGridView1.DataSource = pedido;
        }

        private void pesquisa_txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            // Caminho para salvar o PDF


            // Obtenção dos dados da DataGridView
            int rowIndex = bunifuDataGridView1.SelectedCells[0].RowIndex;

            string nomeCli = bunifuDataGridView1.Rows[rowIndex].Cells["Cliente"].Value.ToString();
            string formaPagamento = bunifuDataGridView1.Rows[rowIndex].Cells["forma_pagamento"].Value.ToString();
            int id = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ID"].Value);
            int idCliente = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ClienteID"].Value);
            int idProduto = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["ProdutoID"].Value);
            int quantidade = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["Quantidade"].Value);
            double precoUnitario = Dao.AcharPrecoUnitario(idProduto);
            double ValorTotal = Convert.ToDouble(bunifuDataGridView1.Rows[rowIndex].Cells["ValorTotal"].Value);
            int parcelas = Convert.ToInt32(bunifuDataGridView1.Rows[rowIndex].Cells["Parcelas"].Value);
            string cpf = Dao.AcharCPF(idCliente);
            string rua = Dao.AcharEndereco(idProduto);
            string cidade = Dao.AcharCidade(idProduto);
            string telefone = Dao.AcharTelefone(idProduto);
            string nomeSemEspacos = nomeCli.Replace(" ", "");

            string pastaNotaFiscal = @"C:\Users\gamer\OneDrive\Documentos";

            // Verificar e criar o diretório, se necessário
            if (!Directory.Exists(pastaNotaFiscal))
            {
                Directory.CreateDirectory(pastaNotaFiscal);
            }

            // Construir o caminho do arquivo
            string caminhoFiscal = Path.Combine(pastaNotaFiscal, $"{nomeSemEspacos}.pdf");

            DateTime data = DateTime.Today;

            // Adicionar logs para depuração
            Console.WriteLine($"Iniciando criação do PDF em: {caminhoFiscal}");

        }      
    }
}


    
