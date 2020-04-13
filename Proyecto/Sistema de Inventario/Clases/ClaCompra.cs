using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    class ClaCompra
    {
        //----- Encabezado Compra
        private int numeroFactura;
        private int proveedor;
        private DateTime fecha;
        private string facturaProveedor;
        ////-------Detalle Compra
        private int idDetalleCompra;
        private int encabezado;
        private string producto;
        private int cantidad;
        private decimal precio;
        private decimal impuesto;
        private decimal subTotal;
        private decimal total;

        private int comodin;
        private string comodin2;

        private int comodinID;

        private ClaConexion conexion;
        private MySqlException error;

        //private MySqlParameter parametro;


        public ClaCompra()
        {
            numeroFactura = 0;
            proveedor = 0;
            fecha = DateTime.Today;
            facturaProveedor = string.Empty;
            idDetalleCompra = 0;
            encabezado = 0;
            producto = string.Empty;
            cantidad = 0;
            precio =  0;
            impuesto = 0;
            subTotal = 0;
            total =0;
            conexion = new ClaConexion();

            //parametro precio  = new parametro

           
        }

        public ClaCompra(int noFac, int pro, DateTime fe, string facPro, int idDetalle, int encaCompra, string produc, int can,
            decimal pre, decimal imp, decimal subt, decimal tot)
        {
            numeroFactura = noFac;
            proveedor = pro;
            fecha = fe;
            facturaProveedor = facPro;

            idDetalleCompra = idDetalle;
            encabezado = encaCompra;
            producto = produc;
            cantidad = can;
            precio = pre;
            impuesto = imp;
            subTotal = subt;
            total = tot;
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

        public int ComodinID
        {
            get => comodinID;
            set
            {
                comodinID = value;
            }
        }
        public string Comodin2
        {
            get => comodin2;
            set
            {
                comodin2 = value;
            }
        }

        public int NumeroFactura
        {
            get { return numeroFactura; }

            set { value = numeroFactura; }
        }

        public int Proveedor
        {
            get { return proveedor; }
            set { value = proveedor; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { value = fecha; }
        }


        public string FacturaProveedor
        {
            get { return facturaProveedor; }

            set { value = facturaProveedor; }
        }

        ///----------------
        public int IdDetalleCompra
        {
            get => idDetalleCompra;
            set
            {
                idDetalleCompra = value;
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

        public string Producto
        {
            get => producto;
            set
            {
                producto = value;
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

        public decimal SubTotal
        {
            get => subTotal;
            set
            {
                subTotal = value;
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


        /// <summary>
        /// Genera un numero autoincremental al abrir una ventana Ventas o al presionar el botón Terminar Venta
        /// </summary>
        /// <returns></returns>
        public Boolean GenerarEncabezadoCompra()
        {
            //MessageBox.Show("LLega a la Segunda etapa");
            //inserta en la base de datos el cliente con id 0 "no existe" 
            //lo que conlleva a que se genere un nuevo id que es lo que necesitamos 
            //MessageBox.Show("manda a generar a la base de datos");
            if (conexion.IUD(string.Format("INSERT INTO taller.encabezadocompra (proveedor) value('{0}')", 7)))
            //if (conexion.IUD(string.Format("INSERT INTO taller.encabezadocompra (facturaProveedor) value('{0}')", 0)))
            {
                //MessageBox.Show("LLega a la tercera etapa");
                return true;
            }
            else
            {
               // MessageBox.Show("LLega a la tecera etapa error");
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
            DataTable t1 = conexion.consulta(string.Format("SELECT * FROM taller.detallecompra where encabezadocompra = {0}", encabezado));
            if (t1.Rows.Count > 0)
            {
                NumeroFactura = Convert.ToInt32(t1.Rows[0][0].ToString());
                Proveedor = Convert.ToInt32(t1.Rows[0][1].ToString());
                Fecha = Convert.ToDateTime(t1.Rows[0][2].ToString());
                FacturaProveedor = t1.Rows[0][3].ToString();
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

            encabezado= NumeroFactura.ObtenerNumerodeFactura(string.Format("SELECT Max(numeroFactura) as numero FROM taller.encabezadocompra"));
            // se envia un parametro sql con string.Format para ejecutar la consultas
            //encabezado= NumeroFactura.ObtenerNumerodeFactura();
        }


        public Boolean GuardarDetalleCompra()
        {
            if (conexion.IUD(string.Format("INSERT INTO taller.detallecompra (encabezadoCompra, producto, cantidad, precio, impuesto, subTotal,total ) value('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",Encabezado, Producto, Cantidad, Precio, Impuesto, SubTotal, Total)))
            {
                return true;
            }
            else
            {
                //MessageBox.Show("El error es aqui");
                error = conexion.Error;
                return false;
            }
        }

        public Boolean ModificarEncabezado()
        {
           //ssageBox.Show()
            if (conexion.IUD(string.Format("UPDATE encabezadocompra SET proveedor = {0}, facturaProveedor = '{1}' WHERE numeroFactura ={2}", Comodin, Comodin2, Encabezado)))
                //if (conexion.IUD(string.Format("UPDATE encabezadocompra SET proveedor = {0}, facturaProveedor = '{1}' WHERE numeroFactura ={2}", Proveedor, FacturaProveedor, Encabezado)))
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
            if (conexion.IUD(string.Format("INSERT INTO taller.encabezadocompra (numeroFactura, proveedor ) " +
                "value('{0}','{1}') ","5622",1)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }


        public Boolean GuardarEncabezado()
        {
            if (conexion.IUD(string.Format("INSERT INTO encabezadocompra (numeroFactura, proveedor, fecha, facturaProveedor ) " +
                "value('{0}','{1}','{2}','{3}')", NumeroFactura,Proveedor,Fecha, FacturaProveedor)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

      

        public Boolean Eliminar()
        {
            if (conexion.IUD(string.Format("DELETE FROM taller.detallecompra WHERE idDetalleCompra= {0}", ComodinID)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }
        /*public Boolean ModificarDetalle()
        {
            if (conexion.IUD(string.Format("UPDATE detalleCompra SET  encabezadoCompra = '{0}',  producto = '{1}', cantidad = {2}, precio = {3}, " +
                "impuesto = {4}, subTotal = {5}, total = {6} WHERE idDetalleCompra={7}", EncabezadoCompra, Producto, Producto, Cantidad, Precio, 
                Impuesto, SubTotal, Total, IdDetalleCompra)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }*/

        public Boolean BuscarEncabezado(string no)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT numeroFactura, proveedor, fecha, " +
                "facturaProveedor FROM taller.encabezadoCompra where numeroFactura='{0}'", no));
            if (t1.Rows.Count > 0)
            {
                Encabezado= Convert.ToInt32(t1.Rows[0][0].ToString());
                Proveedor = Convert.ToInt32(t1.Rows[0][1].ToString());
                Fecha = Convert.ToDateTime(t1.Rows[0][2].ToString());
                FacturaProveedor = t1.Rows[0][0].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuscarDetalleCompra(string enca)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idDetalleCompra, encabezadoCompra, producto, " +
                "cantidad, precio, impuesto, subTotal, total FROM taller.detallecompra WHERE producto = '{0}'", enca));
            if (t1.Rows.Count > 0)
            {
                IdDetalleCompra = Convert.ToInt32(t1.Rows[0][0].ToString());
                NumeroFactura = Convert.ToInt32(t1.Rows[0][1].ToString());
                Producto = t1.Rows[0][2].ToString();
                Cantidad = Convert.ToInt32(t1.Rows[0][3].ToString());
                Precio = Convert.ToDecimal(t1.Rows[0][4].ToString());
                Impuesto = Convert.ToDecimal(t1.Rows[0][5].ToString());
                SubTotal = Convert.ToDecimal(t1.Rows[0][6].ToString());
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

