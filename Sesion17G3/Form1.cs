using Sesion17G3.Formularios;

namespace Sesion17G3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MostrarFecha();
            timer1.Enabled = true;
            timer1.Start();

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductoFrm frm = new ProductoFrm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estás seguro que deseas salir de la aplicación?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MostrarFecha()
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void MostrarHora()
        {
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MostrarHora();
        }
    }
}
