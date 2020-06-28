using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReentregaPB.Dominio.Model.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReentregaPB.Extensions
{
    public static class RegisterOptions
    {
        public static void RegisterConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<PostHttpOptions>(configuration.GetSection(nameof(PostHttpOptions)));

        }
    }
}
