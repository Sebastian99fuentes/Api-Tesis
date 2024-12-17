using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.ReservaArea
{
    public class ReservaAreaDto
    {
     public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Start { get; set; }
     public DateTime End { get; set; }
    }
}