using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReentregaPB.Dominio.Model.Entity;
using ReentregaPB.Dominio.Model.Interfaces.Services;
using ReentregaPB.Dominio.Model.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReentregaPB.HttpService
{
    public class PostHttpService : IPostService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<PostHttpOptions> _postHttpOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<IdentityUser> _signInManager;

        public PostHttpService(
            IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PostHttpOptions> postHttpOptions,
            IHttpContextAccessor httpContextAccessor,
            SignInManager<IdentityUser> signInManager)
        {
            _postHttpOptions = postHttpOptions ?? throw new ArgumentNullException(nameof(postHttpOptions));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager;
            ;

            _httpClient = httpClientFactory.CreateClient(postHttpOptions.CurrentValue.Name);
            _httpClient.Timeout = TimeSpan.FromSeconds(_postHttpOptions.CurrentValue.Timeout);
        }

        public async Task<IEnumerable<PostEntity>> GetAllAsync()
        {

            var httpResponseMessage = await _httpClient.GetAsync(_postHttpOptions.CurrentValue.PostPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<PostEntity>>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
            }

            return null;
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {

            var pathWithId = $"{_postHttpOptions.CurrentValue.PostPath}/{id}";
            var httpResponseMessage = await _httpClient.GetAsync(pathWithId);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PostEntity>(await httpResponseMessage.Content
                    .ReadAsStringAsync());
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                await _signInManager.SignOutAsync();
                new RedirectToActionResult("Post", "Index", null);
            }

            return null;
        }

        public async Task InsertAsync(PostEntity postEntity)
        {
            var uriPath = $"{_postHttpOptions.CurrentValue.PostPath}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(postEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PostAsync(uriPath, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task UpdateAsync(PostEntity updatedEntity)
        {
            var pathWithId = $"{_postHttpOptions.CurrentValue.PostPath}/{updatedEntity.Id}";

            var httpContent = new StringContent(JsonConvert.SerializeObject(updatedEntity), Encoding.UTF8, "application/json");

            var httpResponseMessage = await _httpClient.PutAsync(pathWithId, httpContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {

            var pathWithId = $"{_postHttpOptions.CurrentValue.PostPath}/{id}";
            var httpResponseMessage = await _httpClient.DeleteAsync(pathWithId);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                await _signInManager.SignOutAsync();
            }
        }
    }
}
