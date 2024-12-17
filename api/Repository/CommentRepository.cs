using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context )
        {
            _context = context;
        }

        public async Task<Comments> CreateAsync(Comments comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comments?> DeleteAsync(Guid id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync( c => c.Id ==id);
            if(existingComment == null)
            {
                return null;
            }

             _context.Comments.Remove(existingComment);

             await _context.SaveChangesAsync();
             
             return existingComment;
        }

        public async Task<List<Comments>> GetAllAsync()
        {
           return  await _context.Comments.ToListAsync();
        }

        public async Task<Comments?> GetByIdAsync(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comments?> UpdateAsync(Guid id, Comments commentModel)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if(existingComment == null)
            {
                return null;
            }

            existingComment.Comentario = commentModel.Comentario;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}