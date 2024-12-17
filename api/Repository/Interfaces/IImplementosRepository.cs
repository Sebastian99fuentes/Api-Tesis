using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers.Dtos.Implemento;
using api.Controllers.Helpers;
using api.Data.Models;

namespace api.Repository.Interfaces
{
    public interface IImplementosRepository
    {
        Task <List<Implemento>>  GetallAsync(); 

        Task<Implemento?> GetByIdAsync(Guid id);

        Task <Implemento?> CreateAsync(Implemento implemento);

        Task <Implemento?> UpdateAsync(Guid id, CreateImplementoRequestDto implementoDto); 

        Task <Implemento?> UpdateImpleAsync(Guid? id, bool upDown);

        Task<Implemento?> DeleteAsync(Guid id);
        Task<bool> Exist(Guid id);  
        
    }
}