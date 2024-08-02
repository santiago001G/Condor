# Configurar encabezados
$userProfile = [System.Environment]::GetFolderPath('UserProfile')
# Define el nombre del archivo del instalador
$instalador = "WindowsSensor.MaverickGyr.exe"
$rutaInstalador = Join-Path -Path $userProfile -ChildPath "Downloads\$instalador"

# Configurar encabezados
$headers = @{
    "Authorization" = "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6InB1YmxpYzo3ODdlZjkxZC01YzMxLTQ3YzktYjg0Ni1kNDIzNTk1M2FmYWEiLCJ0eXAiOiJKV1QifQ.eyJhdWQiOltdLCJjbGllbnRfaWQiOiJmYjg1NTg1ODMzZjQ0MWNjODA4NDc0OGVjZmRiYmJlOCIsImV4cCI6MTcyMjYyOTkzOSwiZXh0Ijp7InN1Yl90eXBlIjoiY2xpZW50In0sImlhdCI6MTcyMjYyODEzOSwiaXNzIjoiaHR0cHM6Ly9hcGkuY3Jvd2RzdHJpa2UuY29tLnRvZG8vIiwianRpIjoiOGI4MDFhZTgtNWExZS00NDc1LTljNmMtMDNkMWJmMmRlZTVjIiwibmJmIjoxNzIyNjI4MTM5LCJzY3AiOltdLCJzdWIiOiJmYjg1NTg1ODMzZjQ0MWNjODA4NDc0OGVjZmRiYmJlOCIsInN1Yl90eXBlIjoiY2xpZW50In0.vimYF9Dy43Fd3sA4FwDA6wPPZb3P6TeJJqoSkM3848QHFWT3Is_L7mNyZ7XizOK8mJ2XbJcROVPwgJPVwbd-7xUsJIwmXOaU6ZlyXgSUCvsfGRZFdaOZWJjnkZ5JMVx5PUkeNlgk9aMLYV_kqb7wWj8iFOgj_ndsPwrXrzuXKQHCK44JcawrWTyS6I_UUhNw_695M2v_pLRpHXiO6soNPu9t5UWLex1BQjF3m7ewN0vl_nuRsWm2DCJWFad0o41e29zvz1j28vFP8gHPKnPTcpEc2VK0wWzsXosQXksSOjUbQazMLeZuKeKDESP2G9VN5LKR1Q6tuqhCDBlPj8n-AkHK757yW84oC0znj584XZR0KB7V9LvGqFFGWnIXrD79AezpxcYK6TcV0iBWC-Qc3BLM-1FnE0uQzHBK9RjlDp_npsHvqm8xUJHM_zpCmO6n5EFcKB8pw2Sv9h_kU1McK3Ljx8E5MBBhb6MVqlSNyOPVxO83K5uhTNXgI5gzGPa2ERoRaVcUwGbIc8W7ERcqCN8jBKk7Raw7erDF6WD_CGKzRbsgTmSuepYjNDJrNReUjzNcL0X2Y6IOrnwqr7nPaw-jAabbeUmZmnaE6N05bF1PfkC5EOofvnvkc4MxVpglNptE32u4B2HS4yHOkBp_MnDktOx7g1Y-diWE3eLhJCw"
}

# URL del archivo a descargar
$url = 'https://api.us-2.crowdstrike.com/sensors/entities/download-installer/v2?id=689f876941a61dd9358b12e70f1d120e53921efa1d86d561e54eb08684e70e0a'

# Realizar la solicitud y guardar el archivo
Invoke-WebRequest -Uri $url -Method 'GET' -Headers $headers -OutFile $rutaInstalador

if (!(Get-Service -Name 'CSFalconService' -ErrorAction SilentlyContinue)) {
    # Desactivar el Windows Defender
    # Set-MpPreference -DisableRealtimeMonitoring $true

    #Define el CustomerId de IFX
    $ifxCID = 33633

    # Define el serviceId de IFX
    $ifxSID = "121991_2299"


    # Define el VCenter
    $ifxVcenter = 55555

    # Define el VimId
    $ifxVimId = 444444


    # Obtiene la ruta del perfil del usuario actual
    $userProfile = [System.Environment]::GetFolderPath('UserProfile')

    # Define el nombre del archivo del instalador
    $instalador = "WindowsSensor.MaverickGyr"

    # Combina la ruta del perfil del usuario con la carpeta de Descargas y el nombre del archivo
    $rutaInstalador = Join-Path -Path $userProfile -ChildPath "Downloads\$instalador"

    # Id del cliente Intermedio - IFX Networks Colombia S A S
    $CCID = "08DC0EA2E9E644859FC19402CCE115B9-63"

    #Construye el string con todas las etiquetas
    $ifxCustomTags = "cid_$ifxCID,sid_$ifxSID,vcent_$ifxVcenter,vimId_$ifxVimId"

    # Se construye la cadena de argumentos
    $argumentos = "/install /quiet /norestart CID=$CCID GROUPING_TAGS=$ifxCustomTags"

    # Para la instalacipon se ejecuta desde la ruta del instalador y pasando por parámetro el CCID
    Start-Process -FilePath $rutaInstalador -ArgumentList $argumentos -Wait

    # Muestra la ruta completa del archivo
    Write-Host "Las etiquetas de instalacion son: $ifxCustomTags" 
} else{
    Write-Host "El servicio de sensor ya se encuentra instalado" 
    sc.exe query csagent
}