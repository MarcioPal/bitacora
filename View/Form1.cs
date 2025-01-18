using Bitacora.Controllers;
using Bitacora.Model;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using Bitacora.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;
using System.Management.Automation;
using Windows.Data.Xml.Dom;

namespace Bitacora
{
    public partial class MiBitacora : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal BitacoraController BitacoraController { get; private set; }

        public MiBitacora()
        {
            InitializeComponent();

            backgroundWorker1 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();
            //calendario.MaxSelectionCount = 7;
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
            DateTime fechatarea = calendario.SelectionStart;
            //Debug.WriteLine(fecha);
            Tarea tarea = new Tarea(boxRecurso.Text, boxtipoTarea.Text, boxBanco.Text, boxModulo.Text, txtDecripTarea.Text, txtObservaciones.Text, (int)horas.Value, (int)minutos.Value, fechatarea);
            BitacoraController bc = new BitacoraController();



            DateTime startDate = calendario.SelectionStart;
            DateTime endDate = calendario.SelectionEnd; // Agregar las fechas seleccionadas a la lista
            List<DateTime> rangoFechas = new List<DateTime>();

            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {
                if (!rangoFechas.Contains(fecha))
                {
                    rangoFechas.Add(fecha);
                }
            }
            foreach (DateTime dia in rangoFechas)
            {

                Debug.WriteLine(dia);

            }
            Debug.WriteLine(startDate);
            Debug.WriteLine(endDate);
            bc.registrar(tarea, rangoFechas);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            //while (true)
            // {
            // DateTime tInicio = DateTime.Now;
            //Thread.Sleep(1000);
            /*
            while (true)
            {
                Thread.Sleep(5000);
                string script = @"
                Install-Module -Name BurntToast -Force -Scope CurrentUser;
                Import-Module BurntToast;
                New-BurntToastNotification -Text 'MiBitacora', 'No olvides registrar tus tareas diarias en la bitacora!';
                 ";

                // Crear un archivo temporal para almacenar el script PowerShell
                string scriptFile = Path.GetTempFileName() + ".ps1";
                File.WriteAllText(scriptFile, script);

                // Ejecutar PowerShell como un proceso externo
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptFile}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Leer la salida
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    Console.WriteLine("Output: " + output);
                    Console.WriteLine("Error: " + error);

                }

                // Eliminar el archivo temporal
                File.Delete(scriptFile);
            }*/
        }
        // }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
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

        private void boxModulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void boxRecurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ver_Click(object sender, EventArgs e)
        {
            new BitacoraController().visualizar(boxRecurso.Text);
        }

        private void enviar_Click(object sender, EventArgs e)
        {
            DateTime fechatarea = calendario.SelectionStart;

            Debug.WriteLine(fechatarea);
            BitacoraController bc = new BitacoraController();

            bc.enviar(boxRecurso.Text, fechatarea.Month, fechatarea.Year);
        }
    }
}
