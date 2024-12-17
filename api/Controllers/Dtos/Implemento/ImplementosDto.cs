using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers.Dtos.Implemento
{
    public class ImplementosDto
    {
        public Guid Id { get; set; }
        public string NombreImple { get; set; } = string.Empty;

        public int Cantidad { get; set; }
    }
}