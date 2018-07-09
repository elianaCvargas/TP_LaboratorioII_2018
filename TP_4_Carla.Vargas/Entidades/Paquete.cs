using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;



namespace Entidades
{
    
     

    public class Paquete:IMostrar<Paquete>
    {
        public event DelegadoEstado InformarEstado;
        
        public  delegate void DelegadoEstado(object sender, EventArgs e);
        
        

        private string direccionEntrega;
        private EEstado estado;
        private string trackingId;

        public Paquete(string direccionEntrega, string trackingId)
        {
            this.DireccionEntrega = direccionEntrega;
            this.TrackingId = trackingId;
            this.Estado = EEstado.Ingresado;
        }
        public string DireccionEntrega
        {
            get { return this.direccionEntrega; }
            set { this.direccionEntrega = value; }
        }

        public EEstado Estado
        {
            get { return this.estado; }
            set { this.estado = value; }
        }

        public string TrackingId
        {
            get { return this.trackingId; }
            set { this.trackingId = value; }
        }
        public void MockCicloVida()//esto ya es un  hilo
        {
            //a. Colocar una demora de 10 segundos.
            //b. Pasar al siguiente estado.
            CambiarEstado();
            this.InformarEstado.Invoke(this, null);

            //e. Finalmente guardar los datos del paquete en la base de datos (a tal fin generar un nuevo
            //    evento que contenga la excepción)
            // informeEstado();          
            try
            {
                PaqueteDAO.Insertar(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
        public string MostrarDatos(IMostrar<Paquete> p)
        {
            return String.Format("{0} - {1}", ((Paquete)p).trackingId.ToString(), ((Paquete)p).direccionEntrega.ToString());
        }
        public static bool operator ==(Paquete p1, Paquete p2)
        {
            if(p1.trackingId == p2.trackingId)
                return true;
            return false;
        }
        public static bool operator !=(Paquete p1, Paquete p2)
        { return !(p1 == p2);}

        public override string ToString()
        {
 	         return this.MostrarDatos(this);
        }

        public void CambiarEstado()
        {
            while (this.estado != EEstado.Entregado)
            {           
                try
                {
                    //c. Informar el estado a través de InformarEstado. EventArgs no tendrá ningún dato extra.
                    this.InformarEstado.Invoke(this, null);
                    Thread.Sleep(3000);
                    switch (this.estado)
                    {
                        case EEstado.Ingresado:
                            this.estado = EEstado.EnViaje;
                            break;
                        case EEstado.EnViaje:
                            this.estado = EEstado.Entregado;
                            break;                                             
                    }
                   

                    
                    

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
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
