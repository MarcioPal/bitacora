using Bitacora.Controllers;
using Bitacora.Model;
using System.Diagnostics;

namespace Bitacora
{
    public partial class Form1 : Form
    {
        internal BitacoraController BitacoraController { get; private set; }

        public Form1()
        {
            InitializeComponent();
            DateTime startDate = new DateTime(2024, 12, 6);
            DateTime endDate = new DateTime(2024, 12, 12);

            // Limpiar las fechas previas resaltadas
            calendario.RemoveAllBoldedDates();

            // Iterar sobre las fechas en el rango
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Añadir cada fecha dentro del rango a las fechas resaltadas
                calendario.AddBoldedDate(date);
            }

            // Actualizar la visualización para aplicar el cambio
            calendario.UpdateBoldedDates();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Debug.WriteLine($"El directorio de trabajo actual es: {currentDirectory}");
            DateTime fecha = calendario.SelectionStart;
            Debug.WriteLine(fecha);
            Tarea tarea = new Tarea(boxRecurso.Text,boxtipoTarea.Text,boxBanco.Text,boxModulo.Text,txtDecripTarea.Text,txtObservaciones.Text,(int)horas.Value,(int)minutos.Value,fecha.Day);
            BitacoraController bc = new BitacoraController();
            bc.registrar(tarea); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click1(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
