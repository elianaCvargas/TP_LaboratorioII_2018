using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class GuardaString
    {
        public static bool Guarda(this string texto, string archivo)
        {
            try
            {
                //obtenemos el path del directorio al cual queremos guardar
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + archivo;
                bool fileExist = File.Exists(path);
                if (fileExist)
                {
                    StreamWriter sw = new StreamWriter(path, true);//esto ya verifica si el archivo existe, si existe agrega los datos, sino lo crea.
                    sw.WriteLine(texto);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

    
    }
        //public bool leer(string archivo, out string datos)
        //{
        //    try
        //    {
        //        using (System.IO.StreamReader file = new System.IO.StreamReader(archivo))
        //        {
        //            datos = file.ReadToEnd();
        //        }

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        datos = "";
        //        return false;
        //    }
        //}

}
}