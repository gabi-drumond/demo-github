using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PortalAutorizacion.Controllers;
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

    [Fact]
    public void Reporte_CodificaEntradaHtml()
    {
        var controller = new EstudiosController(CrearServicio());

        var resultado = controller.Reporte("<script>alert(1)</script>");

        var ok = Assert.IsType<OkObjectResult>(resultado);
        Assert.Equal("Generando reporte para &lt;script&gt;alert(1)&lt;/script&gt;", ok.Value);
    }
}
