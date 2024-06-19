using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pedidos;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class AdicionarPedido : Form
    {
        double preco;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );
        public AdicionarPedido(string id, string fabricante)
        {
            InitializeComponent();

            idProd_txt.Text = id;
            idFabricante_txt.Text = fabricante;
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));

        }
        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void login_button_Click(object sender, EventArgs e)
        {

            try
            {
                if (String.IsNullOrEmpty(idProd_txt.Text))
                {
                    nome.Show(this, "Id do Produto vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                
                else if (String.IsNullOrEmpty(idFabricante_txt.Text))
                {
                    nome.Show(this, "Id do Fabricante vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                else if (String.IsNullOrEmpty(dataPedido_txt.Text))
                {
                    nome.Show(this, "Data do Pedido vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                else if (String.IsNullOrEmpty(pagamento_txt.Text))
                {
                    nome.Show(this, "Forma de Pagamento vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                else if (String.IsNullOrEmpty(idCli_txt.Text))
                {
                    nome.Show(this, "Id do Cliente vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                else if (String.IsNullOrEmpty(parcelas_txt.Text))
                {
                    nome.Show(this, "Parcelas vazia", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
                else if (String.IsNullOrEmpty(quantidade_txt.Text ) || quantidade_txt.Text == "0")
                {
                    nome.Show(this, "Quantidade do Produto vazio", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error); return;
                }
            
                int idProduto = int.Parse(idProd_txt.Text);
                int quantidade = int.Parse(quantidade_txt.Text);

                int quantidadeEmEstoque = Dao.AcharQuantidadeEstoque(idProduto);
                 if(quantidadeEmEstoque < quantidade) {
                    nome.Show(this, "Quantidade pedida,maior que o estoque disponivel.", Bunifu.UI.WinForms.BunifuSnackbar.MessageTypes.Error);
                    return;
                }

                int parcelas = int.Parse(parcelas_txt.Text);
                int idProd = int.Parse(idProd_txt.Text);
                DateTime data = DateTime.Parse(dataPedido_txt.Text);
                int idCli = int.Parse(idCli_txt.Text);
                preco = Dao.AcharPrecoUnitario(idProd);
                double valorTotal = preco * quantidade;
                int fabricante = int.Parse(idFabricante_txt.Text);

                Pedido pedidos = new Pedido(data, idCli, idProd, LoginID.IdUser, parcelas, quantidade, valorTotal, pagamento_txt.Text, fabricante);
                pedidos.CriarPedido(LoginID.IdUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void AdicionarPedido_Load(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
