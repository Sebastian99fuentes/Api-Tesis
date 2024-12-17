using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Horario
{
    public class UpdateHorarioRequestDto
    {
          [Required]
         public bool Disponible { get; set; }  // Indica si el horario está libre.

            [Required]
        public bool EsImplemento { get; set; }  // Flag para saber si es un implemento   
    }
}