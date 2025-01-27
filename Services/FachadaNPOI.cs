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
    internal class FachadaNPOI
    {
        public void insertarTarea(Tarea tarea, List<DateTime> rangoFechas)
        {

            Calendario calendario = new Calendario();
            string[] rec = tarea.recurso.Split(' ');
            string Nombre = rec[0];
            string Apellido = rec[1];
            int Año = tarea.fecha.Year;
            string Mes = calendario.mesToString(tarea.fecha.Month);

            string filePath = $"../misBitacoras/Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls";
            string origen = @"../misBitacoras/template.xls";

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


                    foreach (DateTime fecha in rangoFechas)
                    {
                        bool hayRegistros = false;
                        int desde = 1;
                        for (int rowIndex = 1; rowIndex <= 100; rowIndex++)
                        {
                            IRow fila = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                            fila.Height = sheet.GetRow(rowIndex - 1).Height; // Copiar la altura de la fila
                            ICell? celda = fila.GetCell(0);
                            if (celda is not null && celda.CellType == CellType.Numeric)
                            {
                                double dia = fila.GetCell(0).NumericCellValue;
                                hayRegistros = true;
                                if (dia != 0 && tarea.fecha.Day > dia)
                                {
                                    desde = rowIndex + 1;
                                }
                                else
                                {
                                    if (dia == tarea.fecha.Day)
                                    {
                                        desde = rowIndex;
                                        break;
                                    }
                                }
                            }


                        }

                        if (!hayRegistros)
                        {
                            grabarFila(workbook, sheet, tarea, 1);
                        }
                        else
                        {
                            sheet.ShiftRows(desde, sheet.LastRowNum, 1);
                            //desplazarFilas(sheet, desde);
                            grabarFila(workbook, sheet, tarea, desde);
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
                } // El 'FileStream' se cierra automáticamente aquí
            }
            catch (System.IO.IOException ex)
            {
                FileHandler.cerrarInstancia($"Bitacora-{Apellido}-{Nombre}-{Mes}-{Año}.xls");
                insertarTarea(tarea, rangoFechas);
                return;
            }

        }

        public string mesToString(int number)
        {
            switch (number)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: throw new Exception("Numero de mes invalido");
            }
        }

        public void desplazarFilas(ISheet sheet, int index) {
            int rowIndexToInsert = index; // Índice de fila donde quieres insertar (0 basado)
            int lastRowNum = sheet.LastRowNum; // Último número de fila en la hoja



            // Mover filas hacia abajo
            for (int i = 100; i >= rowIndexToInsert; i--)
            {
                IRow sourceRow = sheet.GetRow(i);
                IRow targetRow = sheet.CreateRow(i + 1);
                // IRow targetRow = sheet.GetRow(i+1);

                if (sourceRow != null)
                {
                    ICell dia = sourceRow.GetCell(0);
                    if (dia != null && dia.CellType == CellType.Numeric && dia.NumericCellValue !=0)
                    {
                        //IRow targetRow = sheet.GetRow(i + 1);
                        // sourceRow.CopyRowTo(i+1);
                        
                        CopyRow(sourceRow, targetRow);
                        
                    }
                    MessageBox.Show("dia: " + targetRow.GetCell(0).ToString());
                }
                
            }
        }

        public void grabarFila(HSSFWorkbook workbook, ISheet sheet, Tarea tarea, int rowNum)
        {
            IRow row = sheet.CreateRow(rowNum);

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

            dia.SetCellValue(tarea.fecha.Day);
            mes.SetCellValue(mesToString(tarea.fecha.Month));
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

        public void getLastElementIndex(ISheet sheet) { 
            
        }

    }
}
