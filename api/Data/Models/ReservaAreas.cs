using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data.Models
{
    public class ReservaAreas
    {
         [Key]
      public Guid Id { get; set; } = Guid.NewGuid(); // Esto generará un GUID automáticamente al crear un nuevo registro

        public Guid AreaId {get; set; }    

           // Navigation 
        public Guid AppUserId {get; set; }     // Navigation 
        public AppUser User {get; set; } = null!; 

        public string Title {get; set; }  =string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

    }
}