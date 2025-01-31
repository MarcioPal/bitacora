using Bitacora.Model;
using Bitacora.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace Bitacora.Controllers
{
    public class BitacoraController
    {
        public FachadaNPOI bitacora = new FachadaNPOI();
        //public FachadaEPPlus Library = new FachadaEPPlus();

        public void registrar(Tarea tarea, List<DateTime> rangoFechas)
        {
            if (tarea.recurso == "")
            {
                MessageBox.Show("Debe seleccionar un valor en el campo Recurso", "Error");
                return;
            }

            bitacora.insertarTarea(tarea, rangoFechas);

        }
        public void visualizar(string recurso, DateTime fecha)
        {

            if (recurso == "")
            {
                MessageBox.Show("Debe seleccionar un valor en el campo Recurso", "Error");
                return;
            }
            string[] NomApe = recurso.Split(' ');
            string Nombre = NomApe[0];
            string Apellido = NomApe[1];
            int Año = fecha.Year;
            string Mes = Calendario.mesToString(fecha.Month);
            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
            string rutaCompleta = Path.GetFullPath(filePath);
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c start \"\" \"{filePath}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");

            }
        }

        public void enviar(string recurso, int month, int Año)
        {
            if (recurso == "")
            {
                MessageBox.Show("Debe seleccionar un valor en el campo Recurso", "Error");
                return;
            }
            string[] NomApe = recurso.Split(' ');
            string Nombre = NomApe[0];
            string Apellido = NomApe[1];
            string Mes = Calendario.mesToString(month);
            string filePath = Path.GetFullPath($"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls");
            // string filePath = Path.GetFullPath(@"./Bitacora-Palazzo-Marcio-Enero-2025.xlsx");
            Debug.WriteLine(filePath);
            Debug.WriteLine(Directory.GetCurrentDirectory());
            int hora = DateTime.Now.Hour;
            string saludo = "Buen dia";
            try
            {
                string processName = "Outlook"; // Por ejemplo: "notepad"

                // Obtener todos los procesos con ese nombre
                var processes = Process.GetProcessesByName(processName);

                if (processes.Length > 0)
                {
                    // Cerrar el primer proceso encontrado
                    processes[0].Kill();
                    Debug.WriteLine($"El proceso {processName} ha sido cerrado.");
                }
                else
                {
                    Debug.WriteLine("No se encontró una instancia activa de Outlook.");
                }

                Outlook.Application outlookApp = new Outlook.Application();
                Outlook.MailItem mailItem = (Outlook.MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);

                if (hora > 12 && hora < 20)
                {
                    saludo = "Buenas tardes";
                }
                if (hora > 20)
                {
                    saludo = "Buenas noches";
                }
                mailItem.Subject = $"Envío Bitácora {Mes} {Año}";
                mailItem.Body = $"{saludo}.\n Se adjunta bitacora del mes de {Mes}\nSaludos. ";
                mailItem.To = "Fernando.Sottano@ar.unisys.com";
                mailItem.CC = "Maximiliano.Primi@unisys.com";
                mailItem.Attachments.Add(filePath);

                mailItem.Display(false); // Mostrar el correo


                Console.WriteLine("Correo mostrado correctamente.");
            }

            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"No existe el archivo: {filePath}", "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }




        }

        public List<Tarea> leerTareas(string recurso, DateTime fecha) {

            return bitacora.leerTareas(recurso, fecha);
        }
        public List<int> getBoldedDates(string filePath)
        {
            return bitacora.getBoldedDates(filePath);
        }

        public void Eliminar(int nroFila, string filePath) { 
            bitacora.Eliminar(nroFila, filePath);
        }
    }
}
