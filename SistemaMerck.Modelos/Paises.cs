using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Paises
{
    public int PaisId { get; set; }

    public string NombrePais { get; set; } = null!;

    public virtual ICollection<Provincia> Provincia { get; } = new List<Provincia>();
}
