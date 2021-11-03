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
    public class BlogPostModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogPostModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogPostModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posts.Include(b => b.Blog).Include(b => b.BlogUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogPostModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.Posts
                .Include(b => b.Blog)
                .Include(b => b.BlogUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostModel == null)
            {
                return NotFound();
            }

            return View(blogPostModel);
        }

        // GET: BlogPostModels/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName");
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BlogPostModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BlogId,BlogUserId,Title,Abstract,PostContent,PostCreated,PostUpdated,ReadyStatus,Slug,ImageData,ContentType")] BlogPostModel blogPostModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPostModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName", blogPostModel.BlogId);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.BlogUserId);
            return View(blogPostModel);
        }

        // GET: BlogPostModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.Posts.FindAsync(id);
            if (blogPostModel == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName", blogPostModel.BlogId);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.BlogUserId);
            return View(blogPostModel);
        }

        // POST: BlogPostModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,BlogUserId,Title,Abstract,PostContent,PostCreated,PostUpdated,ReadyStatus,Slug,ImageData,ContentType")] BlogPostModel blogPostModel)
        {
            if (id != blogPostModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPostModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostModelExists(blogPostModel.Id))
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
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "BlogName", blogPostModel.BlogId);
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogPostModel.BlogUserId);
            return View(blogPostModel);
        }

        // GET: BlogPostModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPostModel = await _context.Posts
                .Include(b => b.Blog)
                .Include(b => b.BlogUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPostModel == null)
            {
                return NotFound();
            }

            return View(blogPostModel);
        }

        // POST: BlogPostModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPostModel = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(blogPostModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostModelExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
