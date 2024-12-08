using System;
using System.Diagnostics;
using System.IO;
using Bitacora.Model;
using NPOI.HSSF.UserModel;  // Para archivos .xls
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;    // Para trabajar con celdas y hojas

namespace Bitacora.Services
{
    internal class FachadaNPOI
    {
        public void insertarTarea(Tarea tarea)
        {
            // Ruta del archivo Excel a editar
            string filePath = "../../../misBitacoras/template.xls";

            // Abrir el archivo para lectura
            using (FileStream fsRead = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Cargar el libro de trabajo (.xls)
                HSSFWorkbook workbook = new HSSFWorkbook(fsRead);
                // Acceder a la primera hoja
                ISheet sheet = workbook.GetSheetAt(0);

                // Editar una celda en la primera fila, primera columna (índices empiezan en 0)
                // IRow row = sheet.GetRow(1) ?? sheet.CreateRow(1);  // Crea una fila si no existe

                // Modificar el valor de las celdas

                bool hayRegistros = false;
                for (int rowIndex = 1; rowIndex <= 5 ; rowIndex++)
                {
                    IRow fila = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
                    ICell? celda = fila.GetCell(0);


                    double day = celda is null ? 0 : celda.NumericCellValue;

                    if (celda.NumericCellValue != 0)
                    {
                        hayRegistros = true;
                    }
                    

                    if (day == tarea.dia)
                    {

                        desplazarFilas(sheet, rowIndex + 1);

                        IRow row = sheet.CreateRow(rowIndex + 1);  // Crea una fila si no existe
                        ICell dia = row.GetCell(0) ?? row.CreateCell(0);   // Crea una celda si no existe
                        ICell mes = row.GetCell(1) ?? row.CreateCell(1);
                        ICell anio = row.GetCell(2) ?? row.CreateCell(2);
                        ICell tiempo = row.GetCell(3) ?? row.CreateCell(3);
                        ICell recurso = row.GetCell(4) ?? row.CreateCell(4);
                        ICell banco = row.GetCell(5) ?? row.CreateCell(5);
                        ICell tipoTarea = row.GetCell(6) ?? row.CreateCell(6);
                        ICell descripcion = row.GetCell(7) ?? row.CreateCell(7);
                        ICell modulo = row.GetCell(8) ?? row.CreateCell(8);
                        ICell observaciones = row.GetCell(9) ?? row.CreateCell(9);
                        
                        DateTime fecha = DateTime.Now;
                        dia.SetCellValue(fecha.Day);
                        mes.SetCellValue(mesToString(fecha.Month));
                        anio.SetCellValue(fecha.Year);
                        tiempo.SetCellValue(tarea.horas);
                        recurso.SetCellValue(tarea.recurso);
                        banco.SetCellValue(tarea.banco);
                        tipoTarea.SetCellValue(tarea.tipoTarea);
                        descripcion.SetCellValue(tarea.descripcion);
                        modulo.SetCellValue(tarea.modulo);
                        observaciones.SetCellValue(tarea.obervaciones);


                        Debug.WriteLine("existe");
                        break;
                    }
                   
                }
                if (!hayRegistros) {
                    Debug.WriteLine("hola");
                    IRow row = sheet.CreateRow(1);  // Crea una fila si no existe
                    ICell dia = row.GetCell(0) ?? row.CreateCell(0);   // Crea una celda si no existe
                    ICell mes = row.GetCell(1) ?? row.CreateCell(1);
                    ICell anio = row.GetCell(2) ?? row.CreateCell(2);
                    ICell tiempo = row.GetCell(3) ?? row.CreateCell(3);
                    ICell recurso = row.GetCell(4) ?? row.CreateCell(4);
                    ICell banco = row.GetCell(5) ?? row.CreateCell(5);
                    ICell tipoTarea = row.GetCell(6) ?? row.CreateCell(6);
                    ICell descripcion = row.GetCell(7) ?? row.CreateCell(7);
                    ICell modulo = row.GetCell(8) ?? row.CreateCell(8);
                    ICell observaciones = row.GetCell(9) ?? row.CreateCell(9);

                    DateTime fecha = DateTime.Now;
                    dia.SetCellValue(fecha.Day);
                    mes.SetCellValue(mesToString(fecha.Month));
                    anio.SetCellValue(fecha.Year);
                    tiempo.SetCellValue(tarea.horas);
                    recurso.SetCellValue(tarea.recurso);
                    banco.SetCellValue(tarea.banco);
                    tipoTarea.SetCellValue(tarea.tipoTarea);
                    descripcion.SetCellValue(tarea.descripcion);
                    modulo.SetCellValue(tarea.modulo);
                    observaciones.SetCellValue(tarea.obervaciones);
                }
                    // Guardar los cambios en el archivo Excel
                    using (FileStream fsWrite = new FileStream(filePath, FileMode.Open, FileAccess.Write))
                {
                    // Reinicia la posición del flujo para guardar los cambios
                    fsWrite.Position = 0;
                    workbook.Write(fsWrite);
                }
            } // El 'FileStream' se cierra automáticamente aquí

            Console.WriteLine("Archivo Excel editado correctamente.");
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
            for (int i = lastRowNum; i >= rowIndexToInsert; i--)
            {
                IRow sourceRow = sheet.GetRow(i);
                IRow targetRow = sheet.CreateRow(i + 1);

                if (sourceRow != null)
                {
                   CopyRow(sourceRow, targetRow);
                }
            }
        }
        public void CopyRow(IRow sourceRow, IRow targetRow)
        {
            for (int i = 0; i < sourceRow.LastCellNum; i++)
            {
                ICell sourceCell = sourceRow.GetCell(i);
                if (sourceCell != null)
                {
                    ICell targetCell = targetRow.CreateCell(i);
                    if ((i == 0 || i == 2) && targetCell.NumericCellValue != 0)
                    {
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
