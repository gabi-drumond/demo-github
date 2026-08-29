using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalAutorizacion.Services;

namespace PortalAutorizacion.Controllers;

[ApiController]
[Route("api/estudios")]
public class EstudiosController : ControllerBase
{
    private readonly GeneradorReportes _reportes;

    public EstudiosController(GeneradorReportes reportes)
    {
        _reportes = reportes;
    }

    // [DEMO CodeQL: Reflected XSS] el parametro del usuario se refleja en HTML sin
    // codificar. CodeQL lo detecta como cs/web/xss. Copilot lo corrige codificando
    // la salida (HtmlEncoder) o devolviendo texto plano.
    [HttpGet("buscar")]
    public ContentResult Buscar([FromQuery] string q)
    {
        var html = "<h1>Resultados para: " + q + "</h1>";
        return Content(html, "text/html");
    }

    // [DEMO CodeQL: Command Injection] la entrada del usuario se concatena a un
    // comando del sistema. CodeQL lo detecta como cs/command-line-injection.
    // Copilot lo corrige evitando el shell y pasando argumentos de forma segura.
    [HttpGet("reporte")]
    public IActionResult Reporte([FromQuery] string nombre)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c echo Generando reporte para " + nombre,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        using var proceso = Process.Start(psi);
        var salida = proceso.StandardOutput.ReadToEnd();
        proceso.WaitForExit();
        return Ok(salida);
    }

    // Endpoint limpio: consulta un estudio por id.
    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(string id)
    {
        var estudio = _reportes.BuscarEstudio(id);
        if (estudio == null)
        {
            return NotFound("no encontrado");
        }
        return Ok(estudio);
    }
}
