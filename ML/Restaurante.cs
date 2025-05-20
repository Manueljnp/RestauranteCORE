using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Restaurante
    {
        public int IdRestaurante { get; set; }

        [DisplayName("Nombre del Restaurante")]
        [Required(ErrorMessage = "Nombre de Producto es un campo obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+(?:\s+[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ]+){0,2}$", ErrorMessage = "Nombre inválido")]
        public string? Nombre { get; set; }
        public string? Eslogan { get; set; }
        public byte[]? Imagen { get; set; }
        public string? Descripcion { get; set; }

        public List<object>? Restaurantes { get; set; }
    }
}
