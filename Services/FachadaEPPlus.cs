using Bitacora.Model;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            string filePath = $"../../../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xlsx";
            string origen = @"../../../Resources/template.xlsx";


            if (!File.Exists(filePath))
            {
                File.Copy(origen, filePath, overwrite: false);
            }
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
                            if (fecha.Day <= (double)value)
                            {
                                fromRow = i;
                                break;
                            }
                        }

                    }

                    //Recorre desde el principio y evalua por dia mayor 
                    for (int i = 100; i > 1; i--)
                    {
                        Object value = worksheet.Cells[i, 1].Value;

                        if (value != null && (fecha.Day > (double)value))
                        {
                            fromRow = i + 1;
                            break;
                        }

                    }

                    //Si no hay dias registrados, escribe comenzando en fila 2 sino desde la fila correspondiente
                    fromRow = fromRow > 0 ? fromRow : 2;


                    worksheet.InsertRow(fromRow, 1);
                    worksheet.Cells[fromRow, 1].Value = fecha.Day;
                    worksheet.Cells[fromRow, 2].Value = calendario.mesToString(tarea.fecha.Month);
                    worksheet.Cells[fromRow, 3].Value = tarea.fecha.Year;
                    worksheet.Cells[fromRow, 4].Value = tarea.horas;
                    worksheet.Cells[fromRow, 5].Value = tarea.recurso;
                    worksheet.Cells[fromRow, 6].Value = tarea.banco;
                    worksheet.Cells[fromRow, 7].Value = tarea.tipoTarea;
                    worksheet.Cells[fromRow, 8].Value = tarea.descripcion;
                    worksheet.Cells[fromRow, 9].Value = tarea.modulo;
                    worksheet.Cells[fromRow, 10].Value = tarea.obervaciones;

                    var range = worksheet.Cells[fromRow, 1, fromRow, 10];
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);



                }
                package.Save();
            }
            Console.WriteLine($"Archivo actualizado en {Path.GetFullPath(filePath)}");
        }
    }
}
