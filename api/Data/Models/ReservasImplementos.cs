using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data.Models
{
    public class ReservasImplementos
    {
         [Key]
      public Guid Id { get; set; } = Guid.NewGuid(); // Esto generará un GUID automáticamente al crear un nuevo registro

         public Guid ImplementoId {get; set; }     // Navigation 
           // Navigation 
        public Guid AppUserId {get; set; }     // Navigation 
        public AppUser User {get; set; } = null!; 
        public string Title {get; set; }  =string.Empty;
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool AllDay { get; set; }
    }
}