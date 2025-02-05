using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Bitacora.Model;
using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;  // Para archivos .xls
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;    // Para trabajar con celdas y hojas

namespace Bitacora.Services
{
    public class FachadaNPOI
    {
        public static string origen = @"./Resources/template.xls"; 
        public void insertarTarea(Tarea tarea, List<DateTime> rangoFechas)
        {

            string[] rec = tarea.recurso.Split(' ');
            string Nombre = rec[0];
            string Apellido = rec[1];
            int Año = tarea.fecha.Year;
            string Mes = Calendario.mesToString(tarea.fecha.Month);

            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
            

            if (!File.Exists(filePath))
            {
                File.Copy(origen, filePath, overwrite: false);
            }
            try
            {
                using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fsRead);
                    ISheet sheet = workbook.GetSheetAt(0);

                    bool hayRegistros = false;
                    foreach (DateTime fecha in rangoFechas)
                    {
                        int desde = 1;
                        for (int rowIndex = 1; rowIndex <= 100; rowIndex++)
                        {
                            
                            IRow fila = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                            //fila.Height = sheet.GetRow(rowIndex - 1).Height; // Copiar la altura de la fila
                            ICell? celda = fila.GetCell(0);
                            if (celda is not null && celda.CellType == CellType.Numeric)
                            {
                                double dia = fila.GetCell(0).NumericCellValue;
                                hayRegistros = true;
                                if (dia != 0 && fecha.Day > dia)
                                {
                                    desde = rowIndex + 1;
                                }
                                else
                                {
                                    if (dia == fecha.Day)
                                    {
                                        desde = rowIndex;
                                        break;
                                    }
                                }
                            }
                            Debug.WriteLine(fecha + " " + desde);

                        }

                        if (!hayRegistros)
                        {
                            grabarFila(workbook, sheet, tarea, 1,fecha);
                        }
                        else
                        {
                            sheet.ShiftRows(desde, sheet.LastRowNum, 1);
                            //desplazarFilas(sheet, desde);
                            Debug.WriteLine("NPOI78: "+fecha);
                            grabarFila(workbook, sheet, tarea, desde, fecha);
                        }
                        // Guardar los cambios en el archivo Excel
                        using (FileStream fsWrite = new FileStream(filePath, FileMode.Open, FileAccess.Write))
                        {
                            // Reinicia la posición del flujo para guardar los cambios
                            fsWrite.Position = 0;
                            workbook.Write(fsWrite);
                            new FileHandler().save(tarea);

                        }
                    }
                }
                MessageBox.Show("La tarea se ha registrado correctamente");
            }
            catch (System.IO.IOException ex)
            {
                FileHandler.cerrarInstancia($"Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls");
                insertarTarea(tarea, rangoFechas);
                return;
            }
        }

        public List<Tarea> leerTareas(string rec, DateTime fecha)
        {
            string[] recurso = rec.Split(' ');
            string Nombre = recurso[0];
            string Apellido = recurso[1];
            int Año = fecha.Year;
            string Mes = Calendario.mesToString(fecha.Month);
            List<double> dias = new List<double>();
            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
            List<int> numbers = new List<int>();
            List<Tarea> tareas = new List<Tarea>();

            try
            {
                using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fsRead);
                    ISheet sheet = workbook.GetSheetAt(0);

                    for (int i = 1; i <= 100; i++) // Asume que la primera fila contiene datos
                    {
                        IRow? row = sheet.GetRow(i) ?? null;
                        ICell dia = null;
                        if (row != null) {
                             dia = row.GetCell(0);
                        }

                        if (dia != null)
                        {
                            if (dia.NumericCellValue == fecha.Day)
                            {
                                tareas.Add(new Tarea(row.GetCell(4).StringCellValue,
                                                     row.GetCell(6).StringCellValue,
                                                     row.GetCell(5).StringCellValue,
                                                     row.GetCell(8).StringCellValue,
                                                     row.GetCell(7).StringCellValue,
                                                     row.GetCell(9).StringCellValue,
                                                     row.GetCell(3).DateCellValue.Value.Hour,
                                                     row.GetCell(3).DateCellValue.Value.Minute,
                                                     row.GetCell(3).DateCellValue.Value,
                                                     i //nro fila
                                                     ));
                                //Debug.WriteLine($"fila nro {tareas[i].nroFila}");

                            }
                        }

                    }
                }
            }
            catch (FileNotFoundException ex)
            {
               

            }
            catch(System.IO.IOException ex)
            {
                FileHandler.cerrarInstancia($"Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls");
                return leerTareas(rec,fecha);
            }
            return tareas;

        }

        public void grabarFila(HSSFWorkbook workbook, ISheet sheet, Tarea tarea, int rowNum, DateTime fecha)
        {
            IRow row = sheet.CreateRow(rowNum);
            Debug.WriteLine($"grabar {fecha.Day} {rowNum}");
            // Crear celdas
            ICell dia = row.CreateCell(0);
            ICell mes = row.CreateCell(1);
            ICell anio = row.CreateCell(2);
            ICell tiempo = row.CreateCell(3);
            ICell recurso = row.CreateCell(4);
            ICell banco = row.CreateCell(5);
            ICell tipoTarea = row.CreateCell(6);
            ICell descripcion = row.CreateCell(7);
            ICell modulo = row.CreateCell(8);
            ICell observaciones = row.CreateCell(9);

            // Crear fuente Arial 10 estándar
            NPOI.SS.UserModel.IFont arialNormal = workbook.CreateFont();
            arialNormal.FontName = "Arial";
            arialNormal.FontHeightInPoints = 10;

            // Crear estilo normal
            ICellStyle estiloNormal = workbook.CreateCellStyle();
            estiloNormal.WrapText = true;
            estiloNormal.SetFont(arialNormal);
            estiloNormal.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloNormal.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloNormal.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloNormal.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloNormal.TopBorderColor = IndexedColors.Black.Index;
            estiloNormal.BottomBorderColor = IndexedColors.Black.Index;
            estiloNormal.LeftBorderColor = IndexedColors.Black.Index;
            estiloNormal.RightBorderColor = IndexedColors.Black.Index;

            // Crear fuente Arial 10 negrita
            NPOI.SS.UserModel.IFont arialBold = workbook.CreateFont();
            arialBold.FontName = "Arial";
            arialBold.FontHeightInPoints = 10;
            arialBold.IsBold = true;

            // Crear estilo en negrita
            ICellStyle estiloBold = workbook.CreateCellStyle();
            estiloBold.SetFont(arialBold);
            estiloBold.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloBold.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloBold.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloBold.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            estiloBold.TopBorderColor = IndexedColors.Black.Index;
            estiloBold.BottomBorderColor = IndexedColors.Black.Index;
            estiloBold.LeftBorderColor = IndexedColors.Black.Index;
            estiloBold.RightBorderColor = IndexedColors.Black.Index;

            // Asignar estilos y valores a las celdas
            dia.CellStyle = estiloNormal;
            mes.CellStyle = estiloNormal;
            anio.CellStyle = estiloNormal;
            tiempo.CellStyle = estiloNormal;
            banco.CellStyle = estiloNormal;
            tipoTarea.CellStyle = estiloNormal;
            descripcion.CellStyle = estiloNormal;
            observaciones.CellStyle = estiloNormal;

            recurso.CellStyle = estiloBold; // Aplicar estilo en negrita
            modulo.CellStyle = estiloBold; // Aplicar estilo en negrita

            dia.SetCellValue(fecha.Day);
            mes.SetCellValue(Calendario.mesToString(tarea.fecha.Month));
            anio.SetCellValue(tarea.fecha.Year);

            // Configurar formato para el campo tiempo
            ICellStyle timeStyle = workbook.CreateCellStyle();
            timeStyle.SetFont(arialNormal);
            IDataFormat dataFormat = workbook.CreateDataFormat();
            timeStyle.DataFormat = dataFormat.GetFormat("hh:mm:ss");

            timeStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            timeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            timeStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            timeStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            timeStyle.TopBorderColor = IndexedColors.Black.Index;
            timeStyle.BottomBorderColor = IndexedColors.Black.Index;
            timeStyle.LeftBorderColor = IndexedColors.Black.Index;
            timeStyle.RightBorderColor = IndexedColors.Black.Index;

            tiempo.CellStyle = timeStyle;
            tiempo.SetCellValue(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, tarea.horas, tarea.minutos, 0));

            recurso.SetCellValue(tarea.recurso);  // Estilo en negrita
            modulo.SetCellValue(tarea.modulo);   // Estilo en negrita

            banco.SetCellValue(tarea.banco);
            tipoTarea.SetCellValue(tarea.tipoTarea);
            descripcion.SetCellValue(tarea.descripcion);
            observaciones.SetCellValue(tarea.obervaciones);

        }


        public void CopyRow(IRow sourceRow, IRow targetRow)
        {
            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                ICell sourceCell = sourceRow.GetCell(i);
                if (sourceCell != null)
                {
                    ICell targetCell = targetRow.CreateCell(i);
                    if ((i == 0 || i == 2))// && targetCell.NumericCellValue != 0 && sourceCell.NumericCellValue != 0)
                    {
                        
                        if (targetCell.NumericCellValue == 0 || sourceCell.NumericCellValue == 0) {
                            targetCell.SetCellValue("");
                            break;
                        }
                        targetCell.SetCellValue(sourceCell.NumericCellValue);
                    }
                    else {
                        targetCell.SetCellValue(sourceCell.ToString());
                    }
                    
                }
            }
        }

        public List<int> getBoldedDates(string filePath)
        {
            List<double> dias = new List<double>();


            List<int> numbers = new List<int>();

            try
            {
                using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fsRead);
                    ISheet sheet = workbook.GetSheetAt(0);

                    for (int i = 1; i <= 100; i++) // Asume que la primera fila contiene datos
                    {
                        IRow? row = sheet.GetRow(i) ?? null;
                        ICell dia = null;
                        if (row is not null) {
                            dia = row.GetCell(0);
                        }

                        if (dia != null)
                        {
                            numbers.Add((int)dia.NumericCellValue);

                        }
                    }
                }
            }
            catch (FileNotFoundException ex) {
               // File.Copy(origen, filePath, overwrite: true);
            }
            catch (System.IO.IOException ex)
            {
                FileHandler.cerrarInstancia(filePath);
                getBoldedDates(filePath);
            }
            catch (System.NullReferenceException e)
            {
                Debug.WriteLine(e.Message);

            }
            return numbers;
        }

        public void Eliminar(int nroFila, string filePath) {
            try
            {
                using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fsRead);
                    ISheet sheet = workbook.GetSheetAt(0);
                    IRow? row = sheet.GetRow(nroFila) ?? null;

                    if (row != null)
                    {
                        // Eliminar la fila
                        sheet.RemoveRow(row);

                        // Desplazar las filas posteriores hacia arriba
                        int lastRowNum = sheet.LastRowNum;

                        if (nroFila < lastRowNum)
                        {
                            sheet.ShiftRows(nroFila + 1, lastRowNum, -1);
                        }
                        using (FileStream fsWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            workbook.Write(fsWrite);
                        }
                    }

                }
            }
            catch (FileNotFoundException ex) { 
                
            }
            catch (System.IO.IOException ex)
            {
                FileHandler.cerrarInstancia(filePath);
                Eliminar(nroFila, filePath);
            }
        }
           
    }
}

