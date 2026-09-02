using System.Text.Encodings.Web;
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
        var nombreSeguro = HtmlEncoder.Default.Encode(nombre ?? string.Empty);
        var salida = $"Generando reporte para {nombreSeguro}";
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
