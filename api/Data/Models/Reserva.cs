using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Data.Models
{
    public class Reserva
    {
       [Key]
      public Guid Id { get; set; } = Guid.NewGuid(); // Esto generará un GUID automáticamente al crear un nuevo registro
   // Navigation 
        public Guid AppUserId {get; set; }     // Navigation 
        public AppUser User {get; set; } = null!;

//         UserId: Es una clave foránea opcional que apunta al usuario que realizó la reserva.
// El tipo int? significa que este valor puede ser nulo (si no hay un usuario asignado).
// AppUser: Es la propiedad de navegación que permite acceder al objeto completo del usuario relacionado con la reserva.
// Ejemplo: Si tienes un objeto reserva, podrías acceder al nombre del usuario con algo como:

           // Navigation 
        public Guid HorarioId {get; set; }     // Navigation 
        public Horario Horario {get; set; } = null!;

//         HorarioId: Es una clave foránea opcional que indica el horario reservado.
// El int? hace que la propiedad pueda ser nula, útil si la reserva no tiene un horario específico.
// Horario: Propiedad de navegación para acceder al objeto del horario asociado.
    
    }
}