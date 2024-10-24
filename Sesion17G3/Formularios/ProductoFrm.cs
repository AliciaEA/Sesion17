using Sesion17G3.Archivos;
using Sesion17G3.Modelos;
using Sesion17G3.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesion17G3.Formularios
{
    public partial class ProductoFrm : Form
    {
        ProductoServicio productos;
        Producto productoSel = new Producto();

        public ProductoFrm()
        {
            InitializeComponent();
            productos = new ProductoServicio(); //Buenas Practicas
        }

        //Para Agregar los Datos Ingresados
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto prod = new Producto();
                prod.ID = int.Parse(TbID.Text);
                prod.Nombre = TbNombre.Text;
                prod.Descripcion = TbDescripcion.Text;
                prod.Precio = double.Parse(TbPrecio.Text);
                productos.AgregarProducto(prod);
                MostrarRegistros();
                CleanAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void MostrarRegistros()
        {
            DgvRegistros.DataSource = null;
            DgvRegistros.DataSource = productos.Productos();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto prod = new Producto();
                prod.ID = int.Parse(TbID.Text);
                prod.Nombre = TbNombre.Text;
                prod.Descripcion = TbDescripcion.Text;
                prod.Precio = double.Parse(TbPrecio.Text);
                productos.ActualizarProducto(prod, productoSel.ID);
                MostrarRegistros();
                DesactivarBotones();
                CleanAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    productos.EliminarProducto(productoSel);

                    MostrarRegistros();
                    CleanAll();
                    DesactivarBotones();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvRegistros_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewRow index = DgvRegistros.CurrentRow;
            if (index != null)
            {
                AsignarProducto(index);
                TbID.Text = productoSel.ID.ToString();
                TbNombre.Text = productoSel.Nombre;
                TbDescripcion.Text = productoSel.Descripcion;
                TbPrecio.Text = productoSel.Precio.ToString();
                ActivarBotones();
            }
        }
        private void CleanAll()
        {
            TbNombre.Clear();
            TbDescripcion.Clear();
            TbPrecio.Clear();
            TbID.Clear();
            TbID.Focus();

        }
        private void ActivarBotones()
        {
            //Generalmente ocurre cuando se SELECCIONA un registro existente para su edición. 
            //Al habilitar los botones de edición y eliminación, el usuario puede realizar acciones como editar o eliminar el registro seleccionado, y claro no lo guarda repetitivamente.
            BtnEditar.Enabled = true;
            BtnEliminar.Enabled = true;
            BtnGuardar.Enabled = false;
        }

        private void DesactivarBotones()
        {
            //Se llama cuando se necesita deshabilitar los botones de edición y eliminación, y habilitar el botón de guardar.
            //Generalmente ocurre cuando se está agregando un NUEVO REGISTRO.
            //Al deshabilitar los botones de edición y eliminación, se evita que el usuario realice acciones no válidas antes de guardar el nuevo registro.
            BtnEditar.Enabled = false;
            BtnEliminar.Enabled = false;
            BtnGuardar.Enabled = true;
        }



        private void AsignarProducto(DataGridViewRow row)
        {
            productoSel.ID = int.Parse(row.Cells[0].Value.ToString());
            productoSel.Nombre = row.Cells[1].Value.ToString();
            productoSel.Descripcion = row.Cells[2].Value.ToString();
            productoSel.Precio = double.Parse(row.Cells[3].Value.ToString());
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnGuardarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = ("Archivo DAT (*.dat)|*.dat");
                saveFileDialog1.Title = "Guardar Archivo";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ProductoArchivo archivo = new ProductoArchivo();
                    archivo.Guardar(productos.Productos(), saveFileDialog1.FileName /*Aqui le estoy mandando la ruta*/);
                    MessageBox.Show("Archivo guardado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbrirArchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Archivo DAT (*.dat)|*.dat| Todos los archivos (*.*)| *.*";
            /* configura el cuadro de diálogo de apertura de archivo para mostrar el directorio inicial en "C:" 
             * Filtra los archivos para mostrar solo archivos con la extensión ".dat" o todos los archivos.*/
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ruta = openFileDialog1.FileName;

                ProductoArchivo archivo = new ProductoArchivo();
                productos.AgregarProductos(archivo.CargarProductos(ruta));
                MostrarRegistros();
            }
            else
            {
                MessageBox.Show("No se seleccionó ningún archivo", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

