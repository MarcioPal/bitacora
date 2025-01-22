using Bitacora.Model;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Services
{
    internal class FachadaEPPlus ()
    {

       
        public void insertarTarea(Tarea tarea, List<DateTime> rangoFechas) {
            // Habilitar licencia no comercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Calendario calendario = new Calendario();
            string[] recurso = tarea.recurso.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            int Año = tarea.fecha.Year;
            string Mes = calendario.mesToString(tarea.fecha.Month);

            string filePath = $"../../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx";
            string origen = @"../../../../Resources/template.xlsx";


            if (!File.Exists(filePath))
            {
                File.Copy(origen, filePath, overwrite: false);
            }
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets["Bitacora"];


                    //DateTime fecha = DateTime.Now;

                    foreach (DateTime fecha in rangoFechas)
                    {

                        int fromRow = 0;

                        //Recorre desde el principio y evalua por dia menor o igual
                        for (int i = 2; i < 100; i++)
                        {
                            Object value = worksheet.Cells[i, 1].Value;

                            if (value != null)
                            {
                                if (value is double)
                                {
                                    if (fecha.Day <= (double)value)
                                    {
                                        fromRow = i;
                                        break;
                                    }

                                }
                                if (value is int)
                                {
                                    if (fecha.Day <= (int)value)
                                    {
                                        fromRow = i;
                                        break;
                                    }
                                }
                            }

                        }

                        //Recorre desde el principio y evalua por dia mayor 
                        for (int i = 100; i > 1; i--)
                        {
                            Object value = worksheet.Cells[i, 1].Value;

                            if (value is double)
                            {
                                if (value != null && (fecha.Day > (double)value))
                                {
                                    fromRow = i + 1;
                                    break;
                                }
                            }
                            if (value is int)
                            {
                                if (value != null && (fecha.Day > (int)value))
                                {
                                    fromRow = i + 1;
                                    break;
                                }
                            }



                        }

                        //Si no hay dias registrados, escribe comenzando en fila 2 sino desde la fila correspondiente
                        fromRow = fromRow > 0 ? fromRow : 2;


                        worksheet.InsertRow(fromRow, 1);
                        worksheet.Cells[fromRow, 1].Value = fecha.Day;
                        worksheet.Cells[fromRow, 2].Value = calendario.mesToString(tarea.fecha.Month);
                        worksheet.Cells[fromRow, 3].Value = tarea.fecha.Year;
                        worksheet.Cells[fromRow, 4].Style.Numberformat.Format = "hh:mm:ss";
                        worksheet.Cells[fromRow, 4].Value = new TimeSpan(tarea.horas, tarea.minutos, 0);
                        worksheet.Cells[fromRow, 5].Value = tarea.recurso;
                        worksheet.Cells[fromRow, 6].Value = tarea.banco;
                        worksheet.Cells[fromRow, 7].Value = tarea.tipoTarea;
                        worksheet.Cells[fromRow, 8].Value = tarea.descripcion;
                        worksheet.Cells[fromRow, 9].Value = tarea.modulo;
                        worksheet.Cells[fromRow, 10].Value = tarea.obervaciones;

                        worksheet.Cells[fromRow, 5].Style.Font.Bold = true;
                        worksheet.Cells[fromRow, 9].Style.Font.Bold = true;
                        var range = worksheet.Cells[fromRow, 1, fromRow, 10];
                        range.Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);



                    }

                    package.Save();
                    new FileHandler().save(tarea);
                }
            }

            catch (InvalidOperationException ex)
            {
                cerrarInstancia($"Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx");
                insertarTarea(tarea, rangoFechas);
                return;
            }
            /*
            catch (System.IO.IOException ex)
            {
                cerrarInstancia($"Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx");
                insertarTarea(tarea, rangoFechas);
                return;
            }*/
            MessageBox.Show("La tarea se ha registrado correctamente");
            Console.WriteLine($"Archivo actualizado en {Path.GetFullPath(filePath)}");
        }

        public void cerrarInstancia(string name) {

            //string excelFileName = "Bitacora-Palazzo-Marcio-Enero-2025.xlsx";

            foreach (var process in Process.GetProcessesByName("EXCEL"))
            {
                // Opcional: Identificar la instancia basada en criterios adicionales
                if (process.MainWindowTitle.Contains(name))
                {
                    process.CloseMainWindow(); // Forzar cierre
                    Console.WriteLine($"Instancia cerrada: {process.MainWindowTitle}");
                }
            }
        }

        public List<Tarea> leerTareas(string rec, DateTime fecha)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Calendario calendario = new Calendario();
            string[] recurso = rec.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            int Año = fecha.Year;
            string Mes = calendario.mesToString(fecha.Month);
            List<double> dias = new List<double>();
            string filePath = $"../../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx";

            List<int> numbers = new List<int>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Necesario para EPPlus
            List<Tarea> tareas = new List<Tarea>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Obtén la primera hoja (cambia el índice si necesitas otra)
                int rows = worksheet.Dimension.End.Row; // Obtén el número de filas con datos

                for (int row = 2; row <= 100; row++) // Asume que la primera fila contiene datos
                {
                    var cellValue = worksheet.Cells[row, 1].Value;

                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int number))
                    {
                        if (number == fecha.Day) {
                            tareas.Add(new Tarea((string)worksheet.Cells[row, 5].Value,
                                                 (string)worksheet.Cells[row, 7].Value,
                                                 (string)worksheet.Cells[row, 6].Value,
                                                 (string)worksheet.Cells[row, 9].Value,
                                                 (string)worksheet.Cells[row, 8].Value,
                                                 (string)worksheet.Cells[row, 10].Value));
                        }
                    }
                }
            }
            return tareas;

        }

        public List<int> getBoldedDates(Tarea tarea)
        {
            // Habilitar licencia no comercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Calendario calendario = new Calendario();
            string[] recurso = tarea.recurso.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            int Año = tarea.fecha.Year;
            string Mes = calendario.mesToString(tarea.fecha.Month);
            List<double> dias = new List<double>();
            string filePath = $"../../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx";

            List<int> numbers = new List<int>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Necesario para EPPlus

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Obtén la primera hoja (cambia el índice si necesitas otra)
                int rows = worksheet.Dimension.End.Row; // Obtén el número de filas con datos

                for (int row = 2; row <= 100; row++) // Asume que la primera fila contiene datos
                {
                    var cellValue = worksheet.Cells[row, 1].Value;

                    if (cellValue != null && int.TryParse(cellValue.ToString(), out int number))
                    {
                        numbers.Add(number);

                    }
                }
            }
            return numbers;
        }
    }
}
