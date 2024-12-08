using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Model
{
    internal class Tarea
    {
        public String recurso;
        public String tipoTarea;
        public String banco;
        public String modulo;
        public String descripcion;
        public String obervaciones;
        public int horas;
        public int minutos;
        public int dia;

        public Tarea(string recurso, string tipoTarea, string banco, string modulo, string descripcion, string obervaciones, int horas, int minutos, int dia)
        {
            this.recurso = recurso;
            this.tipoTarea = tipoTarea;
            this.banco = banco;
            this.modulo = modulo;
            this.descripcion = descripcion;
            this.obervaciones = obervaciones;
            this.horas = horas;
            this.minutos = minutos;
            this.dia = dia;

        }

    }
}
