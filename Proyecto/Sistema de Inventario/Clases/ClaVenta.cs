using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms; 

namespace Sistema_de_Inventario.Clases
{
    class ClaVenta
    {
        /// Detalle Venta
        private int idDetalle;
        private int encabezado;
        private string idProducto;
        private int cantidad;
        private decimal precio;
        private decimal impuesto;
        private decimal subtotal;
        private decimal total;
        private int comodin;

        // Encabezado Venta
        private int numeroFactura;
        private DateTime fecha;
        private string cliente;

        private ClaConexion conexion;
        private MySqlException error;


        public ClaVenta()
        {
            encabezado = 25266;
            idProducto = string.Empty;
            cliente = "";
            cantidad = 0;
            precio = 0;
            impuesto = 0;
            subtotal = 0;
            total = 0;
            idDetalle = 0;
            numeroFactura = 0;
            fecha = DateTime.Today;
            conexion = new ClaConexion();
            

        }

        public ClaVenta(int Encabezado,string IDProducto,string Cliente,int Cantidad,decimal Precio,decimal Impuesto,decimal Subtotal,decimal Total, int IdDetalle,
            int Factura, DateTime Fecha)
        {

            encabezado = Encabezado;
            idProducto = IDProducto;
            cliente = Cliente;
            cantidad = Cantidad;
            precio = Precio;
            impuesto = Impuesto;
            subtotal = Subtotal;
            total = Total;
            idDetalle = IdDetalle;
            numeroFactura = Factura;
            fecha = Fecha;


            conexion = new ClaConexion();
        }
        public int Comodin
        {
            get => comodin;
            set
            {
                comodin = value;
            }
        }
        public int Encabezado
        {
            get => encabezado;
            set
            {
                encabezado = value;
            }
        }

        public string IDProducto
        {
            get => idProducto;
            set
            {
                idProducto = value;
            }
        }

        public string Cliente
        {
            get => cliente;
            set
            {
                cliente = value;
            }
        }

        public int Cantidad
        {
            get => cantidad;
            set
            {
                cantidad = value;
            }
        }

        public decimal Precio
        {
            get => precio;
            set
            {
                precio = value;
            }
        }

        public decimal Impuesto
        {
            get => impuesto;
            set
            {
                impuesto = value;
            }
        }

        public decimal Subtotal
        {
            get => subtotal;
            set
            {
                subtotal = value;
            }
        }

        public decimal Total
        {
            get => total;
            set
            {
                total = value;
            }
        }


        public int NumeroFactura
        {
            get { return numeroFactura; }
            set { value = numeroFactura;  }
        }


        public int IdDetalle
        {
            get { return idDetalle; }
            set { value = idDetalle; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { value = fecha; }
        }

        /// <summary>
        /// Genera un numero autoincremental al abrir una ventana Ventas o al presionar el botón Terminar Venta
        /// </summary>
        /// <returns></returns>
        public Boolean GenerarEncabezado()
        {
            //inserta en la base de datos el cliente con id 0 "no existe" 
            //lo que conlleva a que se genere un nuevo id que es lo que necesitamos 
            //MessageBox.Show("manda a generar a la base de datos");
            if (conexion.IUD(string.Format("INSERT INTO encabezadoventa (cliente) value('{0}')", 0)))
            {
                return true;
            }
            else
            {
                //en caso de error 
                error = conexion.Error;
                return false;
            }
        }

        public Boolean Guardar_Detalle_Venta()
        {
            //inserta en la base de datos el cliente con id 0 "no existe" 
            //lo que conlleva a que se genere un nuevo id que es lo que necesitamos 
            //MessageBox.Show(cantidad.ToString(), impuesto.ToString());
            Modificar();
            

            //if (conexion.IUD(string.Format("INSERT INTO detalleventa (encabezado,producto,cantidad,precio,impuesto,subTotal,total) value('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 90, "7676", 2,6, 7, 8,9)))
            if (conexion.IUD(string.Format("INSERT INTO detalleventa (encabezado,producto,cantidad,precio,impuesto,subTotal,total) value('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", Encabezado, IDProducto, Cantidad, Precio, Impuesto, Subtotal, Total)))
            {
                return true;
            }
            else
            {
                //en caso de error 
                error = conexion.Error;
                return false;
            }
        }
        /// <summary>
        /// Muestra una serie de registros (Detalles de Venta) de la venta actual (encabezado, que es generado al abrir la ventana)
        /// </summary>
        /// <returns></returns>
        public Boolean MostrarFacturacondetalles()
        {
            //selecciona y muestra todos los registros que se tengan insertados en el encabdezado de venta actual
            DataTable t1 = conexion.consulta(string.Format("SELECT * FROM taller.detalleventa where encabezado = {0}", encabezado));
            if (t1.Rows.Count > 0)
            {
                NumeroFactura = Convert.ToInt32(t1.Rows[0][0].ToString());
                Cliente = t1.Rows[0][1].ToString();
                Fecha = Convert.ToDateTime(t1.Rows[0][2].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Muestra numero de encabezado de Venta (se genera al abrir la ventana)
        /// </summary>
        public void MostarNumeroEncabezado()
        {
            //MessageBox.Show("manda MOSTRAR");
            //se instancia la clase conexión con el nombre numeroFactura
            ClaConexion NumeroFactura = new ClaConexion();
            //se le asigna un valor a encabezado , en este caso la función  obtener factura devuelve n por lo tanto
            //encabezado = n

            encabezado = NumeroFactura.ObtenerNumerodeFactura(string.Format("SELECT Max(numeroFactura) as numero FROM taller.encabezadoventa"));
            // se envia un parametro sql con string.Format para ejecutar la consulta
            //encabezado= NumeroFactura.ObtenerNumerodeFactura();
        }

        public Boolean Modificar()
        {
            if (conexion.IUD(string.Format("UPDATE encabezadoventa SET cliente = '{0}' WHERE numeroFactura = {1}", Cliente, Encabezado)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean IniciarEncabezado()
        {
            if (conexion.IUD(string.Format("INSERT INTO encabezadoventa ( cliente ) value('{0}')", 0)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }
        public Boolean Guardar()
        {
            if (conexion.IUD(string.Format("INSERT INTO encabezadoventa ( cliente, fecha ) value('{0}','{1}')", Cliente, Fecha)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean GuardarDetalle()
        {
            if (conexion.IUD(string.Format("INSERT INTO taller.detalleventa (idDetalle,encabezado, producto, cantidad,precio,impuesto,subtotal,total ) value('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", IdDetalle, Encabezado, IDProducto, Cantidad, Precio, Impuesto, Subtotal, Total)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        /// <summary>
        /// Elimina un Detalle venta del Datagrid de detalle de ventas (factura)
        /// </summary>
        /// <returns></returns>
        public Boolean Eliminar()
        {
            
            if (conexion.IUD(string.Format("DELETE FROM taller.detalleventa WHERE idDetalle = {0} ", comodin)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }
        /*
                public Boolean Modificar()
                {
                    if (conexion.IUD(string.Format("UPDATE encabezadoventa SET cliente = '{0}',  fecha = '{1}'  WHERE numeroFactura = {2}", Cliente, Fecha, NumeroFactura)))
                    {
                        return true;
                    }
                    else
                    {
                        error = conexion.Error;
                        return false;
                    }
                }

                public Boolean ModificarDetalleVenta()
                {
                    if (conexion.IUD(string.Format("UPDATE detalledoventa SET encabezado = {0}, producto = '{1}', cantidad = {2}, precio = {3}, " +
                        "impuesto = {4}  WHERE idDetalle = {5}", Encabezado, IDProducto, Cantidad, Precio, Impuesto, IdDetalle)))
                    {
                        return true;
                    }
                    else
                    {
                        error = conexion.Error;
                        return false;
                    }
                }*/

        public Boolean BuscarEncabezadoVenta(string no)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT numeroFactura, cliente, fecha FROM taller.encabezadoventa where numeroFactura= {0}", no));
            if (t1.Rows.Count > 0)
            {
                NumeroFactura = Convert.ToInt32(t1.Rows[0][0].ToString());
                Cliente = t1.Rows[0][1].ToString();
                Fecha = Convert.ToDateTime(t1.Rows[0][2].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuscarDetalleVenta(string pro)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idDetalle, encabezado, producto, " +
                "cantidad, precio, impuesto, subTotal, total FROM taller.detalleventa WHERE producto = '{0}'", pro));
            if (t1.Rows.Count > 0)
            {
                IdDetalle = Convert.ToInt32(t1.Rows[0][0].ToString());
                NumeroFactura = Convert.ToInt32(t1.Rows[0][1].ToString());
                IDProducto = t1.Rows[0][2].ToString();
                Cantidad = Convert.ToInt32(t1.Rows[0][3].ToString());
                Precio = Convert.ToDecimal(t1.Rows[0][4].ToString());
                Impuesto = Convert.ToDecimal(t1.Rows[0][5].ToString());
                Subtotal = Convert.ToDecimal(t1.Rows[0][6].ToString());
                Total = Convert.ToDecimal(t1.Rows[0][7].ToString());

                return true;
            }
            else
            {
                return false;
            }
        }

        public MySqlException Error
        {
            get { return error; }
        }

    }
}