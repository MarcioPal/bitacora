using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitacora.Services
{
    internal class Notification
    {
        public static void enviarNotificacion(string mensaje)
        {
            string script = $@"
        Install-Module -Name BurntToast -Force -Scope CurrentUser;
        Import-Module BurntToast;
        $imagePath = '../Resources/icono.ico';
        New-BurntToastNotification -Text 'MiBitacora', '{mensaje}' -AppLogo $imagePath;
        ";

            // Crear un archivo temporal para almacenar el script PowerShell
            string scriptFile = Path.GetTempFileName() + ".ps1";
            File.WriteAllText(scriptFile, script);

            // Ejecutar PowerShell como un proceso externo
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptFile}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                // Leer la salida
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                Console.WriteLine("Output: " + output);
                Console.WriteLine("Error: " + error);
            }

            // Eliminar el archivo temporal
            File.Delete(scriptFile);
            //Thread.Sleep(10800000);
        }

    }
}
