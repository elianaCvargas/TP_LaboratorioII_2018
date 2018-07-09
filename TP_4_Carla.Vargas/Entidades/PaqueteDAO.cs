using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class PaqueteDAO
    {
        private static SqlCommand comando;
        private static SqlConnection conexion;

        static PaqueteDAO()
        {
             conexion = new SqlConnection(Properties.Settings.Default.CadenaConexion);
            // CREO UN OBJETO SQLCOMMAND
            comando = new SqlCommand();
            // INDICO EL TIPO DE COMANDO
            comando.CommandType = System.Data.CommandType.Text;
            // ESTABLEZCO LA CONEXION
            comando.Connection = conexion;
        }
        public static bool Insertar(Paquete p)
        {           
                                  //"INSERT INTO Paquetes (direccionEntrega,TrackingId,alumno,nombre,apellido) VALUES(";
            string sql = "INSERT INTO Paquetes (direccionEntrega,TrackingID,alumno) VALUES('"  + p.DireccionEntrega.ToString() + "'," + "'" + p.TrackingId.ToString() + "',"  + "'Carla Eliana Vargas'" +")";
            //sql = sql + "'" + e.Legajo + "'," + e.Salario + "," + (int)e.PuestoJerarquico + ",'" + e.Nombre + "','" + e.Apellido + "')";

            try
            {
                comando.CommandText = sql;
                conexion.Open();
                comando.ExecuteNonQuery();                
            }
            catch (Exception e)
            {

                return false;
            }
            finally
            {
               // if (todoOk)
                   conexion.Close();
            }
            return true;
        } 
    }
}
