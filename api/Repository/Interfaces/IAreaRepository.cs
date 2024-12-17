using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Area;
using api.Controllers.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IAreaRepository
    {
        
            Task<List<Area>> GetAllAsync(QueryObject query);

             Task<Area?> GetByIdAsync(Guid id);  ///first or default cant be null

            Task<Area> CreateAsync(Area area);

             Task<Area?> UpdateAsync(Guid id, CreateAreaRequestDto areaDto);

             Task<Area?> DeleteAsync(Guid id); 

             Task<bool> Exist(Guid id); 
        
    }
}