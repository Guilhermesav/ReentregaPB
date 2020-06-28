using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReentregaPB.Dominio.Model.Interfaces.Services;
using ReentregaPB.Dominio.Model.Options;
using ReentregaPB.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReentregaPB.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var estadoHttpOptionsSection = configuration.GetSection(nameof(PostHttpOptions));
            var estadoHttpOptions = estadoHttpOptionsSection.Get<PostHttpOptions>();

            services.AddHttpClient(estadoHttpOptions.Name, x => { x.BaseAddress = estadoHttpOptions.ApiBaseUrl; });

            services.AddScoped<IPostService, PostHttpService>();

        }
    }
}
