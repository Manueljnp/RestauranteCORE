using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public partial class RestauranteGetAllSP
    {
        public int IdRestaurante { get; set; }
        public string? Nombre { get; set; }
        public string? Eslogan { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Descripcion { get; set; }

    }
}
