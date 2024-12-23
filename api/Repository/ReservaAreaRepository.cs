using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Repository.Interfaces;

namespace api.Repository
{
    public class ReservaAreaRepository : IReservaAreaRepository
    {
           private readonly  ApplicationDBContext _context;
           public ReservaAreaRepository(ApplicationDBContext context)
           {
                _context = context;
           }

     
        public async Task<ReservaAreas> CreateAsync(ReservaAreas ReservaAreas)
        {
             await _context.ReservaAreas.AddAsync(ReservaAreas);
             await _context.SaveChangesAsync();

             return ReservaAreas;
        }

        public async Task<ReservaAreas?> DeleteAsync(Guid id)
        {
              var reserva = _context.ReservaAreas.FirstOrDefault(i => i.Id == id);
           if(reserva == null)
           {
            return null;
           }

           _context.Remove(reserva);
           await _context.SaveChangesAsync();

           return reserva;
        }

        public  Task<bool> Exist(Guid id)
        {
            return _context.ReservaAreas.AnyAsync(a => a.Id ==id);
        }

        public async Task<List<ReservaAreas>> GetAllAsync(Guid Id)
        {
                  var reservas = await _context.ReservaAreas
                   .Where(r => r.AreaId == Id)
                   .ToListAsync();

               return reservas;
        }

        public async Task<ReservaAreas?> GetByIdAsync(Guid id)
        {
             return await _context.ReservaAreas.FirstOrDefaultAsync(x => x.Id== id);

        }

           public async Task<int> CountActiveReservationsByUserAsync(Guid usuarioId)
        {
                  
             return await _context.ReservaAreas.CountAsync(r => r.AppUserId == usuarioId);
        }

        public async Task<List<ReservaAreas>> GetAllByUserAsync(Guid Id)
        {
                     // Filtramos las reservas por userId y también incluimos la información del horario relacionado.
               var reservas = await _context.ReservaAreas
                   .Where(r => r.AppUserId == Id)
                   .ToListAsync();

               return reservas;
        }
    }
}