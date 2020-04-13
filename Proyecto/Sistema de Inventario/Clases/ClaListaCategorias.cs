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
    class ClaListaCategorias
    {
        private List<ClaCategoria> categorias;
        private ClaConexion conexion;
        
        private DataTable tabla;

        public ClaListaCategorias()
        {
            categorias = new List<ClaCategoria>();
            conexion = new ClaConexion();
            tabla = new DataTable();
            Cargar_Datos();
        }

        public void Cargar_Datos()
        {
            conexion.conectar();
            tabla = conexion.consulta(string.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria"));
            foreach (DataRow f in tabla.Rows)
            {
                ClaCategoria C = new ClaCategoria();
                C.IdCategoria = f.Field<int>(0);
                C.Nombre = f.Field<string>(1);
                C.Descripcion = f.Field<string>(2);
                categorias.Add(C);
            }
        }
        public List<ClaCategoria> ListaCategorias
        {
            get
            {
                return ListaCategorias;
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
