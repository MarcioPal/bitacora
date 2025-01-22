using Bitacora.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Services
{
    internal class FileHandler
    {

        public void save(Tarea tarea)
        {
            // Ruta del archivo CSV
            Calendario calendario = new Calendario();
            string[] recurso = tarea.recurso.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            int Año = tarea.fecha.Year;
            string Mes = calendario.mesToString(tarea.fecha.Month);
            List<double> dias = new List<double>();
            string filePath = $"../../../../misBitacoras/bck.txt";



            // Crear y escribir el archivo CSV
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine($"{tarea.recurso};{tarea.tipoTarea};{tarea.banco};{tarea.modulo};{tarea.descripcion.Replace("\r", "").Replace("\n", "")};{tarea.obervaciones.Replace("\r", "").Replace("\n", "")};{tarea.horas};{tarea.minutos}");

            }
        }

        public Tarea getTarea() {
            String path = "../../../../misBitacoras/bck.txt";
            Tarea tarea = null;
            if (File.Exists(path))
            {
                if (new FileInfo(path).Length > 0)
                {
                    using (var reader = new StreamReader(path))
                    {
                        while (!reader.EndOfStream)
                        {
                            // Leer línea por línea
                            var line = reader.ReadLine();
                            // Dividir la línea por el delimitador (coma en este caso)
                            var fields = line.Split(';');
                            tarea = new Tarea(
                            fields[0],
                            fields[1],
                            fields[2],
                            fields[3],
                            fields[4],
                            fields[5]);



                        }
                    }
                }
            }
            return tarea;

        }
        
        public void cargarTarea(ComboBox recurso, ComboBox tipoTarea, ComboBox banco, ComboBox modulo,RichTextBox txtDesc, RichTextBox txtObser, NumericUpDown horas, NumericUpDown minutos,Tarea tarea)
        {
            String path = "../../../../misBitacoras/bck.txt";
            if (File.Exists(path))
            {
                if (new FileInfo(path).Length > 0)
                {
                    using (var reader = new StreamReader(path))
                    {
                        while (!reader.EndOfStream)
                        {
                            // Leer línea por línea
                            var line = reader.ReadLine();

                            // Dividir la línea por el delimitador (coma en este caso)
                            var fields = line.Split(';');

                            recurso.SelectedItem = fields[0];
                            tipoTarea.SelectedItem = fields[1];
                            banco.SelectedItem = fields[2];
                            modulo.SelectedItem = fields[3];
                            txtDesc.Text = fields[4];
                            txtObser.Text = fields[5];
   

                            
                        }
                    }
                }
            }

        }
    }
}
