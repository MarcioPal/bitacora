using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Model
{
    public class Tarea
    {
        public String recurso;
        public String tipoTarea;
        public String banco;
        public String modulo;
        public String descripcion;
        public String obervaciones;
        public int horas;
        public int minutos;
        public DateTime fecha;
        public int nroFila;

        public Tarea(string recurso, string tipoTarea, string banco, string modulo, string descripcion, string obervaciones, int horas, int minutos, DateTime fecha)
        {
            this.recurso = recurso;
            this.tipoTarea = tipoTarea;
            this.banco = banco;
            this.modulo = modulo;
            this.descripcion = descripcion;
            this.obervaciones = obervaciones;
            this.horas = horas;
            this.minutos = minutos;
            this.fecha = fecha;

        }
        public Tarea(string recurso, string tipoTarea, string banco, string modulo, string descripcion, string obervaciones, int horas, int minutos, DateTime fecha, int nroFila)
        {
            this.recurso = recurso;
            this.tipoTarea = tipoTarea;
            this.banco = banco;
            this.modulo = modulo;
            this.descripcion = descripcion;
            this.obervaciones = obervaciones;
            this.horas = horas;
            this.minutos = minutos;
            this.fecha = fecha;
            this.nroFila = nroFila;

        }

        public Tarea(string recurso, string tipoTarea, string banco, string modulo, string descripcion, string obervaciones)
        {
            this.recurso = recurso;
            this.tipoTarea = tipoTarea;
            this.banco = banco;
            this.modulo = modulo;
            this.descripcion = descripcion;
            this.obervaciones = obervaciones;

        }
        public Tarea(string recurso) {
            this.recurso = recurso;
        }

    }
}
