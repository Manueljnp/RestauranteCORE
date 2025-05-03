using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;

namespace DL;

public partial class Restaurante
{
    public int IdRestaurante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Eslogan { get; set; } = null!;

    public byte[]? Imagen { get; set; }

    public string? Descripcion { get; set; }

}
