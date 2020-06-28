using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReentregaPB.Data.Context;
using ReentregaPB.Dominio.Model.Entity;
using ReentregaPB.Dominio.Model.Interfaces.Services;

namespace ReentregaPB.Controllers
{
    [Authorize]

    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllAsync();

            if(posts == null)
            {
                return View();
            }

            return View(posts);
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _postService.GetByIdAsync(id.Value);
            if (postEntity == null)
            {
                return NotFound();
            }

            return View(postEntity);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostEntity postEntity)
        {
            if (ModelState.IsValid)
            {
                postEntity.Poster = User.Identity.Name;
                await _postService.InsertAsync(postEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(postEntity);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _postService.GetByIdAsync(id.Value);
            if (postEntity == null)
            {
                return NotFound();
            }
            return View(postEntity);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,PostEntity postEntity)
        {
            if (id != postEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.UpdateAsync(postEntity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _postService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postEntity);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postEntity = await _postService.GetByIdAsync(id.Value);
            if (postEntity == null)
            {
                return NotFound();
            }

            return View(postEntity);
        }

  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
