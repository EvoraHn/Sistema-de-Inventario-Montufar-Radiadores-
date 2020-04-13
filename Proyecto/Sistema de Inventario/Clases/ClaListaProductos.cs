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
    class ClaListaProductos
    {
        private List<ClaProducto> productos;
        private ClaConexion conexion;
        private DataTable tabla;

        public ClaListaProductos()
        {
            productos = new List<ClaProducto>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            Cargar_Datos();
        }

        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idProducto, nombre, categoria, marca, año, proveedor, existencia, precioCompra, precioVenta FROM taller.producto"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaProducto P = new ClaProducto();
                P.IdProducto = f.Field<string>(0);
                P.Nombre = f.Field<string>(1);
                P.Categoria = f.Field<int>(2);
                P.Marca = f.Field<string>(3);
                P.Año = f.Field<string>(4);
                P.Proveedor = f.Field<int>(5);
                P.Existencia = f.Field<int>(6);
                P.PrecioCompra =(f.Field<decimal>(7));
                P.PrecioVenta = (f.Field<decimal>(8));
                productos.Add(P);
            }
        }
        public List<ClaProducto> ListaProductos
        {
            get
            {
                return ListaProductos;
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
