using Bitacora.Controllers;
using Bitacora.Model;
using System.Diagnostics;
using System.ComponentModel;
using Bitacora.Services;



namespace Bitacora
{
    public partial class MiBitacora : Form
    {
        public static int consul_clicks = 0;
        public static DateTime fechaElegida;
        public static int fila_actual = -1;
        public BitacoraController bc = new BitacoraController();    

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal BitacoraController BitacoraController { get; private set; }

        public MiBitacora()
        {
            InitializeComponent();
            fechaElegida = calendario.SelectionStart;

            backgroundWorker1 = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.RunWorkerAsync();

            //BitacoraController bc = new BitacoraController();
            List<int> dias = new List<int>();
            DateTime dateTime = DateTime.Now;
            FileHandler fh = new FileHandler();
            Tarea tarea = fh.getTarea();

            if (tarea is not null)
            {
                string Apellido = tarea.recurso.Split(' ')[1];
                string Nombre = tarea.recurso.Split(' ')[0];
                string Mes = Calendario.mesToString(DateTime.Now.Month);
                int Año = DateTime.Now.Year;

                string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
                tarea.fecha = DateTime.Now;
                dias = bc.getBoldedDates(filePath);
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
            // Debug.WriteLine(startDate);
            //Debug.WriteLine(endDate);
            bc.registrar(tarea, rangoFechas);

            //Marca en negrita las fechas registradas
            for (DateTime fecha = startDate; fecha <= endDate; fecha = fecha.AddDays(1))
            {

                calendario.AddBoldedDate(fecha);
                calendario.UpdateBoldedDates();
            }
            txtDecripTarea.Text = "";
            txtObservaciones.Text = "";
            horas.Value = 1;
            minutos.Value = 0;
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
                string nombre = FileHandler.getNombre();
                if (nombre != "")
                {
                    Services.Notification.enviarNotificacion($"{nombre} no olvides registrar tus tareas diarias en la bitacora!");
                }
                else
                {
                    Services.Notification.enviarNotificacion($"No olvides registrar tus tareas diarias en la bitacora!");
                }

                DateTime hoy = DateTime.Now;
                int mes = hoy.Month;
                if (hoy.Month == 2)
                {
                    if (hoy.Day == 28) { Services.Notification.enviarNotificacion("Recuerda enviar la Bitacora antes de que finalice el mes!"); }

                }
                else
                {
                    if (hoy.Month == 4 || hoy.Month == 6 || hoy.Month == 9 || hoy.Month == 11)
                    {
                        if (hoy.Day == 30) { Services.Notification.enviarNotificacion("Recuerda enviar la Bitacora antes de que finalice el mes!"); }
                    }
                    else
                    {
                        if (hoy.Day == 31) { Services.Notification.enviarNotificacion("Recuerda enviar la Bitacora antes de que finalice el mes!"); }
                    }
                }

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
            calendario.UpdateBoldedDates();
            List<int> dias = new List<int>();
            FileHandler fh = new FileHandler();
            BitacoraController bc = new BitacoraController();
            Tarea tarea = new Tarea(boxRecurso.Text);
            string[] recurso = tarea.recurso.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            tarea.fecha = DateTime.Now;
            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Calendario.mesToString(DateTime.Now.Month)}-{DateTime.Now.Year}.xls";
            bool existe = File.Exists(filePath);

            try
            {
                if (existe)
                {
                    dias = bc.getBoldedDates(filePath);
                    for (int i = 0; i < dias.Count; i++)
                    {
                        DateOnly fecha = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, dias[i]);
                        calendario.AddBoldedDate(fecha.ToDateTime(TimeOnly.MinValue)); // Convierte a DateTime para usar con el calendario
                        calendario.UpdateBoldedDates();
                    }
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {

            consul_clicks += 1;
           // BitacoraController bc = new BitacoraController();
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
                horas.Value = tareas[consul_clicks - 1].horas;
                minutos.Value = tareas[consul_clicks - 1].minutos;
                fila_actual = tareas[consul_clicks - 1].nroFila;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("No hay tareas registradas para la fecha seleccionada.", "Error");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"fila: {fila_actual}");
            string Nombre = boxRecurso.Text.Split(' ')[0];
            string Apellido = boxRecurso.Text.Split(' ')[1];
            string Mes = Calendario.mesToString(calendario.SelectionStart.Month);
            int Año = calendario.SelectionStart.Year;

            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
            try
            {
                bc.Eliminar(fila_actual, filePath);
                List<int> dias = bc.getBoldedDates(filePath);
                calendario.RemoveAllBoldedDates();
                for (int i = 0; i < dias.Count; i++)
                {
                    DateOnly fecha = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, dias[i]);
                    calendario.AddBoldedDate(fecha.ToDateTime(TimeOnly.MinValue)); 
                    calendario.UpdateBoldedDates();
                }
                limpiarCampos();
                MessageBox.Show("La tarea se ha eliminado correctamente");
            }
            catch (Exception exc) {
                MessageBox.Show($"Se ha producido un error: {exc.Message}", "Error");
            }
        }

        public void limpiarCampos() {
            boxBanco.SelectedIndex = -1;
            boxModulo.SelectedIndex = -1;
            boxtipoTarea.SelectedIndex = -1;
            txtDecripTarea.Text = "";
            txtObservaciones.Text = "";
            horas.Value = 1;
            minutos.Value = 0;
        }
    }
}
