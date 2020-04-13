using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Inventario
{
    public partial class Proveedores : Form
    {
        ClaConexion c;
        private Clases.ClaListaProveedores proveedores;
        private Clases.ClaProveedor proveedor;
        public Proveedores()
        {
            InitializeComponent();
            c = new ClaConexion();
            proveedores = new Clases.ClaListaProveedores();
            proveedor = new Clases.ClaProveedor();

            txtIdProveedor.Enabled = false;
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            DataTable t1 = proveedores.SQL(String.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
                "direccion, correoElectronico FROM taller.proveedor"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();

            DataGridLectura();

        }

        /// <summary>
        /// con esta funcion se mantiene  los datos del DataGrid Proveedores solo de lectura sin ponder modficar
        /// alguna columna de este
        /// </summary>
        public void DataGridLectura()
        {
            dataGridView2.Columns["idProveedor"].ReadOnly = true;
            dataGridView2.Columns["RTNProveedor"].ReadOnly = true;
            dataGridView2.Columns["nombre"].ReadOnly = true;
            dataGridView2.Columns["telefono"].ReadOnly = true;
            dataGridView2.Columns["direccion"].ReadOnly = true;
            dataGridView2.Columns["correoElectronico"].ReadOnly = true;
        }

        private void Cargar_Datos()
        {
            txtIdProveedor.Text = proveedor.p.ToString();
            txtRTN.Text = proveedor.r;
            txtNombreProveedor.Text = proveedor.n;
            txtTelefono.Text = proveedor.t;
            txtDireccion.Text = proveedor.d;
            txtCorreo.Text = proveedor.c;
            txtRTN.Focus();
            txtIdProveedor.Enabled = false;

        }
        private void limpiar()
        {
            txtIdProveedor.Text = "";
            txtRTN.Text = "";
            txtNombreProveedor.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtCorreo.Text = "";
            txtIdProveedor.Enabled = false;
            txtRTN.Focus();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                txtIdProveedor.Enabled = false;
                //proveedor.p = Convert.ToInt32(txtIdProveedor.Text);
                proveedor.r = txtRTN.Text;
                proveedor.n = txtNombreProveedor.Text;
                proveedor.t = txtTelefono.Text;
                proveedor.d = txtDireccion.Text;
                proveedor.c = txtCorreo.Text;
                
                if (proveedor.Guardar())
                {
                    MessageBox.Show("Registro guardado correctamente", "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = proveedores.SQL(String.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
                                "direccion, correoElectronico FROM taller.proveedor"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();

                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", proveedor.Error.ToString()), "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            else
            {
                MessageBox.Show("Se cancelo la edición");

            }
            limpiar();
        }

        private Boolean Validar()
        {
            Boolean r = true;
            
             if (txtNombreProveedor.Text == "")
            {
                MessageBox.Show("Escriba el nombre del Proveedor", "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProveedor.Focus();
                r = false;
            }
            
            if (proveedor.BuscarIdProveedor(txtIdProveedor.Text))
            {
                MessageBox.Show(string.Format("Ya existe el codigo del Proveedor\n{0}\t{1}", proveedor.p, proveedor.n));
                r = false;
            }
            else if (proveedor.BuscarProveedor(txtNombreProveedor.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre del Proveedor\n{0}\t{1}", proveedor.p, proveedor.n));

                r = false;
            }
            else if (proveedor.BuscarRTNProveedor(txtRTN.Text))
            {
                MessageBox.Show(string.Format("Ya existe este RTN del Proveedor\n{0}\t{1}", proveedor.p, proveedor.n));

                r = false;
            }
            else
                r = true;
            return r;

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e){}

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                proveedor.p = Convert.ToInt32(txtIdProveedor.Text.ToString());
                proveedor.r = txtRTN.Text;
                proveedor.n = txtNombreProveedor.Text;
                proveedor.t = txtTelefono.Text;
                proveedor.d = txtDireccion.Text;
                proveedor.c = txtCorreo.Text;
                if (proveedor.ModificarProveedor())
                {
                    MessageBox.Show("Registro actualizado correctamente", "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = proveedores.SQL(String.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
                                "direccion, correoElectronico FROM taller.proveedor"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", proveedor.Error.ToString()), "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
            }
        }

        private Boolean ValidarModificar()
        {
            Boolean r = true;

            if (txtNombreProveedor.Text == "")
            {
                MessageBox.Show("Escriba el nombre de la Proveedor", "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProveedor.Focus();
                r = false;
            }
            else if (!proveedor.BuscarIdProveedor(txtIdProveedor.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo de la categoria\n{0}\t{1}", proveedor.p, proveedor.n));
                r = false;
            }

            else if (proveedor.n == txtNombreProveedor.Text || proveedor.r == txtRTN.Text)
            {
                MessageBox.Show(string.Format("Modificaste un Proveedor \n{0}\t{1}", proveedor.p, proveedor.n));
            }
            else if (proveedor.BuscarProveedor(txtNombreProveedor.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre del Proveedor\n{0}\t{1}", proveedor.p, proveedor.n));

                r = false;
            }
            else if (proveedor.BuscarRTNProveedor(txtRTN.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre del Proveedor\n{0}\t{1}", proveedor.p, proveedor.n));

                r = false;
            }

            /*
            else if (proveedor.BuscarProveedor(txtNombreProveedor.Text))
            {
                if (MessageBox.Show(string.Format("Ya existe el nombre un proveedor con este nombre\n{0}\t{1}\n¿Desea Continuar?", proveedor.p, proveedor.n),
                    "Modificar Proveedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    r = true;
                }
                else
                {
                    r = false;
                }
            }*/
            else
                r = true;
            return r;
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtIdProveedor.Enabled = false;
            txtIdProveedor.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtRTN.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtNombreProveedor.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtTelefono.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtDireccion.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            txtCorreo.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (ValidarEliminar())
            {
                proveedor.p = Convert.ToInt32(txtIdProveedor.Text);
                if (proveedor.Eliminar())
                {
                    MessageBox.Show("Registro eliminado correctamente", "Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = proveedores.SQL(String.Format("SELECT idProveedor, RTNProveedor, nombre, telefono, " +
                                "direccion, correoElectronico FROM taller.proveedor"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", proveedor.Error.ToString()), "Departamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
            }
        }

        private Boolean ValidarEliminar()
        {
            Boolean r = true;
            if (txtIdProveedor.Text == "")
            {
                MessageBox.Show("Escriba el codigo del Producto", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdProveedor.Focus();
                r = false;
            }
            else if (!proveedor.BuscarIdProveedor(txtIdProveedor.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo del departamento\n{0}\t{1}", proveedor.p, proveedor.n));
                r = false;
            }
            else
                r = true;
            return r;
        }
    }
}
