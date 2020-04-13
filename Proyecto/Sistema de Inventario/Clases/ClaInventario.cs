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
    class ClaInventario
    {
        private int idInventario;
        private string producto;
        private int existencia;
        private decimal precio;
        private decimal precioVenta;
        private ClaConexion conexion;
        private MySqlException error;

        public ClaInventario()
        {
            idInventario = 0;
            producto = string.Empty;
            existencia = 0;
            precio = 0;
            precioVenta= 0;
            conexion = new ClaConexion();
        }

        public ClaInventario(int c, string p, int e, decimal pre, decimal preVen)
        {
            idInventario = c;
            producto = p;
            existencia = e;
            precio = pre;
            precioVenta = preVen;
            conexion = new ClaConexion();
        }

        public int IdInventario
        {
            get { return idInventario; }
            set { idInventario = value; }
        }

        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        public int Existencia
        {
            get { return existencia; }
            set { existencia = value; }
        }

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        public decimal PrecioVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }
        public Boolean BuscarProducto(string id)
        {
            DataTable t1 = conexion.consulta(string.Format("SELECT idInventario, producto, existencia, precio, precioVenta FROM taller.inventario where producto='{0}'", id));
            if (t1.Rows.Count > 0)
            {
                idInventario = Convert.ToInt32(t1.Rows[0][0].ToString());
                producto = t1.Rows[0][1].ToString();
                existencia = Convert.ToInt32(t1.Rows[0][2].ToString());
                precio = Convert.ToInt32(t1.Rows[0][3].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean GuardarInventario()
        {
         
            if (conexion.IUD(string.Format("INSERT INTO  taller.inventario(producto, existencia, precio,precioVenta) " +
                "value('{0}', {1}, {2}, {3})",Producto,Existencia,Precio,PrecioVenta)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }

        public Boolean ModificarInventario()
        {
            
            if (conexion.IUD(string.Format("UPDATE taller.inventario SET existencia = {0}, precio = {1} , precioVenta = {2} " +
                "WHERE idInventario={3} ", Existencia,Precio, PrecioVenta, IdInventario)))
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
            if (conexion.IUD(string.Format("DELETE FROM taller.inventario WHERE producto={0}", Producto)))
            {
                return true;
            }
            else
            {
                error = conexion.Error;
                return false;
            }
        }
        public MySqlException Error
        {
            get { return error; }
        }
    }
}
