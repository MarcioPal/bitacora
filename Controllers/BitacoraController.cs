using Bitacora.Model;
using Bitacora.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Controllers
{
    internal class BitacoraController
    {
        public void registrar(Tarea tarea, List<DateTime> rangoFechas) { 
            FachadaEPPlus obj = new FachadaEPPlus();
            if (tarea.fecha > DateTime.Now)
            {
                MessageBox.Show("No se puede registrar tareas para una fecha posterior a la actual","Error");
            }
            else
            {
                obj.insertarTarea(tarea, rangoFechas);
            }
        }
        public void visualizar(string recurso) {

            string[] NomApe = recurso.Split(' ');
            string Nombre = NomApe[0];
            string Apellido = NomApe[1];
            int Año = DateTime.Now.Year;
            string Mes = new Calendario().mesToString(DateTime.Now.Month);
            string filePath = $"../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx";
            string rutaCompleta = Path.GetFullPath(filePath);

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}
