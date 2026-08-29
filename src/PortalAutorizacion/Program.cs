// Portal de Autorizacion de Estudios de Imagen - Contoso (DEMO Tech Day)
// -----------------------------------------------------------------------------
// AVISO: contiene vulnerabilidades INTENCIONALES y patrones "legacy" para las
// demos de GitHub Advanced Security y de la extension de App Modernization.
// NO usar en produccion. Los datos de paciente se tratan como Confidenciales.
// -----------------------------------------------------------------------------
using PortalAutorizacion.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Patron legacy: el servicio lee la cadena de conexion directamente de la
// configuracion (hardcoded en appsettings.json). Candidato de modernizacion:
// mover a Azure Key Vault + Managed Identity.
builder.Services.AddSingleton<GeneradorReportes>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
