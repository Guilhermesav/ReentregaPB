
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReentregaPB.Data.Context;
using ReentregaPB.Data.Repository;
using ReentregaPB.Dominio.Model.Interfaces.Infraestrutura;
using ReentregaPB.Dominio.Model.Interfaces.Repository;
using ReentregaPB.Dominio.Model.Interfaces.Services;
using ReentregaPB.Dominio.Service.Service;
using ReentregaPB.Infraestrutura.Services.Blob;
using System;
using ReentregaPB.Infraestrutura.Services.Queue;
namespace ReentregaPB.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<PostContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PostContext")));

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IBlobService, BlobService>(provider =>
            new BlobService(configuration.GetConnectionString("ConnectionStringStorageAccount")));
            services.AddScoped<IQueueMessage, QueueMessage>(provider =>
              new QueueMessage(configuration.GetConnectionString("ConnectionStringStorageAccount")));
        }
    }
}
