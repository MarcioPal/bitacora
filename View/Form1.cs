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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;


namespace Bitacora
{
    public partial class MiBitacora : Form
    {
        public static int consul_clicks = 0;
        public static DateTime fechaElegida; 
        List<Tarea> tareas = new List<Tarea>();

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal BitacoraController BitacoraController { get; private set; }

        public MiBitacora()
        {
            InitializeComponent();
            fechaElegida = calendario.SelectionStart;
            /*
            backgroundWorker1 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();
            */
            BitacoraController bc = new BitacoraController();
            List<int> dias = new List<int>();
            DateTime dateTime = DateTime.Now;
            FileHandler fh = new FileHandler();
            Tarea tarea = fh.getTarea();
            tarea.fecha = DateTime.Now;
            if (tarea is not null)
            {
                dias = bc.getBoldedDates(tarea);
                //dias.Add(15);
                //dias.Add(16);
                for (int i = 0; i < dias.Count; i++)
                {
                    Debug.WriteLine(dias[i]);
                    DateOnly fecha = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, dias[i]);
                    calendario.AddBoldedDate(fecha.ToDateTime(TimeOnly.MinValue)); // Convierte a DateTime para usar con el calendario
                    calendario.UpdateBoldedDates();
                }

                fh.cargarTarea(boxRecurso, boxtipoTarea, boxBanco, boxModulo, txtDecripTarea, txtObservaciones, horas, minutos, tarea);
            }
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

            //Marca en negrita las fechas registradas
            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {

                calendario.AddBoldedDate(fecha);
                calendario.UpdateBoldedDates();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {


            while (true)
            {
                string script = @"
                Install-Module -Name BurntToast -Force -Scope CurrentUser;
                Import-Module BurntToast;
                New-BurntToastNotification -Text 'MiBitacora', 'No olvides registrar tus tareas diarias en la bitacora!'
                                            -AppLogo '..\..\..\..\Resources\icono.png\';
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
                Thread.Sleep(10800000);
            }
        }

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
            calendario.RemoveAllBoldedDates();
            Calendario cal = new Calendario();
            List<int> dias = new List<int>();
            FileHandler fh = new FileHandler();
            BitacoraController bc = new BitacoraController();
            Tarea tarea = new Tarea(boxRecurso.Text);
            string[] recurso = tarea.recurso.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            tarea.fecha = DateTime.Now;
            bool existe = File.Exists($"../../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{cal.mesToString(DateTime.Now.Month)}-{DateTime.Now.Year}.xlsx");

            try
            {
                dias = bc.getBoldedDates(tarea);
                for (int i = 0; i < dias.Count; i++)
                {
                    DateOnly fecha = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, dias[i]);
                    calendario.AddBoldedDate(fecha.ToDateTime(TimeOnly.MinValue)); // Convierte a DateTime para usar con el calendario
                    calendario.UpdateBoldedDates();
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                calendario.RemoveAllBoldedDates();
                calendario.UpdateBoldedDates();
            }


        }


        private void ver_Click(object sender, EventArgs e)
        {
            new BitacoraController().visualizar(boxRecurso.Text, calendario.SelectionStart);
        }

        private void enviar_Click(object sender, EventArgs e)
        {
            DateTime fechatarea = calendario.SelectionStart;

            Debug.WriteLine(fechatarea);
            BitacoraController bc = new BitacoraController();

            bc.enviar(boxRecurso.Text, fechatarea.Month, fechatarea.Year);
        }
        private void consultar_Click(object sender, EventArgs e)
        {

            /*
            ++consul_clicks;
            BitacoraController bc = new BitacoraController();
           List<Tarea> tareas = bc.leerTareas(boxRecurso.Text, calendario.SelectionStart);
            boxRecurso.SelectedItem = tareas[consul_clicks - 1].recurso;
            boxtipoTarea.SelectedItem = tareas[consul_clicks - 1].tipoTarea;
            boxBanco.SelectedItem = tareas[consul_clicks - 1].banco;
            boxModulo.SelectedItem = tareas[consul_clicks - 1].modulo;
            txtDecripTarea.Text = tareas[consul_clicks - 1].descripcion;
            txtObservaciones.Text = tareas[consul_clicks - 1].obervaciones;
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {

            consul_clicks += 1;
            BitacoraController bc = new BitacoraController();
            List<Tarea> tareas = bc.leerTareas(boxRecurso.Text, calendario.SelectionStart);
            if (consul_clicks > tareas.Count) { consul_clicks = 1; }

            try
            {
                boxRecurso.SelectedItem = tareas[consul_clicks - 1].recurso;
                boxtipoTarea.SelectedItem = tareas[consul_clicks - 1].tipoTarea;
                boxBanco.SelectedItem = tareas[consul_clicks - 1].banco;
                boxModulo.SelectedItem = tareas[consul_clicks - 1].modulo;
                txtDecripTarea.Text = tareas[consul_clicks - 1].descripcion;
                txtObservaciones.Text = tareas[consul_clicks - 1].obervaciones;
            }
            catch (System.ArgumentOutOfRangeException ex) {
                MessageBox.Show("No hay tareas registradas para la fecha seleccionada.","Error");
            }

        }
    }
}
