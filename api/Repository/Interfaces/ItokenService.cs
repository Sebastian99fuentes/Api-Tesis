using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;

namespace api.Repository.Interfaces
{
    public interface ItokenService
    {
         string CreateToken(AppUser user);
    }
}