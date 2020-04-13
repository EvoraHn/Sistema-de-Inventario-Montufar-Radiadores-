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
    public partial class Compras : Form
    {
        ClaConexion c;
        private Clases.ClaListaProductos productos;
        private Clases.ClaProducto producto;

        private Clases.ClaListaCompras compras;
        private ClaCompra compra;

        private Clases.ClaListaProveedores proveedores;
        private Clases.ClaProveedor proveedor;

        private Clases.ClaListaInventario unInventario;
        private ClaInventario inventario;

        public Compras()
        {
            InitializeComponent();
            
            c = new ClaConexion();
            productos = new Clases.ClaListaProductos();
            producto = new Clases.ClaProducto();

            proveedores = new Clases.ClaListaProveedores();
            proveedor = new Clases.ClaProveedor();

            compras = new Clases.ClaListaCompras();
            compra = new ClaCompra();

            unInventario = new Clases.ClaListaInventario();
            inventario = new ClaInventario();

            GenerarEncabezadoCompra();
            Bloqueartxt();
            Limpiar();

        }

        private void Compras_Load(object sender, EventArgs e)
        {
            DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia," +
            "precioCompra,precioVenta FROM taller.producto"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DataTable t2 = compras.SQL(String.Format("SELECT e.numeroFactura, e.proveedor, e.fecha, e.facturaProveedor, d.idDetalleCompra," +
           " d.producto, d.cantidad, d.precio, d.impuesto,d.subTotal, d.total FROM taller.encabezadocompra " +
           "As e INNER JOIN taller.detallecompra AS d ON e.numeroFactura = d.encabezadoCompra"));
            //DataTable t2 = compras.SQL(String.Format("SELECT * FROM taller.vistacompraproducto;"));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t2;
            dataGridView1.Refresh();

            Datos_DataGrid();
            txtEncabezado.Enabled = false;
            DataGridLectura();

        }

        public void Datos_DataGrid()
        {

            DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia," +
            "precioCompra,precioVenta FROM taller.producto"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DataTable t2 = compras.SQL(String.Format("SELECT e.numeroFactura, e.proveedor, e.fecha, e.facturaProveedor, d.idDetalleCompra," +
            " d.producto, d.cantidad, d.precio, d.impuesto,d.subTotal, d.total FROM taller.encabezadocompra " +
            "As e INNER JOIN taller.detallecompra AS d ON e.numeroFactura = d.encabezadoCompra"));
            //DataTable t2 = compras.SQL(String.Format("SELECT * FROM taller.vistacompraproducto;"));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t2;
            dataGridView1.Refresh();


        }

        /// <summary>
        ///  Se Desabilitaron los botones textbox cuando se le de doble click al datagrid
        /// </summary>
        public void DeshabilidarTextBox()
        {
            dtpFecha.Text = compra.Fecha.ToString();
            txtFacturaProveedor.Enabled = false;
            txtProve.Text = compra.Proveedor.ToString();
            txtProducto.Text = compra.Producto;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtImpuesto.Enabled = false;
            txtSubTotal.Enabled = false;
            txtTotal.Enabled = false;

        }

        /// <summary>
        ///  Se Habilitaron los textbox  al eliminar una compra
        /// </summary>
        public void HabilidarTextBox()
        {
            dtpFecha.Text = compra.Fecha.ToString();
            txtFacturaProveedor.Enabled = true;
            //txtProve.Text = compra.Proveedor.ToString();
            txtProducto.Text = compra.Producto;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtImpuesto.Enabled = true;
            txtSubTotal.Enabled = true;
            txtTotal.Enabled = true;

        }

        private void LimpiarAlgunosCampos()
        {
            dtpFecha.Value = DateTime.Today;
            txtEncabezado.Focus();
            txtTotal.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtBuscarProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            txtImpuesto.Text = string.Empty;
            txtProducto.Text = string.Empty;
        }
        public void DataGridLectura()
        {
            /// Para el datagrid de productos
            dataGridView2.Columns["idProducto"].ReadOnly = true;
            dataGridView2.Columns["nombre"].ReadOnly = true;
            dataGridView2.Columns["categoria"].ReadOnly = true;
            dataGridView2.Columns["marca"].ReadOnly = true;
            dataGridView2.Columns["año"].ReadOnly = true;
            dataGridView2.Columns["proveedor"].ReadOnly = true;
            dataGridView2.Columns["existencia"].ReadOnly = true;
            dataGridView2.Columns["precioCompra"].ReadOnly = true;
            dataGridView2.Columns["precioVenta"].ReadOnly = true;

            /// Para el DataGrid de compras
            dataGridView1.Columns["numeroFactura"].ReadOnly = true;
            dataGridView1.Columns["proveedor"].ReadOnly = true;
            dataGridView1.Columns["fecha"].ReadOnly = true;
            dataGridView1.Columns["facturaProveedor"].ReadOnly = true;
            dataGridView1.Columns["idDetalleCompra"].ReadOnly = true;
            dataGridView1.Columns["producto"].ReadOnly = true;
            dataGridView1.Columns["cantidad"].ReadOnly = true;
            dataGridView1.Columns["precio"].ReadOnly = true;
            dataGridView1.Columns["impuesto"].ReadOnly = true;
            dataGridView1.Columns["subTotal"].ReadOnly = true;
            dataGridView1.Columns["total"].ReadOnly = true;

        }

        private void Cargar_Datos()
        {
            txtEncabezado.Text = compra.Encabezado.ToString();
            dtpFecha.Text = compra.Fecha.ToString();
            txtFacturaProveedor.Text = compra.FacturaProveedor;
            txtProve.Text = compra.Proveedor.ToString();

            txtEncabezado.Text = compra.NumeroFactura.ToString();
            txtProducto.Text = compra.Producto;
            txtCantidad.Text = compra.Cantidad.ToString();
            txtPrecio.Text = compra.Precio.ToString();
            txtImpuesto.Text = compra.Impuesto.ToString();
            txtSubTotal.Text = compra.SubTotal.ToString();
            txtTotal.Text = compra.Total.ToString();

            txtEncabezado.Focus();
            SendKeys.Send("{Tab}");
        }
        /// <summary>
        /// deja en blanco todos los txt menos el encabezado de Venta
        /// </summary>
        private void Limpiar()
        {
            dtpFecha.Value = DateTime.Today;
            txtEncabezado.Focus();
            txtTotal.Text = string.Empty;
            txtFacturaProveedor.Text = string.Empty;
            txtProve.Text = string.Empty;
            txtBuscarProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            txtImpuesto.Text = string.Empty;
            txtProve.Text = string.Empty;
            txtProducto.Text = string.Empty;
        }
        /// <summary>
        /// bloquea los txt especificados para que no se pueda modificar su valor manualmente
        /// </summary>
        /// 

        private void Bloqueartxt()
        {
            txtEncabezado.Enabled = false;
            txtPrecio.Enabled = true;
            txtSubTotal.Enabled = true;
            txtTotal.Enabled = true;
            txtProducto.Enabled = false;
            txtProve.Enabled = false;


        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeshabilidarTextBox();
            DataGridLectura();
            txtEncabezado.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtProve.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dtpFecha.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtFacturaProveedor.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            //compra.ComodinID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            txtProducto.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtCantidad.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtPrecio.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtImpuesto.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtSubTotal.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            txtTotal.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtProducto.Enabled = false;
            txtProve.Enabled = false;
            txtProducto.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtProve.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            txtPrecio.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
        }

        private void label13_Click(object sender, EventArgs e) { }

        public void actualizarInventario()
        {
            
            if (verificacion())
            {
                string sql = "";

                sql = string.Format("UPDATE taller.producto AS inv INNER JOIN taller.detallecompra AS dv ON  " +
                "inv.idProducto = dv.producto SET inv.existencia = inv.existencia + dv.cantidad  WHERE inv.idProducto ='{0}'", compra.Producto);
                c.IUD(sql);
            }

        }
        public Boolean verificacion()
        {
            Boolean r = true;
            if (txtCantidad.Text != "")
            {
                return true;
            }
            else
                r = true;
            return r;

        }

        /// <summary>
        ///  Boton de agregar donde se guardan los parametros correspondientes al los textBox
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                compra.Encabezado = Convert.ToInt32(txtEncabezado.Text);
                compra.Producto = txtProducto.Text;
                compra.Cantidad = Convert.ToInt32(txtCantidad.Text);
                compra.Precio = Convert.ToDecimal(txtPrecio.Text);
                compra.Impuesto = Convert.ToDecimal(txtImpuesto.Text);
                compra.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
                compra.Total = Convert.ToDecimal(txtTotal.Text);

                if (compra.GuardarDetalleCompra())
                {
                    //MessageBox.Show("Se agrego un producto a la compra", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datos_DataGrid();
                    txtEncabezado.Enabled = false;
                    txtFacturaProveedor.Enabled = false;
                    MessageBox.Show(txtProve.Text);
                    actualizarInventario();
                    DataGridLectura();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", compra.Error.ToString()), "Compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Se cancelo la edición");

            }
            LimpiarAlgunosCampos();
        }


        private Boolean Validar()
        {
            Boolean r = true;
            if (txtFacturaProveedor.Text == "")
            {
                MessageBox.Show("Escriba la factura del proveedor", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFacturaProveedor.Focus();
                r = false;
            }
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Escriba el nombre del producto", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProducto.Focus();
                r = false;
            }
            else
                r = true;
            return r;
        }

        public void modificar_encabezado()
        {
            if (verificar())
            {
                compra.Encabezado = Convert.ToInt32(txtEncabezado.Text);
                compra.Comodin= Convert.ToInt32(txtProve.Text);
                MessageBox.Show(txtFacturaProveedor.Text);
                if (compra.ModificarEncabezado())
                {
                    MessageBox.Show("Compra Realizada Correcta");
                    //txtCliente.Text = venta.Cliente;
                    //cargar_Venta_Actual();
                    //GenerarEncabezado();
                }
                else
                {
                    MessageBox.Show("no se modifico el encabezado");
                }
            }
            Limpiar();
        }

        public Boolean verificar()
        {
            Boolean r = true;
            if (txtProve.Text == "")
            {
                MessageBox.Show("Escriba algo en la caja de texto", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProve.Focus();
                r = false;
            }
            else
                r = true;
            return r;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HabilidarTextBox();
            modificar_encabezado();
            GenerarEncabezadoCompra();
            Datos_DataGrid();
        }

        private Boolean ValidarEncezado()
        {
            Boolean r = true;
            if (txtFacturaProveedor.Text == "")
            {
                MessageBox.Show("Escriba la factura del proveedor", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFacturaProveedor.Focus();
                r = false;
            }
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Escriba el nombre del producto", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProducto.Focus();
                r = false;
            }
            else
                r = true;
            return r;
        }

        private void GenerarEncabezadoCompra()
        {
            
            Limpiar();
            compra.GenerarEncabezadoCompra();
            compra.MostarNumeroEncabezado();
            txtEncabezado.Text = compra.Encabezado.ToString();
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        { 
            try
            {
                decimal p, c, i;
                c = Convert.ToInt32(txtCantidad.Text);
                p = Convert.ToDecimal(txtPrecio.Text);
                i = (c * p);
                txtSubTotal.Text = i.ToString();
            }
            catch
            {
                MessageBox.Show("Debe Ingrese los valores en orden");
            }
            
        }

        private void txtImpuesto_TextChanged(object sender, EventArgs e) { }

        private void txtImpuesto_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal im, su, to;
                im = Convert.ToDecimal(txtImpuesto.Text) / 100;
                su = Convert.ToDecimal(txtSubTotal.Text);
                to = (im + su);
                txtTotal.Text = to.ToString();
            }
            catch
            {
                MessageBox.Show("Debe Ingresar los valores en orden");
            }
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string sql = "";

            if (Filtro())
            {
                sql = string.Format("SELECT * FROM taller.producto WHERE proveedor LIKE '" + txtBuscarProducto.Text + "'");
            }
            else
            {
                MessageBox.Show("Se cancelo la edición");

            }

            DataTable t1 = productos.SQL(sql);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (ValidarEliminar())
            {
                compra.ComodinID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                if (compra.Eliminar())
                {
                    MessageBox.Show("Registro eliminado correctamente", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Datos_DataGrid();
                    txtFacturaProveedor.Enabled = true;
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", compra.Error.ToString()), "producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Limpiar();
                HabilidarTextBox();
                GenerarEncabezadoCompra();
                //txtIdProducto.Focus();
            }
        }
        private Boolean ValidarEliminar()
        {
            Boolean r = true;
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Seleccion el producto que desea eliminar", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                r = false;
            }
            else if (!compra.BuscarDetalleCompra((txtProducto.Text)))
            {
                MessageBox.Show(string.Format("No existe el codigo del detalle compra\n{0}\t{1}", compra.NumeroFactura, compra.Producto));
                r = false;
            }
            else
                r = true;
            return r;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void txtNumeroFactura_TextChanged(object sender, EventArgs e){}
        private void txtProducto_TextChanged(object sender, EventArgs e) { }
        private void pictureBox6_Click(object sender, EventArgs e) { }
    }
}

