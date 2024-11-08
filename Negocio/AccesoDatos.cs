using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection coneccion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector 
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            coneccion = new SqlConnection("server =.\\SQLEXPRESS; database =CATALOGO_DB; integrated security = true");
            comando = new SqlCommand();
        }


        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarlectura()
        {
            comando.Connection = coneccion;
            coneccion.Open();
            lector = comando.ExecuteReader();
        }

        public void cerrarConeccion() { 
            if(lector != null) 
                lector.Close(); 

            coneccion.Close(); 
        }

        public void ejecutarAccion()
        {
            comando.Connection = coneccion;

            try
            {
                coneccion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex ;
            }
        }

        public void setearParametros(string nombre ,object valor) 
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }
    }

   
}
