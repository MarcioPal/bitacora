param (
    [string]$FilePath
)

# Iniciar Outlook
$outlook = New-Object -ComObject Outlook.Application

# Crear un nuevo correo
$mail = $outlook.CreateItem(0)

# Definir el asunto y el cuerpo del correo
$mail.Subject = "Asunto del correo"
$mail.Body = "Cuerpo del correo"

# Adjuntar un archivo
$attachmentPath = $FilePath #"C:/Users/PalazzoM/OneDrive - Unisys/Desktop/bitacora/misBitacoras/Bitacora-Palazzo-Marcio-Diciembre-2024.xlsx"
$mail.Attachments.Add($attachmentPath)

# Mostrar el correo (puedes usar $mail.Send() para enviarlo directamente)
$mail.Display()
