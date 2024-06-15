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
            int parcelas = int.Parse(parcelas_txt.Text);
            int quantidade = int.Parse(quantidade_txt.Text);
            int idProd = int.Parse(idProd_txt.Text);
            DateTime data = DateTime.Parse(dataPedido_txt.Text);
            int idCli = int.Parse(idCli_txt.Text);
            preco = Dao.AcharPrecoUnitario(idProd);
            double valorTotal = preco * quantidade;
            int fabricante = int.Parse(idFabricante_txt.Text);

            Pedido pedidos = new Pedido(data,idCli,idProd,LoginID.IdUser,parcelas,quantidade,valorTotal,pagamento_txt.Text, fabricante);
            pedidos.CriarPedido(LoginID.IdUser);
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
