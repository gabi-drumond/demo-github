namespace PortalAutorizacion.Models;

// Estudio de imagen (TAC, RM, etc.) sujeto a autorizacion.
public class Estudio
{
    public string Id { get; set; }
    public string Paciente { get; set; }
    public string Tipo { get; set; }
    public bool Contraste { get; set; }
    public string Estado { get; set; }
}
