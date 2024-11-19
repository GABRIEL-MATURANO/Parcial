using CRUDProductos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial
{
    public partial class MainForm : Form
    {
        private Productos productos = new Productos();

        public MainForm()
        {
            InitializeComponent();
            ActualizarProductos();
        }

        private void ActualizarProductos()
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productos.GetAll();
        }

        private Producto ObtenerProductoDesdeFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrWhiteSpace(txtStock.Text))
            {
                throw new Exception("Todos los campos son obligatorios.");
            }

            return new Producto
            {
                ID = int.Parse(txtID.Text),
                Nombre = txtNombre.Text,
                Precio = decimal.Parse(txtPrecio.Text),
                Stock = int.Parse(txtStock.Text)
            };
        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Producto producto = ObtenerProductoDesdeFormulario();
                productos.Insert(producto);

                MessageBox.Show("Producto agregado correctamente.");
                LimpiarCampos();
                ActualizarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Producto producto = ObtenerProductoDesdeFormulario();
                productos.Update(producto);

                MessageBox.Show("Producto actualizado correctamente.");
                LimpiarCampos();
                ActualizarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtID.Text);
                productos.Delete(id);

                MessageBox.Show("Producto eliminado correctamente.");
                LimpiarCampos();
                ActualizarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

   
    }
}