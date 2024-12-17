using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Data.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReservasImplementosRepository : IReservasImplementosRepository
    {
        private readonly  ApplicationDBContext _context;

        public ReservasImplementosRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> CountActiveReservationsByUserAsync(Guid usuarioId)
        {
                        return await _context.ReservasImplementos
            .CountAsync(r => r.AppUserId == usuarioId);
        }

        public async Task<ReservasImplementos> CreateAsync(ReservasImplementos ReservasImplementos)
        {
           await _context.ReservasImplementos.AddAsync(ReservasImplementos);
             await _context.SaveChangesAsync();

             return ReservasImplementos;
        }

        public async Task<ReservasImplementos?> DeleteAsync(Guid id)
        {
            var reserva = _context.ReservasImplementos.FirstOrDefault(i => i.Id == id);
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
            return _context.ReservasImplementos.AnyAsync(a => a.Id ==id);
        }
        public async Task<List<ReservasImplementos>> GetAllAsync(Guid Id)
        {
            var reservas = await _context.ReservasImplementos
                   .Where(r => r.ImplementoId == Id)
                   .ToListAsync();

               return reservas;
        }

        public async Task<List<ReservasImplementos>> GetAllByUserAsync(Guid Id)
        {
                // Filtramos las reservas por userId y también incluimos la información del horario relacionado.
               var reservas = await _context.ReservasImplementos
                   .Where(r => r.AppUserId == Id)
                   .ToListAsync();

               return reservas;
        }

        public async Task<ReservasImplementos?> GetByIdAsync(Guid id)
        {
            return await _context.ReservasImplementos.FirstOrDefaultAsync(x => x.Id== id);
        }
    }
}