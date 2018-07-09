using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorreoUTN;
using Entidades;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Paquete p = new Paquete("direccionEntrega","DireccionSalida");
            PaqueteDAO.Insertar(p);
            //string texto = "Sarasa";
            //texto.Guarda("Archivo");

            
            Console.ReadKey();
        }
    }
}
