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
    class ClaProveedor
    {
        private int idProveedor;
        private string RTNProveedor;
        private string nombreProveedor;
        private string telefono;
        private string direccion;
        private string correoElectronico;

        private ClaConexion conexion;
        private MySqlException error;

        public ClaProveedor()
        {
            idProveedor = 0;
            RTNProveedor = "00000000000000";
            nombreProveedor = "";
            telefono = "00000000000";
            direccion = "";
            correoElectronico = "";
            conexion = new ClaConexion();
        }
        public ClaProveedor(int pro, string rt, string no, string te, string di, string co)
        {
            idProveedor = pro;
            RTNProveedor = rt;
            nombreProveedor = no;
            telefono = te;
            direccion = di;
            correoElectronico = co;
            conexion = new ClaConexion();
        }
        public int p
        {
            get { return idProveedor; }
            set { idProveedor = value; }
        }
        public string r
        {
            get { return RTNProveedor; }
            set { RTNProveedor = value; }
        }
        public string n
        {
            get { return nombreProveedor; }
            set { nombreProveedor = value; }
        }
        public string t
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string d
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string c
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }


        public Boolean Guardar()
        {
            if (conexion.IUD(string.Format("INSERT INTO taller.proveedor (RTNProveedor, nombre, telefono," +
                " direccion, correoElectronico) value('{0}','{1}','{2}','{3}', '{4}')", r, n, t, d, c)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean ModificarProveedor()
        {
            if (conexion.IUD(string.Format("UPDATE taller.proveedor SET RTNProveedor='{0}', nombre= '{1}', telefono ='{2}', direccion = '{3}', correoElectronico = '{4}'" +
                " WHERE idProveedor = {5}", r, n, t, d, c, p)))
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
            if (conexion.IUD(string.Format("DELETE FROM taller.proveedor WHERE idProveedor={0}", p)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean BuscarIdProveedor(string id)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, direccion, " +
                "correoElectronico FROM taller.proveedor where idProveedor='{0}'", id));
            if (t1.Rows.Count > 0)
            {
                p = Convert.ToInt32(t1.Rows[0][0].ToString());
                r = t1.Rows[0][1].ToString();
                n = (t1.Rows[0][2].ToString());
                t = (t1.Rows[0][3].ToString());
                d = (t1.Rows[0][4].ToString());
                c = (t1.Rows[0][5].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean BuscarProveedor(string nombreProveedor)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, direccion, " +
                "correoElectronico FROM taller.proveedor where nombre='{0}'", nombreProveedor));
            if (t1.Rows.Count > 0)
            {
                p = Convert.ToInt32(t1.Rows[0][0].ToString());
                r = t1.Rows[0][1].ToString();
                n = (t1.Rows[0][2].ToString());
                t = (t1.Rows[0][3].ToString());
                d = (t1.Rows[0][4].ToString());
                c = (t1.Rows[0][5].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean BuscarRTNProveedor(string rtnProveedor)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, direccion, " +
                "correoElectronico FROM taller.proveedor where RTNProveedor='{0}'", rtnProveedor));
            if (t1.Rows.Count > 0)
            {
                p = Convert.ToInt32(t1.Rows[0][0].ToString());
                r = t1.Rows[0][1].ToString();
                n = (t1.Rows[0][2].ToString());
                t = (t1.Rows[0][3].ToString());
                d = (t1.Rows[0][4].ToString());
                c = (t1.Rows[0][5].ToString());
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
