using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Localidades
{
    public int Id { get; set; }

    public int IdProvincia { get; set; }

    public string Localidad { get; set; } = null!;
}

