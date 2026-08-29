# Instrucciones para GitHub Copilot (repositorio de demo - Contoso)

- Responde y comenta el codigo en espanol.
- Proyecto: API ASP.NET Core (.NET) para un Portal de Autorizacion de Estudios de Imagen.
- Prioriza la seguridad: valida y codifica toda entrada del usuario; nunca concatenes
  entradas en comandos del sistema (usa argumentos, evita el shell) ni en HTML (codifica la salida).
- Al corregir hallazgos de code scanning (CodeQL), explica brevemente la causa y la mitigacion.
- Modernizacion: prefiere async/await, inyeccion de dependencias y configuracion segura.
  Los secretos van en Azure Key Vault + Managed Identity, no hardcoded en appsettings.
  El almacenamiento de archivos debe migrar a Azure Blob Storage.
- Manten y actualiza las dependencias NuGet vulnerables a versiones seguras.
- Genera pruebas con xUnit.
- Trata los datos de paciente como Confidenciales.
