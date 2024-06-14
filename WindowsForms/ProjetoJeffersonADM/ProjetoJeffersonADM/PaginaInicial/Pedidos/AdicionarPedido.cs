using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public AdicionarPedido()
        {
            InitializeComponent();
        }

        private void AdicionarPedido_Load(object sender, EventArgs e)
        {

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            int parcelas = int.Parse(parcelas_txt.Text);
            int quantidade = int.Parse(quantidade_txt.Text);
            int idProd = int.Parse(idProd_txt.Text);
            DateTime data = DateTime.Parse(dataPedido_txt.Text);
            int idCli = int.Parse(idCli_txt.Text);
            preco = Dao.AcharPrecoUnitario(idProd);
            double valorTotal = preco * quantidade;

            Pedido pedidos = new Pedido(data, idCli, idProd, LoginID.IdUser, parcelas, quantidade, valorTotal, pagamento_txt.Text);
            pedidos.CriarPedido(LoginID.IdUser);
        }
    }
}
