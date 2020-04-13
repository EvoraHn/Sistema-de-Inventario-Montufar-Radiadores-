using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;



namespace Sistema_de_Inventario
{
    public partial class Categoria : Form
    {
        ClaConexion c;
        private Clases.ClaListaCategorias categorias;
        private ClaCategoria categoria;
        public Categoria()
        {
            InitializeComponent();
            c = new ClaConexion();
            categorias = new Clases.ClaListaCategorias();
            categoria = new ClaCategoria();
            txtIdCategoria.Enabled = false;

        }


        private void Categoria_Load(object sender, EventArgs e)
        {
            DataTable t1 = categorias.SQL(String.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria"));
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = t1;
            dataGridView2.Refresh();
            Cargar_Datos();
            DataGridLectura();


        }

        /// <summary>
        /// con esta funcion se mantiene  los datos del DataGrid Proveedores solo de lectura sin ponder modficar
        /// alguna columna de este
        /// </summary>
        public void DataGridLectura()
        {
            dataGridView2.Columns["idCategoria"].ReadOnly = true;
            dataGridView2.Columns["nombre"].ReadOnly = true;
            dataGridView2.Columns["descripcion"].ReadOnly = true;
        }
        private void Cargar_Datos()
        {
            txtIdCategoria.Text = categoria.IdCategoria.ToString();
            txtNombreCategoria.Text = categoria.Nombre;
            txtDescripcion.Text = categoria.Descripcion;
            txtNombreCategoria.Focus();
            txtIdCategoria.Enabled = false;
            
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void limpiar()
        {
            txtIdCategoria.Text = "";
            txtNombreCategoria.Text = "";
            txtDescripcion.Text = "";
            txtIdCategoria.Enabled = false;
            txtNombreCategoria.Focus();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                txtIdCategoria.Enabled = false;
                //categoria.IdCategoria = Convert.ToInt32(txtIdCategoria.Text);
                categoria.Nombre = txtNombreCategoria.Text;
                categoria.Descripcion = txtDescripcion.Text;
                if (categoria.Guardar())
                {
                    MessageBox.Show("Registro guardado correctamente", "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = categorias.SQL(String.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();

                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", categoria.Error.ToString()), "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            /*if (txtIdCategoria.Text == "")
            {
                MessageBox.Show("Escriba el codigo una Categoria", "Departamento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdCategoria.Focus();
                r = false;
            }
              if (txtNombreCategoria.Text == "")
            {
                MessageBox.Show("Escriba el nombre del departamento", "Departamento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCategoria.Focus();
                r = false;
            }
            */
            if (categoria.BuscarCategoria(txtIdCategoria.Text))
            {
                MessageBox.Show(string.Format("Ya existe el codigo de la categoria\n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));
                r = false;
            }
            else if (categoria.BuscarCategoria(txtNombreCategoria.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre de la categoria \n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));

                r = false;
            }
            else
                r = true;
            return r;

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (ValidarModificar())
            {
                categoria.IdCategoria = Convert.ToInt32(txtIdCategoria.Text.ToString());
                categoria.Nombre = txtNombreCategoria.Text;
                categoria.Descripcion = txtDescripcion.Text;
                if (categoria.ModificarCategoria())
                {
                    MessageBox.Show("Registro actulizado correctamente", "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = categorias.SQL(String.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", categoria.Error.ToString()), "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
            }

        }

        private Boolean ValidarModificar()
        {
            Boolean r = true;

            if (txtNombreCategoria.Text == "")
            {
                MessageBox.Show("Escriba el nombre de la categoria", "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreCategoria.Focus();
                r = false;
            }
            else if (!categoria.BuscarIdCategoria(txtIdCategoria.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo de la categoria\n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));
                r = false;
            }

            else if (categoria.Nombre == txtNombreCategoria.Text)
            {
                MessageBox.Show(string.Format("Modificaste la categoria \n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));
            }

            else if (categoria.BuscarCategoria(txtNombreCategoria.Text))
            {
                MessageBox.Show(string.Format("Ya existe el nombre de la categoria \n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));

                r = false;
            }
           
            /*else if (!categoria.BuscarCategoria(txtNombreCategoria.Text))
            {
                if (MessageBox.Show(string.Format("Ya existe el nombre del categoria con este nombre\n{0}\t{1}\n¿Desea Continuar?", categoria.IdCategoria, categoria.Nombre), "Modificar Departamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (ValidarEliminar())
            {
                categoria.IdCategoria = Convert.ToInt32(txtIdCategoria.Text);
                if (categoria.Eliminar())
                {
                    MessageBox.Show("Registro eliminado correctamente", "Categoria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable t1 = categorias.SQL(String.Format("SELECT idCategoria, nombre, descripcion FROM taller.categoria"));
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = t1;
                    dataGridView2.Refresh();
                    Cargar_Datos();
                }
                else
                {
                    MessageBox.Show(string.Format("Error\n{0}", categoria.Error.ToString()), "Departamento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                limpiar();
            }
        }

        private Boolean ValidarEliminar()
        {
            Boolean r = true;
            if (txtIdCategoria.Text == "")
            {
                MessageBox.Show("Escriba el codigo del departamento", "Departamento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdCategoria.Focus();
                r = false;
            }
            else if (!categoria.BuscarIdCategoria(txtIdCategoria.Text))
            {
                MessageBox.Show(string.Format("No existe el codigo del departamento\n{0}\t{1}", categoria.IdCategoria, categoria.Nombre));
                r = false;
            }
            else
                r = true;
            return r;
        }
        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            txtIdCategoria.Enabled = false;
            txtIdCategoria.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            txtNombreCategoria.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();


        }

        private void textBox6_TextChanged(object sender, EventArgs e) { }

        private void textBox5_TextChanged(object sender, EventArgs e) { }

        private void textBox4_TextChanged(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e) { }

        private void panel5_Paint(object sender, PaintEventArgs e) { }

        private void panel3_Paint(object sender, PaintEventArgs e) { }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
