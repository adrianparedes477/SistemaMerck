using System;
using System.Collections.Generic;

namespace SistemaMerck.Modelos;

public partial class Administradores
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
