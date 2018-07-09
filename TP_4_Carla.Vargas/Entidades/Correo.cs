using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entidades
{
    public class Correo:IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> listPaquetes;

        public List<Paquete> Paquetes {
            get { return this.listPaquetes; }
            set { this.listPaquetes = value; }
        }

        public Correo()
        {
            this.mockPaquetes = new List<Thread>();
            this.listPaquetes = new List<Paquete>();
        }

        public void FinEntregas()
        {
            foreach (Thread item in this.mockPaquetes)
            {
                if(item.IsAlive)
                    item.Abort();   
            }
        }
            
        public string MostrarDatos(IMostrar<List<Paquete>> elementos) 
        {
            StringBuilder sb = new StringBuilder();
            if (elementos is Correo)
            {
                foreach (Paquete item in listPaquetes)
                {
                    sb.Append(item.TrackingId.ToString() + " ");
                    sb.Append(item.DireccionEntrega.ToString() + " ");
                    sb.Append("(" + item.Estado.ToString() + ").\n");
                }
            }
            return sb.ToString();

        }
        public static Correo operator +(Correo c, Paquete p)
        {
            foreach (var item in c.listPaquetes)
            {
                if (p == item)
                {
                    throw new TrackingRepetidoException("El paquete se encuentra en la lista");
                }
            }
            if (c != p ) 
                c.listPaquetes.Add(p);
            try
            {
                Thread hiloCicloDeVida = new Thread(p.MockCicloVida);
                c.mockPaquetes.Add(hiloCicloDeVida);
                hiloCicloDeVida.Start();
                return c;   
            }
            catch (Exception ex)//atrapo la excepcion del mockCiclodeVida (no se pudo insertar en la base de datos)
            {
                
                throw ex;
            }
         
        }

        public static bool operator ==(Correo c, Paquete p)
        {
            foreach (Paquete item in c.listPaquetes)
            {
                if (p == item)
                    return true;                
            }
            return false;
        }
        public static bool operator !=(Correo c, Paquete p)
        {
            return !(c == p);
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
