using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Provincias
{
    public int Id { get; set; }

    public string Provincia { get; set; } = null!;

    public int? IdPais { get; set; }

    public virtual Pais? IdPaisNavigation { get; set; }
}
