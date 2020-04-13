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
    class ClaListaCompras
    {
        private List<ClaCompra> compras;
        private ClaConexion conexion;
        private DataTable tabla;

        public ClaListaCompras()
        {
            compras = new List<ClaCompra>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            //Cargar_DatosEncabezado();
            //Cargar_DatosDetalle();
            Cargar_Datos();
        }

        /// Encabezado Compra

        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT e.numeroFactura, e.proveedor, e.fecha, e.facturaProveedor, d.idDetalleCompra , " +
                "d.encabezadoCompra, d.producto, d.cantidad, d.precio, d.impuesto,d.subTotal,d.total FROM taller.encabezadocompra " +
                "As e INNER JOIN taller.detallecompra AS d ON e.numeroFactura = d.encabezadocompra"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaCompra Cm = new ClaCompra();
                Cm.Encabezado = f.Field<int>(0);
                Cm.Proveedor = f.Field<int>(1);
                Cm.Fecha = f.Field<DateTime>(2);
                Cm.FacturaProveedor = f.Field<string>(3);
                Cm.IdDetalleCompra = f.Field<int>(4);
                Cm.NumeroFactura = f.Field<int>(5);
                Cm.Producto = f.Field<string>(6);
                Cm.Cantidad = f.Field<int>(7);
                Cm.Precio = (f.Field<decimal>(8));
                Cm.Impuesto = (f.Field<decimal>(9));
                Cm.SubTotal = (f.Field<decimal>(10));
                Cm.Total = (f.Field<decimal>(11));
                compras.Add(Cm);
            }
        }



        /*public void Cargar_DatosEncabezado()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT numeroFactura, proveedor, fecha FROM taller.encabezadoCompra"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaCompra Cm= new ClaCompra();
                Cm.NumeroFactura = f.Field<string>(0);
                Cm.Proveedor = f.Field<int>(1);
                Cm.Fecha = f.Field<DateTime>(2);
                compras.Add(Cm);
            }
        }*/

        /*public void Cargar_DatosDetalle()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idDetalleCompra, encabezadoCompra, producto, cantidad," +
                "precio, impuesto, subtotal, total  FROM taller.detalleCompra"));
            foreach (DataRow f in tabla.Rows)
            {
                
                ClaCompra Cm = new ClaCompra();
                Cm.IdDetalleCompra = f.Field<int>(0);
                Cm.EncabezadoCompra = f.Field<string>(1);
                Cm.Producto = f.Field<string>(2);
                Cm.Cantidad = f.Field<int>(3);
                Cm.Precio = Convert.ToInt32(f.Field<Decimal>(4));
                Cm.Impuesto = (f.Field<Decimal>(5));
                Cm.SubTotal = Convert.ToInt32(f.Field<Decimal>(6));
                Cm.Total = Convert.ToInt32(f.Field<Decimal>(7));
                compras.Add(Cm);
            }
        }
        */
        public List<ClaCompra> ListaCompras
        {
            get
            {
                return ListaCompras;
            }
        }
        public DataTable Tabla
        {
            get { return tabla; }
        }
        public DataTable SQL(string sql)
        {
            DataTable t = conexion.consulta(sql);
            return t;
        }

    }
}
