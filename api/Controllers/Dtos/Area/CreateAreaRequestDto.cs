using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Area
{
    public class CreateAreaRequestDto
    {
        
        [Required]
        [MinLength(1, ErrorMessage ="Nombre must be 50 characters")]
        [MaxLength(50, ErrorMessage ="Title cannot be over 300 characters")]
        public string Nombre { get; set; } = string.Empty;         // Nombre de la cancha, sala, etc.



        [Required]
        [MinLength(1, ErrorMessage ="Nombre must be 50 characters")]
        [MaxLength(50, ErrorMessage ="Title cannot be over 300 characters")]
        public string Ubicacion { get; set; }  = string.Empty;       // Ubicación del espacio 



        [Required]
        [MinLength(1, ErrorMessage ="Nombre must be 50 characters")]
        [MaxLength(50, ErrorMessage ="Title cannot be over 300 characters")]
        public string Descripcion { get; set; } = string.Empty;     // Descripción adicional

    }
}