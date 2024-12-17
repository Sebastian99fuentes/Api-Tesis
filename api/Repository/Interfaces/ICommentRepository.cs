using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAllAsync();

        Task<Comments?> GetByIdAsync(Guid id);

        Task<Comments> CreateAsync(Comments commentModel); 

        Task<Comments?> UpdateAsync(Guid id, Comments commentModel);

         Task<Comments?> DeleteAsync(Guid id);
    }
}