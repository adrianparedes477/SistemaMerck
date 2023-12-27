using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Provincia
{
    public int ProvinciaId { get; set; }

    public string NombreProvincia { get; set; } = null!;

    public int? PaisId { get; set; }

    public virtual ICollection<Localidades> Localidades { get; } = new List<Localidades>();

    public virtual Paises? Pais { get; set; }
}
