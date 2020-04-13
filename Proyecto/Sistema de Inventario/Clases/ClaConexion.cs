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
    class ClaConexion
    {
        private MySqlConnection conexion;
        private string bd;
        private string servidor;
        private string usuario;
        private string pass;
        private MySqlException error;

        public ClaConexion()
        {
            bd = "taller";
            servidor = "127.0.0.1";
            usuario = "Taller";
            pass = "desarrollo01";
            conexion = new MySqlConnection();
        }
        public ClaConexion(string b, string se, string u, string p)
        {
            bd = b;
            servidor = se;
            usuario = u;
            pass = p;
            conexion = new MySqlConnection();
        }
        public void conectar()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.ConnectionString = string.Format("Database={0};Server={1};Uid={2};Pwd={3};SslMode=none", bd, servidor, usuario, pass);
                    conexion.Open();
                    
                }
                else
                {
                    conexion.Close();
                    conexion.ConnectionString = string.Format("Database={0};Server={1};Uid={2};Pwd={3};SslMode=none", bd, servidor, usuario, pass);
                    conexion.Open();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(string.Format("Error: \n{0}", ex.ToString()), "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public DataTable consulta(string sql)
        {
            DataTable t = new DataTable();
            conectar();
            MySqlCommand comando = conexion.CreateCommand();
            MySqlDataAdapter adptador = new MySqlDataAdapter();
            try
            {
                comando.Connection = conexion;
                comando.CommandText = sql;
                adptador.SelectCommand = comando;
                adptador.Fill(t);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(string.Format("Error: \n{0}", ex.ToString()), "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return t;
        }
        public Boolean IUD(string sql)
        {
            conectar();
            MySqlCommand comando = conexion.CreateCommand();
            try
            {
                comando.Connection = conexion;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                error = ex;
                return false;
            }
        }

        //recibe un comando sql para ejecutarlo
        //esta seccion de codigo se puede reutilizar para Compras y Ventas ya que es la misma dinámica
        public int ObtenerNumerodeFactura(string sql)
        {
            ClaConexion numero = new ClaConexion();
            //abrir la conexión
            conectar();
            //esta será la variable para enviarla como parámetro
            int n = 0;
            try
            {
                // Query de selección
                //string sql = @"SELECT Max(numeroFactura) as numero FROM taller.encabezadoventa ";



                // Establecer el comando SQL
                MySqlCommand sqlCommand = new MySqlCommand(sql, conexion);
                //se ejecuta un reader para leer solamamente el campo que queremos
                using (MySqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    //busca el valor con el número que se necesita
                    while (rdr.Read())
                    {
                        //lo almacena dentro de la variable n que es string
                        n = Convert.ToInt32(rdr["numero"]);
                        //MessageBox.Show(rdr["numero"].ToString());
                    }
                }
                //retorna n a la funcion dentro d ela clase compra o venta
                return n;
            }
            //captura cualquier error que pueda generarse, asi el programa no se detendrá
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
            finally
            {
                // Cerrar la conexión
                conexion.Close();
            }

        }

        public MySqlException Error
        {
            get { return error; }
        }
    }
}
