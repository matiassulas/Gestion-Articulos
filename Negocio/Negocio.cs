using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio1;
using System.Data;

namespace Negocio
{
    public class NegocioArticulo
    {
        public void modificar(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo,Nombre = @nombre , Descripcion = @descripcion, IdMarca = @idmarca , IdCategoria = @idcategoria, ImagenUrl = @imgUrl, Precio= @precio where Id = @id ;");
                datos.setearParametros("@id", articulo.id);
                datos.setearParametros("@codigo", articulo.codArticulo);
                datos.setearParametros("@nombre", articulo.nombre);
                datos.setearParametros("@descripcion", articulo.descripcion);
                datos.setearParametros("@idmarca", articulo.marca.id);
                datos.setearParametros("@idcategoria", articulo.categoria.id);
                datos.setearParametros("@imgUrl", articulo.urlImagen);
                datos.setearParametros("@precio", articulo.precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                datos.cerrarConeccion();
            }
        }

        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection coneccion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                coneccion.ConnectionString = "server =.\\SQLEXPRESS; database =CATALOGO_DB; integrated security = true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select a.Id,a.Codigo, a.Nombre, a.Descripcion, a.IdMarca, a.IdCategoria, a.ImagenUrl,a.Precio ,m.Descripcion  marca , c.Descripcion categoria from dbo.ARTICULOS a , dbo.MARCAS m , dbo.CATEGORIAS c WHERE m.Id = IdMarca and c.Id = IdCategoria ;";
                comando.Connection = coneccion;
                coneccion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)lector["Id"];
                    aux.codArticulo = (string)lector["Codigo"];
                    aux.nombre = (string)lector["Nombre"];
                    aux.descripcion = (string)lector["Descripcion"];
                    aux.marca = new Marca();
                    aux.marca.id = (int)lector["IdMarca"];
                    aux.marca.descripcion = (string)lector["marca"];
                    aux.categoria = new Categoria();
                    aux.categoria.id = (int)lector["IdCategoria"];
                    aux.categoria.descripcion = (string)lector["categoria"];

                    if(!(lector["ImagenUrl"] is DBNull))
                    aux.urlImagen = (string)lector["ImagenUrl"];

                    aux.precio = Convert.ToDecimal(lector["Precio"]);
                   
                    lista.Add(aux);
                }

                coneccion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void Agregar(Articulo nuevo) {
            AccesoDatos datos = new AccesoDatos();

            try
            {
              
                datos.setearConsulta("insert into ARTICULOS ( Codigo,Nombre , Descripcion , IdMarca  , IdCategoria , ImagenUrl, Precio) values (@codigo,@nombre,@descripcion,@idmarca,@idcategoria,@imgUrl,@precio) ;");
                datos.setearParametros("@idmarca",nuevo.marca.id);
                datos.setearParametros("@idcategoria", nuevo.categoria.id);
                datos.setearParametros("@nombre", nuevo.nombre);
                datos.setearParametros("@codigo", nuevo.codArticulo);
                datos.setearParametros("@descripcion", nuevo.descripcion);
                datos.setearParametros("@imgUrl", nuevo.urlImagen);
                datos.setearParametros("@precio", nuevo.precio);
                
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void eliminar(int id) 
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametros("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }




    }
}

