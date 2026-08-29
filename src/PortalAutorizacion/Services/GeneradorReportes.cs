using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PortalAutorizacion.Models;

namespace PortalAutorizacion.Services;

// -----------------------------------------------------------------------------
// Servicio de reportes - estilo LEGACY, candidato de modernizacion.
// Patrones que la extension GitHub Copilot app modernization detecta:
//   1. Cadena de conexion leida directamente de configuracion (hardcoded).
//      -> Migrar a Azure Key Vault + Managed Identity.
//   2. Escritura sincrona en el sistema de archivos local (C:\temp).
//      -> Migrar a Azure Blob Storage.
//   3. Serializacion con Newtonsoft.Json 12.0.1 (vulnerable, CVE-2024-21907).
//      -> Actualizar dependencia.
// -----------------------------------------------------------------------------
public class GeneradorReportes
{
    private readonly string _connectionString;

    // "Base de datos" en memoria (mock) para la demo.
    private static readonly List<Estudio> Estudios = new List<Estudio>
    {
        new Estudio { Id = "1001", Paciente = "Paciente Demo 001", Tipo = "TAC", Contraste = true,  Estado = "pendiente"  },
        new Estudio { Id = "1002", Paciente = "Paciente Demo 002", Tipo = "RM",  Contraste = false, Estado = "autorizado" }
    };

    public GeneradorReportes(IConfiguration configuration)
    {
        // Patron legacy: leer el secreto directo de configuracion.
        _connectionString = configuration.GetConnectionString("EstudiosDb");
    }

    // Metodo de dominio testeable: devuelve el estudio o null si no existe.
    public Estudio BuscarEstudio(string id)
    {
        return Estudios.FirstOrDefault(e => e.Id == id);
    }

    // Legacy: genera un reporte y lo escribe en disco local de forma sincrona.
    public string GenerarReporte(string id)
    {
        var estudio = BuscarEstudio(id);
        if (estudio == null)
        {
            return null;
        }

        var reporte = new
        {
            estudio,
            generadoEn = System.DateTime.UtcNow.ToString("o"),
            origen = _connectionString
        };

        var json = JsonConvert.SerializeObject(reporte);

        // Escritura en filesystem local: candidato para Azure Blob Storage.
        var ruta = Path.Combine(Path.GetTempPath(), "reporte_" + id + ".json");
        File.WriteAllText(ruta, json);

        return ruta;
    }
}
