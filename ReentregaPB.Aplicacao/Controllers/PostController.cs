using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReentregaPB.Data.Context;
using ReentregaPB.Dominio.Model.Entity;
using ReentregaPB.Dominio.Model.Exceptions;
using ReentregaPB.Dominio.Model.Interfaces.Services;

namespace ReentregaPB.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostEntity>>> GetPosts()
        {
            var posts = await _postService.GetAllAsync();

            return posts.ToList();
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PostEntity>> GetPostEntity(int id)
        {
            if (id<= 0)
            {
                return NotFound();
            }

            var postEntity = await _postService.GetByIdAsync(id);

            return Ok(postEntity);
        }

 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostEntity(int id, PostEntity postEntity)
        {
            if (id != postEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _postService.UpdateAsync(postEntity);
                return Ok();
            }
            catch (EntityValidationException error)
            {
                ModelState.AddModelError(error.PropertyName, error.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
                return BadRequest(ModelState);
            }
        }

        // POST: api/Post
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PostEntity>> PostPostEntity(PostEntity postEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _postService.InsertAsync(postEntity);

                return Ok(postEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PostEntity>> DeletePostEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var postEntity = await _postService.GetByIdAsync(id);
            if (postEntity == null)
            {
                return NotFound();
            }

            await _postService.DeleteAsync(id);

            return Ok(postEntity);
        }
    }
}
