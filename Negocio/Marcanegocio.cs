using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio1;

namespace Negocio
{
    public class Marcanegocio
    {
        public List<Marca> Listar()
        {
			List<Marca> lista = new List<Marca>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("select id, descripcion from MARCAS");
				datos.ejecutarlectura();

				while (datos.Lector.Read())
				{
					Marca aux = new Marca();
					aux.id = (int)datos.Lector["id"];
					aux.descripcion = (string)datos.Lector["descripcion"];
					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}

			finally { 
				datos.cerrarConeccion();
			}
        }
    }
}
