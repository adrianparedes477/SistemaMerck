using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Pais
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Provincias> Provincia { get; } = new List<Provincias>();
}
