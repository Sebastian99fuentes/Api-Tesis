using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.ReservaImplemento;
using api.Data.Models;

namespace api.Controllers.Mappers
{
    public  static class ReservaImplementoMappers
    {
           public static ReservasImplementos ToReservatFromCreate(this CreateReservaImplementoRequestDto reservaModel)
        {

            // Convertir las fechas a UTC antes de crear la entidad
            var startDateUtc = reservaModel.Start.ToUniversalTime();
            var endDateUtc = reservaModel.End.ToUniversalTime();

           return new ReservasImplementos
            {
                AppUserId = reservaModel.UserId,

                ImplementoId = reservaModel.ImplementoId,

                Start =startDateUtc,

                End = endDateUtc,

                Title = reservaModel.Title

                
            };
        }

         public static ReservaImplementoDto ToReservasImplementoDto(this ReservasImplementos ReservasImplementosModel)
        {
            return new ReservaImplementoDto 
            {
                Id = ReservasImplementosModel.Id,
                Start = ReservasImplementosModel.Start,
                End = ReservasImplementosModel.End,
                Title =ReservasImplementosModel.Title
            };
        }

        public static ReservaImplementoDto ToReservasImplementoUserDto(this ReservasImplementos ReservasImplementosModel)
        {
            return new ReservaImplementoDto 
            {
                UserId = ReservasImplementosModel.AppUserId,
                Id = ReservasImplementosModel.Id,
                Start = ReservasImplementosModel.Start,
                End = ReservasImplementosModel.End,
                Title =ReservasImplementosModel.Title
            };
        }  
        
    }
}