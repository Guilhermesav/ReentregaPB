using Microsoft.EntityFrameworkCore;
using ReentregaPB.Data.Context;
using ReentregaPB.Dominio.Model.Entity;
using ReentregaPB.Dominio.Model.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReentregaPB.Data.Repository
{
    public class PostRepository: IPostRepository
    {

        private readonly PostContext _context;

        public PostRepository(
            PostContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var postEntity = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(postEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostEntity>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        

        public async Task InsertAsync(PostEntity updatedModel)
        {
            _context.Add(updatedModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PostEntity insertedModel)
        {
            _context.Update(insertedModel);
            await _context.SaveChangesAsync();
        }
    }
}

