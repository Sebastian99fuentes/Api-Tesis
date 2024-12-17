using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

namespace api.Controllers.Dtos.ReservaArea
{
    public class CreateReservaAreaRequestDto
    {
    [Required]
    public Guid UserId { get; set; } 

    [Required]
    public Guid AreaId { get; set; }
     [Required]
     public DateTime Start { get; set; }
    [Required]
     public DateTime End { get; set; }

      [Required]
     public string Title { get; set; } =  string.Empty ;

    }
}