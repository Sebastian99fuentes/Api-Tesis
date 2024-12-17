using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.ReservaImplemento
{
    public class CreateReservaImplementoRequestDto
    {
         [Required]
    public Guid UserId { get; set; } 

    [Required]
    public Guid ImplementoId { get; set; }
     [Required]
     public DateTime Start { get; set; }
    [Required]
     public DateTime End { get; set; }

      [Required]
     public string Title { get; set; } =  string.Empty ;
    }
}