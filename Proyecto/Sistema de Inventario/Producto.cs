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
    public partial class Producto : Form
    {
        ClaConexion c;
        private Clases.ClaListaProductos productos;
        private Clases.ClaProducto producto;

        private Clases.ClaListaCategorias categorias;
        private ClaCategoria categoria;

        private Clases.ClaListaProveedores proveedores;
        private Clases.ClaProveedor proveedor;

        //private Clases.ClaListaInventario inventarios;
        //private ClaInventario inventario;
        public Producto()
        {
            InitializeComponent();
            c = new ClaConexion();
            productos = new Clases.ClaListaProductos();
            producto = new Clases.ClaProducto();

            categorias = new Clases.ClaListaCategorias();
            categoria = new ClaCategoria();

            proveedores = new Clases.ClaListaProveedores();
            proveedor = new Clases.ClaProveedor();

            //inventarios = new Clases.ClaListaInventario();
            //inventario = new ClaInventario();

        }

        private void Producto_Load(object sender, EventArgs e)
        {
            DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia," +
            "precioCompra,precioVenta FROM taller.producto"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DatosCombos();
            DataGridLectura();    
        }

        /// <summary>
        /// Se cargan los datos en el combobox a la hora darle doble click al DataGrid 
        /// </summary>
        public void DatosCombos()
        {
            DataTable t2 = categorias.SQL(String.Format("SELECT idCategoria, nombre, descripcion FROM " +
            "taller.categoria "));

            cbxCategoria.DataSource = null;
            cbxCategoria.DataSource = t2;
            cbxCategoria.DisplayMember = "nombre";
            cbxCategoria.ValueMember = "idCategoria";
            cbxCategoria.Refresh();

            DataTable t3 = proveedores.SQL(String.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
            "direccion, correoElectronico FROM taller.proveedor"));
            cbxProveedor.DataSource = null;
            cbxProveedor.DataSource = t3;
            cbxProveedor.DisplayMember = "nombre";
            cbxProveedor.ValueMember = "idProveedor";
            cbxProveedor.Refresh();
        }

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


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void Cargar_Datos()
        {
            txtIdProducto.Text = (producto.IdProducto.ToString());
            txtNombreProducto.Text = producto.Nombre;
            cbxCategoria.SelectedValue = producto.Categoria;
            txtAño.Text = producto.Año;
            cbxProveedor.SelectedValue = producto.Proveedor;
            txtMarca.Text = producto.Marca;
            txtCantidad.Text = producto.Existencia.ToString();
            txtPrecioCompra.Text =producto.PrecioCompra.ToString();
            txtPrecioVenta.Text =producto.PrecioVenta.ToString();

            txtIdProducto.Focus();

            
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridLectura();
            DatosCombos();
            txtIdProducto.Enabled = false;
            txtIdProducto.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtNombreProducto.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            cbxCategoria.SelectedValue = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtMarca.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtAño.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            cbxProveedor.SelectedValue = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            txtCantidad.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            txtPrecioCompra.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            txtPrecioVenta.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
        }
        private void limpiar()
        {
            DataGridLectura();
            txtIdProducto.Text = "";
            txtNombreProducto.Text = "";
            txtAño.Text = "";
            txtMarca.Text = "";
            txtIdProducto.Enabled = true;
            txtIdProducto.Focus();
            cbxCategoria.Text= "";
            cbxProveedor.Text = "";
            txtCantidad.Text = "";
            txtPrecioCompra.Text = "";
            txtPrecioVenta.Text = "";

        }

        /// <summary>
        ///  Boton de agregar donde se guardan los parametros correspondientes al los textBox
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                producto.IdProducto = txtIdProducto.Text;
                producto.Nombre = txtNombreProducto.Text;
                producto.Categoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                producto.Año = txtAño.Text;
                producto.Proveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
                producto.Marca = txtMarca.Text;
                producto.Existencia = Convert.ToInt32(txtCantidad.Text);
                producto.PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text);
                //producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                producto.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);


                if (producto.Guardar())
                {
                    
                    MessageBox.Show("Registro guardado correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia, " +
                    "precioCompra, precioVenta FROM taller.producto"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else 
                {
                    MessageBox.Show(string.Format("Error\n{0}", producto.Error.ToString()) , "Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Se cancelo la edición");

            }
            limpiar();
        }

        private Boolean Validar()
        {
            Boolean r = true;
            if (txtIdProducto.Text == "")
            {
                MessageBox.Show("Escriba el codigo del producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdProducto.Focus();
                r = false;
            }
              if (txtNombreProducto.Text == "")
            {
                MessageBox.Show("Escriba el nombre del producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProducto.Focus();
                r = false;
            }
            
            if (producto.BuscarIdProducto(txtIdProducto.Text))
            {
                MessageBox.Show(string.Format("Ya existe el codigo del Producto\n{0}\t{1}", producto.IdProducto, producto.Nombre));
                r = false;
            }
            else if (producto.BuscarProducto(txtNombreProducto.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre del Producto\n{0}\t{1}", producto.IdProducto, producto.Nombre));

                r = false;
            }
            else
                r = true;
            return r;

        }

        /// <summary>
        ///  Funcion para modificar los datos en le formulario
        /// </summary>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                producto.IdProducto = txtIdProducto.Text;
                producto.Nombre = txtNombreProducto.Text;
                producto.Categoria = Convert.ToInt32(cbxCategoria.SelectedValue);
                producto.Año = txtAño.Text;
                producto.Proveedor = Convert.ToInt32(cbxProveedor.SelectedValue);
                producto.Marca = txtMarca.Text;
                producto.Existencia = Convert.ToInt32(txtCantidad.Text);
                producto.PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text);
                //producto.PrecioCompra = Convert.ToDouble(txtPrecioCompra.Text);
                producto.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);

                if (producto.ModificarProducto())
                {
                    MessageBox.Show("Registro actualizado correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor,existencia, " +
                    "precioCompra, precioVenta FROM taller.producto"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", producto.Error.ToString()), "Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
                txtIdProducto.Focus();
                txtIdProducto.Enabled = true;

            }
        }

        private Boolean ValidarModificar()
        {
            Boolean r = true;
            if (txtIdProducto.Text == "")
            {
                MessageBox.Show("Escriba el codigo del producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdProducto.Focus();
                r = false;
            }
            else if (txtNombreProducto.Text == "")
            {
                MessageBox.Show("Escriba el nombre del Producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProducto.Focus();
                r = false;
            }
            else if (!producto.BuscarIdProducto(txtIdProducto.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo del Producto\n{0}\t{1}", producto.IdProducto, producto.Nombre));
                r = false;
            }
            else if (producto.Nombre == txtNombreProducto.Text)
            {
                MessageBox.Show(string.Format("Modificaste el producto \n{0}\t{1}", producto.IdProducto, producto.Nombre));
            }

            else if (producto.BuscarProducto(txtNombreProducto.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre del Producto\n{0}\t{1}", producto.IdProducto, producto.Nombre));

                r = false;
            }
            else
                r = true;
            return r;
        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (ValidarEliminar())
            {
                producto.IdProducto = (txtIdProducto.Text);
                if (producto.Eliminar())
                {
                    MessageBox.Show("Registro eliminado correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia, precioCompra, precioVenta FROM taller.producto"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", producto.Error.ToString()), "producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
                txtIdProducto.Focus();
                txtIdProducto.Enabled = true;

            }
        }

        private Boolean ValidarEliminar()
        {
            Boolean r = true;
            if (txtIdProducto.Text == "")
            {
                MessageBox.Show("Escriba el codigo del Producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdProducto.Focus();
                r = false;
            }
            else if (!producto.BuscarIdProducto(txtIdProducto.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo del producto\n{0}\t{1}", producto.IdProducto, producto.Nombre));
                r = false;
            }
            else
                r = true;
            return r;
        }

        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
