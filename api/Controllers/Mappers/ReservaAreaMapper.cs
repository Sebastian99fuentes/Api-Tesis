using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.ReservaArea;
using api.Data.Models;
using api.Repository;

namespace api.Controllers.Mappers
{
    public static class ReservaAreaMapper
    {
           public static ReservaAreas ToReservatFromCreate(this CreateReservaAreaRequestDto reservaModel)
        {

            // Convertir las fechas a UTC antes de crear la entidad
            var startDateUtc = reservaModel.Start.ToUniversalTime();
            var endDateUtc = reservaModel.End.ToUniversalTime();

           return new ReservaAreas
            {
                AppUserId = reservaModel.UserId,

                AreaId = reservaModel.AreaId,

                Start =startDateUtc,

                End = endDateUtc,

                Title = reservaModel.Title

                
            };
        }
   public static ReservaAreaDto ToReservasAreasDto(this ReservaAreas ReservaAreasModel)
        {
            return new ReservaAreaDto 
            {
                Id = ReservaAreasModel.Id,
                Start = ReservaAreasModel.Start,
                End = ReservaAreasModel.End,
                Title =ReservaAreasModel.Title
            };
        }

        public static ReservaAreaUserDto ToReservasAreasUserDto(this ReservaAreas ReservaAreasModel)
        {
            return new ReservaAreaUserDto 
            {
                UserId = ReservaAreasModel.AppUserId,
                Id = ReservaAreasModel.Id,
                Start = ReservaAreasModel.Start,
                End = ReservaAreasModel.End,
                Title =ReservaAreasModel.Title
            };
        }  


    }
}