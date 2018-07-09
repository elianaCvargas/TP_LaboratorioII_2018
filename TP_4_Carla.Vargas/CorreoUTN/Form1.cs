using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;


namespace CorreoUTN
{
    public partial class Form1 : Form
    {
        Correo correo;
        Paquete paquete;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Correo UTN Carla Vargas 2C";
            this.rtbMostrar.Enabled = false;
            correo = new Correo();
            this.txtMaskTrakingId.Mask = "000-000-0000";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void rtbMostrar_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTrakingId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
// El evento click del botón btnAgregar realizará las siguientes acciones en el siguiente orden:
//a. Creará un nuevo paquete y asociará al evento InformaEstado el método paq_InformaEstado.
//b. Agregará el paquete al correo, controlando las excepciones que puedan derivar de dicha
//acción.
//c. Llamará al método ActualizarEstados
            this.paquete = new Paquete(this.txtDireccion.Text, this.txtMaskTrakingId.Text);
            this.paquete.InformarEstado += paq_InformaEstado;
            try
            {
                this.correo += paquete;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message) ;
            }
            ActualizarEstados();
            


        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        public void paq_InformaEstado(object sender, EventArgs e)
        {

            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else {
                ActualizarEstados();
            }


        }

        public void ActualizarEstados()
        {
//El método ActualizarEstados limpiará los 3 ListBox y luego recorrerá la lista de paquetes agregando
//cada uno de ellos en el listado que corresponda.
            //Se valida que los lists box contengan los items respectivos. y no que estèn mezclados
            this.lstEstadoIngresado.ResetText();
            this.lstEstadoEnViaje.ResetText();
            this.lstEstadoEntregado.ResetText();
            foreach (Paquete element in correo.Paquetes)
            {
                switch (element.Estado)
                {
                    case EEstado.Ingresado:
                        if (!this.lstEstadoIngresado.Items.Contains(element))
                            this.lstEstadoIngresado.Items.Add(element);

                        break;
                    case EEstado.EnViaje:
                        if (!this.lstEstadoEnViaje.Items.Contains(element))
                        { 
                            this.lstEstadoEnViaje.Items.Add(element);
                            this.lstEstadoIngresado.Items.Clear();
                        }
                            
                        break;
                    case EEstado.Entregado:
                        if (!this.lstEstadoEntregado.Items.Contains(element))
                        { 
                            this.lstEstadoEntregado.Items.Add(element);
                            this.lstEstadoEnViaje.Items.Clear();
                        }
                            
                        break;
     
                }
            }
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        public void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            if (elemento != null && elemento is Paquete)
            {
               this.rtbMostrar.Text = ((Paquete)elemento).MostrarDatos((Paquete)elemento);

            }
            if (elemento != null && elemento is Correo)
            {
                this.rtbMostrar.Text = ((Correo)elemento).MostrarDatos((Correo)elemento);
            }
            try
            {
                GuardaString.Guarda(elemento.MostrarDatos(elemento), "Salida.txt");
            }
            catch (Exception e)
            {
                
                MessageBox.Show("No se pudo guardar el archivo");
            }

        }

      

        //private void mostrarToolStripMenuItem_Opening(object sender, CancelEventArgs e)
        //{
        //    
        //}

        private void lstEstadoEntregado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

        
        
    }
}
