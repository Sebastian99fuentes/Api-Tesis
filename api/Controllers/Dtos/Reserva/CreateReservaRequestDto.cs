using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Implemento
{
    public class CreateReservaRequestDto
    {
   
    [Required]
    public Guid UserId { get; set; } 

    [Required]
    public Guid HorarioId { get; set; }

    [Required]
     public DateTime StartDate { get; set; }
     
    [Required]
     public DateTime EndDate { get; set; }


    }
}