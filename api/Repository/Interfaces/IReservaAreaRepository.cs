using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;

namespace api.Repository.Interfaces
{
    public interface IReservaAreaRepository
    {
             Task<List<ReservaAreas>> GetAllAsync(Guid Id);

              Task<List<ReservaAreas>> GetAllByUserAsync(Guid Id);

             Task<ReservaAreas?> GetByIdAsync(Guid id);  ///first or default cant be null

            Task<ReservaAreas> CreateAsync(ReservaAreas  ReservaAreas);

            // Task<Reservas?> UpdateAsync(int id, CreateAreaRequestDto areaDto);

             Task<ReservaAreas?> DeleteAsync(Guid id); 

              Task<bool> Exist(Guid id);  

              Task<int> CountActiveReservationsByUserAsync(Guid usuarioId);

    }
}