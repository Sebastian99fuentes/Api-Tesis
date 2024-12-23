using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Area;
using api.Controllers.Helpers;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDBContext _context;
        public AreaRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAllAsync(QueryObject query)
        {
            // var areas =   _context.Area.Include(c=>c.Comments).AsQueryable(); 

              var areas =   _context.Area.AsQueryable(); 
            
            if(!string.IsNullOrWhiteSpace(query.AreaNombre))
            {
                areas = areas.Where( a => a.Nombre.Contains(query.AreaNombre));
            } 

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("AreaNombre", StringComparison.OrdinalIgnoreCase))
                {
                    areas = query.IsDecsending ? areas.OrderByDescending(a => a.Nombre) : areas.OrderBy( a=> a.Nombre);
                }
            } 
            else
            {
                // Orden por defecto para evitar advertencias de EF Core
              areas = areas.OrderBy(a => a.Id); // Asumiendo que 'Id' es una clave única
            }
//  Este código es un patrón común para implementar paginación eficiente usando LINQ con Entity Framework.
//  El uso de Skip y Take permite extraer solo los elementos necesarios por página, reduciendo la carga y mejorando el rendimiento.
            var skipNumber = (query.PageNumber-1) * query.PageSize;

            return  await  areas.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Area> CreateAsync(Area area)
        {
             await _context.Area.AddAsync(area);
             await  _context.SaveChangesAsync();
             return area;
        }

        public  async Task<Area?> DeleteAsync(Guid  id)
        {
             var areaModel = await _context.Area.FirstOrDefaultAsync(x => x.Id== id);
                if(areaModel == null)
               {
                return null;
               } 

                   _context.Area.Remove(areaModel);
              await  _context.SaveChangesAsync(); 

              return areaModel;
        }

        public async Task<Area?> GetByIdAsync(Guid id)
        {
           return await _context.Area.FirstOrDefaultAsync(x => x.Id== id);
        }

        public async Task<Area?> UpdateAsync(Guid id, CreateAreaRequestDto areaDto)
        {
           var existingArea = await _context.Area.FirstOrDefaultAsync(x => x.Id== id);
             if(existingArea == null)
               {
                return null;
               } 
            
              existingArea.Nombre =  areaDto.Nombre;
             existingArea.Descripcion = areaDto.Descripcion;
             existingArea.Ubicacion = areaDto.Descripcion; 

              await  _context.SaveChangesAsync();

              return existingArea;
        }

        public Task<bool> Exist(Guid id)
        {
            return _context.Area.AnyAsync(a => a.Id ==id);
        }
    }
}