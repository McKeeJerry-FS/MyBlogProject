using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlogProject.Data;
using MyBlogProject.Models;

namespace MyBlogProject.Controllers
{
    public class BlogTagModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogTagModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogTagModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tags.Include(b => b.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogTagModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagModel = await _context.Tags
                .Include(b => b.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTagModel == null)
            {
                return NotFound();
            }

            return View(blogTagModel);
        }

        // GET: BlogTagModels/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract");
            return View();
        }

        // POST: BlogTagModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,BlogUserId,Text")] BlogTagModel blogTagModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTagModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogTagModel.PostId);
            return View(blogTagModel);
        }

        // GET: BlogTagModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagModel = await _context.Tags.FindAsync(id);
            if (blogTagModel == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogTagModel.PostId);
            return View(blogTagModel);
        }

        // POST: BlogTagModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,BlogUserId,Text")] BlogTagModel blogTagModel)
        {
            if (id != blogTagModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTagModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTagModelExists(blogTagModel.Id))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogTagModel.PostId);
            return View(blogTagModel);
        }

        // GET: BlogTagModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTagModel = await _context.Tags
                .Include(b => b.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogTagModel == null)
            {
                return NotFound();
            }

            return View(blogTagModel);
        }

        // POST: BlogTagModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTagModel = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(blogTagModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogTagModelExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
