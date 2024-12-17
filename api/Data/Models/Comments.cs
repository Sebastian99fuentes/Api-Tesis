using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;

namespace api.Models
{
    public class Comments
    {
          [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // Esto generará un GUID automáticamente al crear un nuevo registro 
        

        public string Comentario {get; set;}= string.Empty;
        public DateTime CreatedOn {get; set;} = DateTime.Now;
        

        //  // Navigation 
        // public Guid? UserId {get; set; }     // Navigation 
        // public AppUser? User {get; set; }

        // Navigation 
        public Guid? AreaId {get; set; }     // Navigation 
        public Area? Area {get; set; }


           // Navigation 
        public Guid? ImplementoId {get; set; }     // Navigation 
        public Implemento? Implemento {get; set; }
    }
}