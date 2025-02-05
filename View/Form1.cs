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
                try
                {
                    dias = bc.getBoldedDates(filePath);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            iniciarNotificaciones();
        }

        public void iniciarNotificaciones() {

            bool isRunning = Process.GetProcessesByName("BitacoraNotifications").Any();
            Debug.WriteLine(isRunning);
            if (!isRunning)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "BitacoraNotifications.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                }
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
            try
            {
                if (boxBanco.Text == "" || boxModulo.Text == "" || boxtipoTarea.Text == "" || txtDecripTarea.Text == "")
                {
                    throw new Exception("Debe completar todos los campos obligatorios");
                }
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
            catch (Exception ex) {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
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
            try
            {
                List<Tarea> tareas = bc.leerTareas(boxRecurso.Text, calendario.SelectionStart);
                if (consul_clicks > tareas.Count) { consul_clicks = 1; }

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
            catch (Exception ex) {
                MessageBox.Show(ex.Message,"Error");
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
                calendario.UpdateBoldedDates();
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
