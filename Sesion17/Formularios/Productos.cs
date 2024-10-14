using Sesion17.Modelos;
using Sesion17.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesion17.Formularios
{
    public partial class Productos : Form
    {
        ProductoServicio productos;
        public Productos()
        {
            InitializeComponent();
            productos = new ProductoServicio();
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Producto prod = new Producto();
            prod.ID = int.Parse(tbID.Text);
            prod.Nombre = tbNombre.Text;
            prod.Descripcion = tbDescripcion.Text;
            prod.Precio = double.Parse(tbPrecio.Text);

            productos.AgregarProducto(prod);
            MostrarRegistros();

        }
        private void MostrarRegistros()
        {
            dgvRegistros.DataSource = null;
            dgvRegistros.DataSource = productos.ObtenerProductos();
        }
    }
}
