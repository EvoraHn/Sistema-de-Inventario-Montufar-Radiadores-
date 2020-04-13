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
    public partial class Ventas : Form
    {
        ClaConexion c;
        private Clases.ClaListaProductos productos;
        private Clases.ClaListaVentas ventas;

        private Clases.ClaListaInventario unInventario;
        private ClaInventario inventario;
        private Clases.ClaVenta venta;
        public Ventas()
        {
            InitializeComponent();
            c = new ClaConexion();
            productos = new Clases.ClaListaProductos();
            ventas = new Clases.ClaListaVentas();
            venta = new Clases.ClaVenta();

            unInventario = new Clases.ClaListaInventario();
            inventario = new ClaInventario();

            dtpFechaVenta.Enabled = false;
            GenerarEncabezado();
            Bloqueartxt();
            Limpiar();
        }

        /// <summary>
        /// deja en blanco todos los txt menos el encabezado de Venta
        /// </summary>
        private void Limpiar()
        {
            dtpFechaVenta.Value = DateTime.Today;
            txtEncabezadoVenta.Focus();
            txtTotal.Text = string.Empty;
            txtBuscarProducto.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            txtImpuesto.Text = string.Empty;
            txtCliente.Text = string.Empty;
            txtProducto.Text = string.Empty;

        }

        /// <summary>
        /// deja en blanco la mayoria de los campos excepto el client 
        /// </summary>
        private void LimpiarAlgunosCampos()
        {
            dtpFechaVenta.Value = DateTime.Today;
            txtEncabezadoVenta.Focus();
            txtTotal.Text = string.Empty;
            txtBuscarProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtSubTotal.Text = string.Empty;
            txtImpuesto.Text = string.Empty;
            txtProducto.Text = string.Empty;
        }
        /// <summary>
        /// bloquea los txt especificados para que no se pueda modificar su valor manualmente
        /// </summary>
        private void Bloqueartxt()
        {
            txtEncabezadoVenta.Enabled = false;
            txtPrecio.Enabled = true;
            txtSubTotal.Enabled = true;
            txtTotal.Enabled = true;

        }
        private void Ventas_Load_1(object sender, EventArgs e)
        {
            DataTable t1 = productos.SQL(String.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor FROM taller.producto"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DataTable t2 = ventas.SQL(String.Format("SELECT ev.numeroFactura, ev.fecha, ev.cliente, dv.idDetalle, " +
             "dv.producto, dv.cantidad, dv.precio, dv.impuesto, dv.subTotal, dv.total FROM taller.encabezadoventa As ev " +
            "INNER JOIN taller.detalleventa AS dv ON ev.numeroFactura = dv.encabezado where ev.numeroFactura = {0}", venta.Encabezado));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t2;
            dataGridView1.Refresh();

            cargar_Venta_Actual();

            dtpFechaVenta.Enabled = false;
            dtpFechaVenta.Enabled = false;
            DataGridLectura();


            //txtNumeroFactura.Text = venta.NumeroFactura.ToString();

        }
        /// <summary>
        ///  Se Desabilitaron los botones textbox cuando se le de doble click al datagrid
        /// </summary>
        public void DeshabilidarTextBox()
        {
            dtpFechaVenta.Text = venta.Fecha.ToString();
            dtpFechaVenta.Enabled = false;
            txtCliente.Enabled = false;
            txtProducto.Enabled = false;
            txtProducto.Text = venta.IDProducto;
            txtCantidad.Enabled = false;
            txtPrecio.Enabled = false;
            txtImpuesto.Enabled = false;
            txtSubTotal.Enabled = false;
            txtTotal.Enabled = false;

        }


        /// <summary>
        ///  Se Desabilitaron los botones textbox cuando se le de doble click al datagrid
        /// </summary>
        public void habilidarTextBox()
        {
            dtpFechaVenta.Text = venta.Fecha.ToString();
            txtCliente.Enabled = true;
            txtProducto.Enabled = true;
            txtProducto.Text = venta.IDProducto;
            txtCantidad.Enabled = true;
            txtPrecio.Enabled = true;
            txtImpuesto.Enabled = true;
            txtSubTotal.Enabled = true;
            txtTotal.Enabled = true;

        }
        /// <summary>
        ///  Coloca el dataGrid para que solo este en modo lectura 
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
        private void cargar_Venta_Actual()
        {
            /*DataTable t2 = ventas.SQL(String.Format("SELECT ev.numeroFactura, ev.fecha, ev.cliente, " +
               "dv.producto, dv.cantidad, dv.precio, dv.impuesto, dv.subTotal, dv.total FROM taller.encabezadoventa As ev " +
               "INNER JOIN taller.detalleventa AS dv ON ev.numeroFactura = dv.encabezado where ev.numeroFactura = {0}", venta.Encabezado));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t2;
            dataGridView1.Refresh();*/

            DataTable t2 = ventas.SQL(String.Format("SELECT ev.numeroFactura, ev.fecha, ev.cliente, dv.idDetalle, " +
            "dv.producto, dv.cantidad, dv.precio, dv.impuesto, dv.subTotal, dv.total FROM taller.encabezadoventa As ev " +
            "INNER JOIN taller.detalleventa AS dv ON ev.numeroFactura = dv.encabezado"));
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = t2;
            dataGridView1.Refresh();

            DataTable t1 = unInventario.SQL(String.Format("SELECT * FROM taller.producto"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

        }

        /// <summary>
        /// Envia los datis necesarios a la clase venta para generar el detalle de venta
        /// </summary>
        private void enviar_Datos_para_detalle_Venta()
        {
            venta.Encabezado = Convert.ToInt32(txtEncabezadoVenta.Text);
            venta.IDProducto = txtProducto.Text;
            venta.Cantidad = Convert.ToInt32(txtCantidad.Text);
            venta.Precio = Convert.ToDecimal(txtPrecio.Text);
            venta.Impuesto = Convert.ToDecimal(txtImpuesto.Text);
            venta.Subtotal = Convert.ToDecimal(txtSubTotal.Text);
            venta.Total = Convert.ToDecimal(txtTotal.Text);


            //Cargar_Datos();
            venta.Guardar_Detalle_Venta();
            actualizarInventario();

            cargar_Venta_Actual();
            LimpiarAlgunosCampos();
            //Limpiar();
        }

        public void actualizarInventario()
        {
            if (verificacion())
            {
                string sql = "";

                sql = string.Format("UPDATE taller.producto AS inv INNER JOIN taller.detalleventa AS dv ON  " +
                "inv.idProducto = dv.producto SET inv.existencia = inv.existencia - dv.cantidad  WHERE inv.idProducto ='{0}'", venta.IDProducto);
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
      
        private void Cargar_Datos()
        {

            //txtEncabezadoVenta.Text = venta.NumeroFactura.ToString();
            dtpFechaVenta.Text = venta.Fecha.ToString();
            txtCliente.Text = venta.Cliente;

            //txtEncabezadoVenta.Text = venta.Encabezado.ToString();
            txtProducto.Text = venta.IDProducto;
            txtCantidad.Text = venta.Cantidad.ToString();
            txtPrecio.Text = venta.Precio.ToString();
            txtImpuesto.Text = venta.Impuesto.ToString();
            txtSubTotal.Text = venta.Subtotal.ToString();
            txtTotal.Text = venta.Total.ToString();

            txtEncabezadoVenta.Focus();
            SendKeys.Send("{Tab}");
        }
       
        private void Ventas_Load(object sender, EventArgs e){}

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e){}

        private void button1_Click(object sender, EventArgs e)
        {
            txtCliente.Enabled = true;
            modificar_encabezado();
            GenerarEncabezado();
            cargar_Venta_Actual();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void textBox5_TextChanged(object sender, EventArgs e){}

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtProducto.Enabled = false;
            txtProducto.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtPrecio.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DeshabilidarTextBox();

            txtEncabezadoVenta.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dtpFechaVenta.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtCliente.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //venta.IdDetalle = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            txtProducto.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtCantidad.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtPrecio.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtImpuesto.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtSubTotal.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtTotal.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            enviar_Datos_para_detalle_Venta();
            txtCliente.Enabled = false;
        }

        private Boolean Validar()
        {
            Boolean r = true;
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Escriba el nombre del cliente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCliente.Focus();
                r = false;
            }
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Escriba el nombre del producto", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                venta.Encabezado = Convert.ToInt32(txtEncabezadoVenta.Text);
                venta.Cliente = txtCliente.Text;

                if (venta.Modificar())
                {
                    MessageBox.Show("Venta reazalizada efectivamente");
                    //txtCliente.Text = venta.Cliente;
                    //cargar_Venta_Actual();
                    //GenerarEncabezado();
                }
                else
                {
                    MessageBox.Show("no se realizo la venta");
                }
            }
            Limpiar();
        }

        public Boolean verificar()
        {
            Boolean r = true;
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Escriba algo en la caja de texto", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscarProducto.Focus();
                r = false;
            }
            else
                r = true;
            return r;
        }


        private void GenerarEncabezado()
        {
            Limpiar();
            venta.GenerarEncabezado();
            venta.MostarNumeroEncabezado();
            txtEncabezadoVenta.Text = venta.Encabezado.ToString();
            //cargar_Venta_Actual();
            //MessageBox.Show("Manda a llamar");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            barra_buscar();
        }
        /// <summary>
        /// Busca un valor dentro de la base de datos y lo muestra si cumple alguna de las condiciones
        /// </summary>
        private void barra_buscar()
        {
            string sql = "";

            if (Filtro())
            {
                try
                {
                    sql = string.Format("SELECT * FROM taller.producto WHERE nombre LIKE '%" + txtBuscarProducto.Text + "%' or marca LIKE '%" + txtBuscarProducto.Text + "%'  or idProducto LIKE '%" + txtBuscarProducto.Text + "%'");
                }
                catch (Exception ex)
                {
                }

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
                    
                    MessageBox.Show("Escriba algo en la caja de texto", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscarProducto.Focus();
                    r = false;
                }
                else if (txtBuscarProducto.Text == "")
                {
                    DataTable t1 = unInventario.SQL(String.Format("Select * FROM taller.producto"));
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
                venta.IDProducto = (txtProducto.Text);
                venta.Comodin = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                if (venta.Eliminar())
                {
                    MessageBox.Show("Registro eliminado correctamente", "Compra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar_Venta_Actual();
                    //Datos_DataGrid();
                    //Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", venta.Error.ToString()), "producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Limpiar();
                habilidarTextBox();
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
            else if (!venta.BuscarDetalleVenta((txtProducto.Text)))
            {
                MessageBox.Show(string.Format("No existe el codigo del detalle compra\n{0}\t{1}", venta.NumeroFactura, venta.IDProducto));
                r = false;
            }
            else
                r = true;
            return r;
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
    }
}
