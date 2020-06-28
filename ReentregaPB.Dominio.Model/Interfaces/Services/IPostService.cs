using ReentregaPB.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReentregaPB.Dominio.Model.Interfaces.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostEntity>> GetAllAsync();
        Task<PostEntity> GetByIdAsync(int id);
        Task InsertAsync(PostEntity updatedEntity);
        Task UpdateAsync(PostEntity insertedEntity);
        Task DeleteAsync(int id);
    }
}
