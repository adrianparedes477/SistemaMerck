using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class DatosFormulario
{
    public int Id { get; set; }

    public string? Clinica { get; set; }

    public string? TipoConsulta { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Url { get; set; }
}
