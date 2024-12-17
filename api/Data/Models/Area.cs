using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Area
    {
          [Key] 
         public Guid Id { get; set; } = Guid.NewGuid(); // Esto generará un GUID automáticamente al crear un nuevo registro 

         
        public string Nombre { get; set; } = string.Empty;         // Nombre de la cancha, sala, etc.
        public string Ubicacion { get; set; }  = string.Empty;       // Ubicación del espacio 
        public string Descripcion { get; set; } = string.Empty;     // Descripción adicional

        public List<Comments> Comments {get; set; } = new List<Comments>();  // Lista de comentarios
    }
}