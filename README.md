# Portal de Autorización de Estudios — Demo Contoso (.NET)

Repo de demostración para el Tech Day de **Contoso** (sector hospitalario). Una API en
**ASP.NET Core (.NET 8)** — un "Portal de Autorización de Estudios de Imagen" — que sirve a las
**tres sesiones de GitHub** del bloque *App Development*:

| Sesión | Qué se muestra aquí |
|--------|---------------------|
| **GitHub Enterprise + Advanced Security** (DevSecOps) | `code scanning` con CodeQL detecta XSS y command injection en C#; `Dependabot` alerta el NuGet vulnerable (`Newtonsoft.Json 12.0.1`); `secret scanning`/push protection. |
| **GitHub Copilot + App Modernization** | La **extensión GitHub Copilot app modernization** analiza el proyecto, genera un plan revisable, actualiza el NuGet vulnerable, propone migrar a Azure (Key Vault, Blob) y sube a la última versión de .NET, validando con build + pruebas. |
| **ADO + GitHub Copilot** | `azure-pipelines.yml` + workflows de Actions: ciclo idea → producción, con Copilot generando el pipeline y un *quality gate* de seguridad. |

> ⚠️ Contiene vulnerabilidades **intencionales y rotuladas** y patrones *legacy* para la demo.
> No usar en producción. Los datos de paciente se tratan como **Confidenciales**.

## Por qué .NET
La extensión **GitHub Copilot app modernization** soporta **.NET, Java y C++** (no JavaScript).
Para demostrarla de verdad, el proyecto es .NET. Además, un solo repo .NET cubre las tres
sesiones (CodeQL soporta C#, Dependabot soporta NuGet, secret scanning es agnóstico al lenguaje).

## Estructura
```
demo-github/
├─ src/PortalAutorizacion/
│  ├─ Program.cs                       # arranque ASP.NET Core + DI
│  ├─ Controllers/EstudiosController.cs# XSS + command injection intencionales
│  ├─ Services/GeneradorReportes.cs    # patrones legacy: config hardcoded, IO local, Newtonsoft
│  ├─ Models/Estudio.cs
│  ├─ appsettings.json                 # connection string hardcoded (candidato de modernización)
│  └─ appsettings.Example.json         # guía para la demo de secret scanning
├─ tests/PortalAutorizacion.Tests/     # xUnit (2 pruebas verdes)
├─ .github/
│  ├─ workflows/codeql.yml             # code scanning (CodeQL, C#)
│  ├─ workflows/ci.yml                 # dotnet build + test
│  ├─ dependabot.yml                   # alertas NuGet + actions
│  └─ copilot-instructions.md          # instrucciones de Copilot (español, seguridad, .NET)
├─ azure-pipelines.yml                 # pipeline ADO idea -> prod (.NET)
└─ PortalAutorizacion.sln
```

## Correr localmente
```powershell
dotnet build PortalAutorizacion.sln
dotnet test  PortalAutorizacion.sln         # 2/2 en verde
dotnet run --project src/PortalAutorizacion # http://localhost:5xxx/swagger
```
Endpoints de demo: `/api/estudios/buscar?q=`, `/api/estudios/reporte?nombre=`, `/api/estudios/1001`.

## Hallazgos esperados de GHAS (al subir a GitHub con Advanced Security activo)
- **Code scanning (CodeQL):** Reflected XSS en `/api/estudios/buscar`; Command injection en
  `/api/estudios/reporte`.
- **Dependabot:** `Newtonsoft.Json 12.0.1` (CVE-2024-21907) → sugiere actualizar. El aviso también
  aparece en el build como `NU1903`.
- **Secret scanning / push protection:** se demuestra pegando en vivo una cadena de Azure Storage
  con formato real (ver `GUIA_PasoAPaso.md`, fuera del repo).

El guion paso a paso está en `DEMO_SCRIPT.md` y en `../GUIA_PasoAPaso.md`.
