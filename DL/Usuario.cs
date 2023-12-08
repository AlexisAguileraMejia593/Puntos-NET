using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuarios { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? FechaDeNacimiento { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}
