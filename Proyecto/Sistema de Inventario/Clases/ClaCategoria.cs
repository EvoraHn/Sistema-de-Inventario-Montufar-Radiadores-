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
    class ClaCategoria
    {
        private int idCategoria;
        private string nombre;
        private string descripcion;
        private ClaConexion conexion;
        private MySqlException error;

        public ClaCategoria()
        {
            idCategoria = 0;
            nombre = string.Empty;
            descripcion = string.Empty;
            conexion = new ClaConexion();
        }

        public ClaCategoria(string n, string d)
        {
            idCategoria = 0;
            nombre = n;
            descripcion = d;
            conexion = new ClaConexion();
        }

        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public Boolean Guardar()
        {
            if (conexion.IUD(string.Format("INSERT INTO categoria (nombre, descripcion) value('{0}','{1}')", Nombre, Descripcion)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean ModificarCategoria()
        {
            if (conexion.IUD(string.Format("UPDATE taller.categoria SET nombre='{0}', descripcion='{1}'  WHERE idCategoria={2}", Nombre, Descripcion, IdCategoria)))
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
            if (conexion.IUD(string.Format("DELETE FROM taller.categoria WHERE idcategoria={0}", IdCategoria)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }
        public Boolean BuscarIdCategoria(string id)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria where idcategoria= '{0}'", id));
            if (t1.Rows.Count > 0)
            {
                IdCategoria = Convert.ToInt32(t1.Rows[0][0].ToString());
                Nombre = t1.Rows[0][1].ToString();
                Descripcion = t1.Rows[0][2].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuscarCategoria(string ca)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria where nombre= '{0}'", ca));
            if (t1.Rows.Count > 0)
            {
                IdCategoria = Convert.ToInt32(t1.Rows[0][0].ToString());
                Nombre = t1.Rows[0][1].ToString();
                Descripcion = t1.Rows[0][2].ToString();
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
