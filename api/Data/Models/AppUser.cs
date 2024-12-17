using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Data.Models
{
    public class AppUser : IdentityUser
    {     
         public List<Reserva> Reserva = new List<Reserva>();
    }
}