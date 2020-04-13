using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    public partial class Inicio : Form
    {
        private Clases.ClaUsuario usuarios;
        public Inicio()
        {
            InitializeComponent();
            usuarios = new Clases.ClaUsuario();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" && txtContra.Text == "")
            {
                MessageBox.Show("Debe llenar los campos");
                txtUsuario.Focus();
            }
            else
            {
                if (usuarios.Login(txtUsuario.Text,txtContra.Text))
                {
                    Menu form = new Menu();
                    form.Show();
                    
                    this.DialogResult = DialogResult.Yes;
                    
                }
                else
                    MessageBox.Show("El usuario no existe");
                //this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.No;

        }
    }
    
}
