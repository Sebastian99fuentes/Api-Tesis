using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Controllers.Dtos.Comments;

namespace api.Controllers.Dtos.Area
{
    public class AreaDto
    {
        public Guid Id {get; set;}
        public string Nombre { get; set; } = string.Empty;         // Nombre de la cancha, sala, etc.
        public string Ubicacion { get; set; }  = string.Empty;       // Ubicación del espacio 
        public string Descripcion { get; set; } = string.Empty;     // Descripción adicional
        
         public required  List<CommentDto> Comments {get; set; }  // Lista de comentarios
    }
}