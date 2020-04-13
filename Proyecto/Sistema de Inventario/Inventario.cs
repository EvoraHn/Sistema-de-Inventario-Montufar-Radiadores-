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
    public partial class Inventario : Form
    {
        ClaConexion c;

        private Clases.ClaListaProductos productos;
        private Clases.ClaProducto producto;
        private Clases.ClaListaInventario elinventario;
        private ClaInventario inventario;
        public Inventario()
        {
            InitializeComponent();
            c = new ClaConexion();
            elinventario= new Clases.ClaListaInventario();
            inventario = new ClaInventario();

            productos = new Clases.ClaListaProductos();
            producto = new Clases.ClaProducto();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void Inventario_Load(object sender, EventArgs e)
        {
            DataTable t1 = productos.SQL(String.Format("Select * FROM taller.producto"));

            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DataGridLectura();
        }

        private void agregarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirformhija(new Producto());
            //Producto form = new Producto();
            //form.Show();
        }

        private void abrirformhija(object formhija)
        {
            if (this.panelcontenedor.Controls.Count > 0)
                this.panelcontenedor.Controls.RemoveAt(0);
            //dataGridView2.Visible = false;
            Form fh = formhija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelcontenedor.Controls.Add(fh);
            this.panelcontenedor.Tag = fh;
            fh.Show();

        }
        private void agregarProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirformhija(new Proveedores());
            //Proveedores form = new Proveedores();
            //form.Show();
        }

        private void agregarCategoríaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirformhija(new Categoria());
            //Categoria form = new Categoria();
            //form.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e){}


        /// <summary>
        /// Funcion que permite que el objeto dataGridView2 sea solo para lectura sin la opcion de modificar alguna
        /// columna que pertenece a este
        /// </summary>
        public void DataGridLectura()
        {
            dataGridView2.Columns["idProducto"].ReadOnly = true;
            dataGridView2.Columns["nombre"].ReadOnly = true;
            dataGridView2.Columns["categoria"].ReadOnly = true;
            dataGridView2.Columns["marca"].ReadOnly = true;
            dataGridView2.Columns["año"].ReadOnly = true;
            dataGridView2.Columns["proveedor"].ReadOnly = true;
            dataGridView2.Columns["existencia"].ReadOnly = true;
            dataGridView2.Columns["precioCompra"].ReadOnly = true;
            dataGridView2.Columns["precioVenta"].ReadOnly = true;

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string sql = "";

            if (Filtro())
            {
                
                sql = string.Format("SELECT * FROM taller.producto WHERE nombre LIKE '" + txtBuscarProducto.Text + "'");
            }
            else
            {
                MessageBox.Show("Se cancelo la edición");

            }

            DataTable t1 = productos.SQL(sql);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();
            //inventario.GuardarInventario();
        }

        private Boolean Filtro()
        {
            Boolean r = true;
            if (txtBuscarProducto.Text == "")
            {
                MessageBox.Show("Escriba algo en la caja de texto", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscarProducto.Focus();
                r = false;
            }
            else if (txtBuscarProducto.Text == "")
            {
                DataTable t1 = productos.SQL(String.Format("SELECT * FROM taller.producto"));
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = t1;
                dataGridView2.Refresh();
            }
            else
                r = true;
            return r;
        }

        private void txtBuscarProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            string sql = "";
            sql = string.Format("SELECT * FROM taller.producto WHERE nombre LIKE '" + txtBuscarProducto.Text + "' or idProducto LIKE '" + txtBuscarProducto.Text + "'");
        }
    }
}
