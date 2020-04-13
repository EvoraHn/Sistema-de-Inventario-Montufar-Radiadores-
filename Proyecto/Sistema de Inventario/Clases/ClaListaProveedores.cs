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
    class ClaListaProveedores
    {
        private List<ClaProveedor> proveedores;
        private ClaConexion conexion;
        private DataTable tabla;

        public ClaListaProveedores()
        {
            proveedores = new List<ClaProveedor>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            Cargar_Datos();
        }

        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
                "direccion, correoElectronico FROM taller.proveedor"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaProveedor P = new ClaProveedor();
                P.p = f.Field<int>(0);
                P.r = f.Field<string>(1);
                P.n = f.Field<string>(2);
                P.t = f.Field<string>(3);
                P.d = f.Field<string>(4);
                P.c = f.Field<string>(5);
                proveedores.Add(P);
            }
        }
        public List<ClaProveedor> ListaProveedores
        {
            get
            {
                return ListaProveedores;
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
