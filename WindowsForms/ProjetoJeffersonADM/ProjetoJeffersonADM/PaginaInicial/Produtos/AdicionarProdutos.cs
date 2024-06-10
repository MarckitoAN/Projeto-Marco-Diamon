﻿using ProdutoDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using ProdutoDLL;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Estoque;
using Usuario;

namespace ProjetoJeffersonADM
{
    public partial class AdicionarProdutos : Form
    {
        public AdicionarProdutos()
        {
            InitializeComponent();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdicionarProdutos_Load(object sender, EventArgs e)
        {

        }

        Point DragCursor;
        Point DragForm;
        bool Dragging;


        private void AdicionarProdutos_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void AdicionarProdutos_Click(object sender, EventArgs e)
        {
        }


        private void AdicionarProdutos_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void AdicionarProdutos_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private const int SombraDaJanela = 0x0002000;

        protected override CreateParams CreateParams
        {

            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = SombraDaJanela;
                return cp;
            }

        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            try
            {

            double preco = double.Parse(precoProd_txt.Text);
            int estoque = int.Parse(estoque_txt.Text);
            Produto produtos = new Produto(nomeProd_txt.Text,descriProd_txt.Text,marcaProd_txt.Text,preco,tipoProd_txt.Text,tamanhoProd_txt.Text,corProd_txt.Text,estoque);
            produtos.AdicionarProdutos();
            this.Hide();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            }
    }
}
