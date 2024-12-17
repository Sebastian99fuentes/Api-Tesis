using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Area;
using api.Controllers.Helpers;
using api.Data.Models;
using api.Models;

namespace api.Interfaces
{
    public interface IReservasRepository
    {
        
            Task<List<Reserva>> GetAllAsync(Guid Id);

             Task<Reserva?> GetByIdAsync(Guid id);  ///first or default cant be null

            Task<Reserva> CreateAsync(Reserva  reserva);

            // Task<Reservas?> UpdateAsync(int id, CreateAreaRequestDto areaDto);

             Task<Reserva?> DeleteAsync(Guid id); 

              Task<bool> Exist(Guid id);  

              Task<int> CountActiveReservationsByUserAsync(Guid usuarioId);
        
    }
}