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

    public partial class Entrada_saida : Form
    {
        public Entrada_saida()
        {
            InitializeComponent();
        }

        private void Entrada_saida_Load(object sender, EventArgs e)
        {
            DataTable tabelaEntrada = Dao.ObterEntradas();
            entrada.DataSource = tabelaEntrada;
            DataTable tabelaSaida = Dao.ObterSaida();
            saida.DataSource = tabelaSaida;


        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
            Financas financas =  new Financas();
            financas.Show();
        }
    }
}