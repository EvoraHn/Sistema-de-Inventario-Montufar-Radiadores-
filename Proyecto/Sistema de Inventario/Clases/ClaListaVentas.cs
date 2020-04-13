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
    class ClaListaVentas
    {
        private List<ClaVenta> ventas;
        private ClaConexion conexion;
        private DataTable tabla;

        public ClaListaVentas()
        {
            ventas = new List<ClaVenta>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            Cargar_Datos();
            //Cargar_DatosEncabezado();
            //Cargar_DatosDetalle();
        }


        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT ev.numeroFactura, ev.fecha, ev.cliente, dv.idDetalle, dv.encabezado, " +
                "dv.producto, dv.cantidad, dv.precio, dv.impuesto, dv.subTotal, dv.total FROM taller.encabezadoventa As ev " +
                "INNER JOIN taller.detalleventa AS dv ON ev.numeroFactura = dv.encabezado"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaVenta V = new ClaVenta();
                V.Encabezado = f.Field<int>(0);
                V.Fecha = f.Field<DateTime>(1);
                V.Cliente = f.Field<string>(2);
                V.IdDetalle = f.Field<int>(3);
                V.NumeroFactura = f.Field<int>(4);
                V.IDProducto = f.Field<string>(5);
                V.Cantidad = f.Field<int>(6);
                V.Precio = f.Field<decimal>(7);
                V.Impuesto = f.Field<decimal>(8);
                V.Subtotal = f.Field<decimal>(9);
                V.Total = f.Field<decimal>(10);
                ventas.Add(V);
            }
        }

        /*
        public void Cargar_DatosEncabezado()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT numeroFactura, fecha, cliente FROM taller.encabezadoVenta"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaVenta V = new ClaVenta();
                V.NumeroFactura = f.Field<int>(0);
                V.Fecha = f.Field<DateTime>(1);
                V.Cliente = f.Field<string>(2);
                ventas.Add(V);
            }
        }

        public void Cargar_DatosDetalle()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idDetalle, encabezado, producto, cantidad, precio, impuesto, subtotal, total FROM taller.detaleVenta"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaVenta V = new ClaVenta();
                V.IdDetalle = f.Field<int>(0);
                V.Encabezado = f.Field<int>(1);
                V.IDProducto = f.Field<string>(2);
                V.Cantidad = f.Field<int>(3);
                V.Precio = f.Field<decimal>(4);
                V.Impuesto = f.Field<decimal>(5);
                V.Subtotal = f.Field<decimal>(6);
                V.Total = f.Field<decimal>(7);

                ventas.Add(V);
            }
        }
        */
        public List<ClaVenta> ListaVentas
        {
            get
            {
                return ListaVentas;
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
