using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PortalAutorizacion.Services;
using Xunit;

namespace PortalAutorizacion.Tests;

public class EstudiosTests
{
    private static GeneradorReportes CrearServicio()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:EstudiosDb"] = "Server=(localdb)\\mssqllocaldb;Database=EstudiosImagen;Trusted_Connection=True;"
            })
            .Build();
        return new GeneradorReportes(config);
    }

    [Fact]
    public void BuscarEstudio_DevuelveEstudioExistente()
    {
        var servicio = CrearServicio();

        var estudio = servicio.BuscarEstudio("1001");

        Assert.NotNull(estudio);
        Assert.Equal("Paciente Demo 001", estudio.Paciente);
    }

    [Fact]
    public void BuscarEstudio_DevuelveNullSiNoExiste()
    {
        var servicio = CrearServicio();

        var estudio = servicio.BuscarEstudio("9999");

        Assert.Null(estudio);
    }
}
