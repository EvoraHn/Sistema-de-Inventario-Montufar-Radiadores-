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
    class ClaListaInventario
    {
        private List<ClaInventario> inventario;
        private ClaConexion conexion;
        private DataTable tabla;

        public ClaListaInventario()
        {
            inventario = new List<ClaInventario>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            Cargar_Datos();
        }

        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idInventario, producto, existencia, precio FROM taller.inventario"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaInventario I = new ClaInventario();
                I.IdInventario = f.Field<int>(0);
                I.Producto = f.Field<string>(1);
                I.Existencia = f.Field<int>(2);
                I.Precio = f.Field<decimal>(3);
                inventario.Add(I);
            }
        }


        public List<ClaInventario> ListaInventario
        {
            get
            {
                return ListaInventario;
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
