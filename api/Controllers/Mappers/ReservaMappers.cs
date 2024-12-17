using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Implemento;
using api.Controllers.Dtos.Reserva;
using api.Data.Models;

namespace api.Controllers.Mappers
{
    public static class ReservaMappers
    { 

        public static Reserva ToReservatFromCreate(this CreateReservaRequestDto reservaModel)
        {
           return new Reserva
            {
                AppUserId = reservaModel.UserId,

                HorarioId = reservaModel.HorarioId

            };
        }
        
    }
}