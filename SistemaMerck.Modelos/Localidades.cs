using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Localidades
{
    public int LocalidadId { get; set; }

    public string NombreLocalidad { get; set; } = null!;

    public int? ProvinciaId { get; set; }

    public virtual Provincia? Provincia { get; set; }
}
