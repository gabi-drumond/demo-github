# Guion de demo — GitHub (Contoso, sector hospitalario)

Bloque **App Development**: 3 sesiones seguidas en un único repo `demo-github` (.NET).
Reparte el tiempo en ~1/3 por sesión. Sin horarios fijos: corren una tras otra.

**Antes del evento:** sube el repo a una org con GitHub Advanced Security activo, deja que
CodeQL y Dependabot corran una vez (para que las alertas ya existan), y ten Copilot y la
extensión de modernización listos. Detalle de preparación en `../GUIA_PasoAPaso.md`.

---

## Sesión 1 — GitHub Enterprise + Advanced Security (DevSecOps)
Objetivo: seguridad de código como parte del flujo, no un paso aparte.

1. Pestaña **Security** del repo: panorama (code scanning, Dependabot, secret scanning).
2. **Code scanning**: abrir la alerta de **XSS** en `/api/estudios/buscar` y la de **command
   injection** en `/api/estudios/reporte`. Explicar el *data flow* que CodeQL muestra (de la
   entrada del usuario al *sink*).
3. **Copilot arregla en vivo**: en `EstudiosController.cs`, Copilot Chat →
   *"corrige esta vulnerabilidad y explica la mitigación"*. Mostrar el `diff` seguro
   (codificar la salida con `HtmlEncoder`; usar argumentos sin shell en lugar de `cmd.exe`).
4. **Dependabot**: alerta de `Newtonsoft.Json 12.0.1` (CVE-2024-21907); mostrar el PR automático.
5. **Secret scanning / push protection**: pegar en vivo una cadena de Azure Storage (la tienes en
   `../GUIA_PasoAPaso.md`) en `appsettings.json` e intentar `git push`. GitHub **bloquea** el push.
6. Cierre: "seguridad desplazada a la izquierda, dentro del PR".

**Gancho hospitalario:** los datos de paciente son Confidenciales; asegurar el código es parte
del cumplimiento (privacidad de datos de salud).

## Sesión 2 — GitHub Copilot + App Modernization
Objetivo: mostrar la **extensión GitHub Copilot app modernization** como un proceso gobernado:
evalúa → planifica → aplica → valida, con trazabilidad. (Soporta .NET/Java/C#, no JavaScript.)

1. Abrir la extensión de modernización en VS Code sobre este proyecto. Conectar con la Sesión 1:
   Dependabot encontró `Newtonsoft.Json`; ahora lo convertimos en un plan ejecutable.
2. **Assessment**: dejar que analice el proyecto. Señala el NuGet vulnerable, el target .NET 8
   (candidato a subir a la última versión) y los patrones no *cloud-ready*: cadena de conexión
   hardcoded en `appsettings.json` (→ Azure Key Vault + Managed Identity) y escritura de archivos
   en disco local en `GeneradorReportes.cs` (→ Azure Blob Storage).
3. **Plan gobernado**: revisar el plan y las tareas. Destacar que cada tarea se aprueba, edita o
   excluye antes de tocar el código.
4. **Aplicar**: aprobar la actualización del NuGet (y, si hay tiempo, el upgrade de .NET). Mostrar
   el `diff` y la trazabilidad que genera la extensión.
5. **Validar**: ejecutar `dotnet test` (2/2 en verde) y confirmar que el `NU1903`/CVE desaparece.
6. Cierre: modernizar es evaluar → planificar → aprobar → transformar → validar. Copilot como
   *peer programmer* que acelera el desarrollo interno.

**Plan B:** llevar capturas del assessment y del diff. Si la extensión tarda, mostrar el plan ya
generado y ejecutar solo la validación (`dotnet test`).

**Gancho hospitalario:** mover secretos a Key Vault y archivos a Blob no es solo "buenas prácticas":
es prontitud para la nube y para el manejo seguro de datos clínicos.

## Sesión 3 — ADO + GitHub Copilot
Objetivo: ciclo completo idea → producción.

1. Narrativa: del commit al deploy. Mostrar `.github/workflows/ci.yml` (`dotnet build` + `test`).
2. Abrir `azure-pipelines.yml`; con Copilot: *"agrega un stage de análisis de seguridad (CodeQL)
   antes del stage de Deploy"*. Mostrar cómo Copilot escribe el YAML.
3. Integración GitHub ↔ Azure DevOps (repos/boards/pipelines). El gate al environment
   `producción` con **aprobación manual** se configura en ADO (Environments → Approvals and
   checks), no en el YAML — mencionarlo así.
4. Ligar de vuelta a la Sesión 1: el pipeline **falla** si CodeQL encuentra algo crítico →
   seguridad como *quality gate*.
5. Cierre: idea → PR → checks (test + seguridad) → deploy gobernado.

---

## Plan B (global)
- Si no hay GHAS en la org: mostrar CodeQL corriendo en **Actions** (workflow `codeql.yml`) y los
  resultados; usar capturas de la pestaña Security de respaldo.
- Tener el repo ya clonado y `dotnet build`/`dotnet test` hechos localmente por si la red falla.
- Prompts de Copilot escritos aquí para copiar/pegar (no improvisar bajo el reloj).
