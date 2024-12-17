using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Horario
{

    public class CreateHorarioRequestDto
    {

        [Required] 
          [Range(0,10,ErrorMessage ="DE 0 A 7")]
        public int Dia { get; set; }  // Día específico.

        [Required] 
          [Range(8,22,ErrorMessage ="DE 8 A 22")]
        public int Hora { get; set; }  // Hora específica (para áreas).

         [Required]
        public bool EsImplemento { get; set; }  // Flag para saber si es un implemento   
    }
}