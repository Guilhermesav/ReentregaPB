using Newtonsoft.Json;
using ReentregaPB.Dominio.Model.Entity;
using ReentregaPB.Dominio.Model.Interfaces.Infraestrutura;
using ReentregaPB.Dominio.Model.Interfaces.Repository;
using ReentregaPB.Dominio.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReentregaPB.Dominio.Service.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IQueueMessage _queueService;
        private readonly IBlobService _blobService;

        public PostService(
            IPostRepository postRepository, IBlobService blobService,IQueueMessage queueService)
        {
            _postRepository = postRepository;
            _blobService = blobService;
            _queueService = queueService;
        }

        public async Task DeleteAsync(int id)
        {
             await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PostEntity>> GetAllAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(PostEntity postEntity)
        {
            var message = new
            {
                ImageURI = postEntity.UrlFoto,
                Id = $"{postEntity.Id}",
            };

            var jsonMessage = JsonConvert.SerializeObject(message);
            var bytesJsonMessage = UTF8Encoding.UTF8.GetBytes(jsonMessage);
            string jsonMessageBase64 = Convert.ToBase64String(bytesJsonMessage);

            await _queueService.SendAsync(jsonMessageBase64, "queue-image-insert");

            await _postRepository.InsertAsync(postEntity);
        }

        public async Task UpdateAsync(PostEntity insertedModel)
        {
            await _postRepository.UpdateAsync(insertedModel);
        }

    }
}

