using Dominio1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;

namespace TPFinalNivel2_Sulas
{
    public partial class Agregar : Form
    {
        private Articulo articulo = null;
        public Agregar()
        {
            InitializeComponent();
            
        }
        public Agregar( Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
            cargarimagen(articulo.urlImagen);
        }
       
        private void cargarimagen(string imagen)
        {
            try
            {
          
                pcbImagen.Load(imagen);
            }
            catch (Exception)
            {

                pcbImagen.Load("https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg");
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAgregar2_Click(object sender, EventArgs e)
        {
             
            NegocioArticulo negocio = new NegocioArticulo();
            try
            {
                if (articulo == null)
                    articulo = new Articulo();
               
                articulo.codArticulo = txtCodArticulo.Text;
                articulo.nombre = txtNombre.Text;
                articulo.descripcion = txtDescripcion.Text;
                articulo.urlImagen = txtUrlImagen.Text;
                articulo.precio = decimal.Parse(txtPrecio.Text);
                articulo.marca = (Marca)cmbMarca.SelectedItem;
                articulo.categoria = (Categoria)cmbCategoria.SelectedItem;

                if (articulo.id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }
                
                Close();

            }
            catch (Exception)
            {

                MessageBox.Show("Por favor completa todos los campos");
            }
        }

        private void Agregar_Load(object sender, EventArgs e)
        {
            Marcanegocio marca = new Marcanegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();
            try
            {
                cmbCategoria.DataSource = categoria.listar();
                cmbCategoria.ValueMember = "id";
                cmbCategoria.DisplayMember = "descripcion";
                cmbMarca.DataSource = marca.Listar();
                cmbMarca.ValueMember = "id";
                cmbMarca.DisplayMember = "descripcion";

                if (articulo != null)
                {
                    txtCodArticulo.Text = articulo.codArticulo;
                    txtNombre.Text = articulo.nombre;
                    txtDescripcion.Text = articulo.descripcion;
                    txtPrecio.Text = articulo.precio.ToString();
                    txtUrlImagen.Text = articulo.urlImagen;
                    cmbMarca.SelectedValue = articulo.marca.id;
                    cmbCategoria.SelectedValue = articulo.categoria.id;


                }
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTitulo2_Click(object sender, EventArgs e)
        {

        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string img = txtUrlImagen.Text;
                cargarimagen(img);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            { 
                txtUrlImagen.Text = ofd.FileName;
                cargarimagen(ofd.FileName);

                // no ejecuta guardar foto

              
            }
            
        }
    }
}
