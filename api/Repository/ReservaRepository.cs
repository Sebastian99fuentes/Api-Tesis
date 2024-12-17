using api.Controllers.Mappers;
using api.Data;
using api.Data.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReservaRepository : IReservasRepository
    {
        private readonly  ApplicationDBContext _context;

        public ReservaRepository(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<Reserva?> CreateAsync(Reserva reserva)
        {
             await _context.Reserva.AddAsync(reserva);
             await _context.SaveChangesAsync();

             return reserva;
        }

         public async Task<Reserva?> DeleteAsync(Guid id)
        {
           var reserva = _context.Reserva.FirstOrDefault(i => i.Id == id);
           if(reserva == null)
           {
            return null;
           }

           _context.Remove(reserva);
           await _context.SaveChangesAsync();

           return reserva;
        }

        public async Task<Reserva?> GetByIdAsync(Guid id)
        { 
          
            return await _context.Reserva.Include(h=>h.Horario).AsQueryable().FirstOrDefaultAsync(x => x.Id== id);

          
        }

        public Task<bool> Exist(Guid id)
        {
             return _context.Reserva.AnyAsync(a => a.Id ==id);
        }

        public  async Task<List<Reserva>> GetAllAsync(Guid userId)
        {
               // Filtramos las reservas por userId y también incluimos la información del horario relacionado.
               var reservas = await _context.Reserva
                   .Where(r => r.AppUserId == userId)
                   .Include(r => r.Horario) // Incluimos el horario relacionado.
                   .ToListAsync();

               return reservas;
        }

        public async  Task<int> CountActiveReservationsByUserAsync(Guid usuarioId)
        {
             return await _context.Reserva
            .CountAsync(r => r.AppUserId == usuarioId);
        }
    }
}