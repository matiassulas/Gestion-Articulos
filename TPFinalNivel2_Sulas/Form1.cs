using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio1;
using Negocio;
using TPFinalNivel2_Sulas;

namespace TPFinalNivel2_Sulas
{
    public partial class Form1 : Form
    {
        private List <Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }

     //       private void Form1_Load(object sender, EventArgs e)
 //      {
        
 //       }

        private void cargar()
        {
            NegocioArticulo negocio = new NegocioArticulo();
            try
            {
                
                listaArticulo = negocio.listar();
                dgvform1.DataSource = listaArticulo;
                cargarImagen(listaArticulo[0].urlImagen);
                ocultarcolumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
          
        }
        private void ocultarcolumnas()
        {
            dgvform1.Columns["urlImagen"].Visible = false;
            dgvform1.Columns["Id"].Visible = false;
        }
      //  private void btnAgregar_Click(object sender, EventArgs e)
       // {
          
       // }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            cargar();
            
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Agregar agregar = new Agregar();
            agregar.ShowDialog();
            cargar();
            
        }

        private void dgvform1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvform1.CurrentRow != null)
            {
                Articulo articulo = (Articulo)dgvform1.CurrentRow.DataBoundItem;
                cargarImagen(articulo.urlImagen);
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxform1.Load(imagen);
            }
            catch (Exception )
            {

                pbxform1.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");
            } 
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo modificado;
                modificado = (Articulo)dgvform1.CurrentRow.DataBoundItem;
                Agregar modificar = new Agregar(modificado);
                modificar.ShowDialog();
                cargar();
            }
            catch (Exception )
            {

                MessageBox.Show("Debes seleccionar un articulo antes de modificar ");
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            NegocioArticulo articulo = new NegocioArticulo();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Eliminar articulo seleccionado","eliminando",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvform1.CurrentRow.DataBoundItem;
                    articulo.eliminar(seleccionado.id);
                    MessageBox.Show("Articulo eliminado exitosamente");
                    cargar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

      //  private void btnBuscar_Click(object sender, EventArgs e)
      // {
      // }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> articulos;
            string filtro = txtBuscar.Text;

            if (filtro != "")
            {
                articulos = listaArticulo.FindAll(X => X.categoria.descripcion.ToLower().Contains(filtro.ToLower()) || X.marca.descripcion.ToLower().Contains(filtro.ToLower()));
            }
            else
            { 
               articulos = listaArticulo;
            }

            dgvform1.DataSource = null;
            dgvform1.DataSource = articulos;
            ocultarcolumnas();
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo articulo;
                articulo = (Articulo)dgvform1.CurrentRow.DataBoundItem;
                MessageBox.Show("Codigo de articulo : " + articulo.codArticulo + " - Nombre de producto : " + articulo.nombre + " - Descripcion : " + articulo.descripcion + " - Marca : " + articulo.marca + " - Categoria : " + articulo.categoria + " - Precio : " + articulo.precio ) ;
                
            }
            catch (Exception )
            {

                MessageBox.Show("Debes seleccionar un articulo antes de ver sus detalles ");
            }
        }

        private void lblBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
