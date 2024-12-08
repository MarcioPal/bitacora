using Bitacora.Model;
using Bitacora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Controllers
{
    internal class BitacoraController
    {
        public void registrar(Tarea tarea) { 
            FachadaNPOI obj = new FachadaNPOI();
            obj.insertarTarea(tarea);
            
        }
    }
}
