using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;

namespace api.Repository.Interfaces
{
    public interface IReservasImplementosRepository
    {
           Task<List<ReservasImplementos>> GetAllAsync(Guid Id);

              Task<List<ReservasImplementos>> GetAllByUserAsync(Guid Id);

             Task<ReservasImplementos?> GetByIdAsync(Guid id);  ///first or default cant be null

            Task<ReservasImplementos> CreateAsync(ReservasImplementos  ReservasImplementos);

            // Task<Reservas?> UpdateAsync(int id, CreateAreaRequestDto areaDto);

             Task<ReservasImplementos?> DeleteAsync(Guid id); 

              Task<bool> Exist(Guid id);  

              Task<int> CountActiveReservationsByUserAsync(Guid usuarioId);
    }
}