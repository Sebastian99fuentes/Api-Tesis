using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Implemento
{
    public class CreateImplementoRequestDto
    {

    [Required(ErrorMessage = "El nombre del implemento es obligatorio.")]
    [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
    public string NombreImple { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(1, 300, ErrorMessage = "La cantidad debe estar entre 1 y 300.")]
    public int Cantidad { get; set; }

    }
}