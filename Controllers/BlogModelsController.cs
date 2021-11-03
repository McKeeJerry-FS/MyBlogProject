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
    public class BlogModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        // Constructor
        public BlogModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Blogs.Include(b => b.BlogUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs
                .Include(b => b.BlogUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogModel == null)
            {
                return NotFound();
            }

            return View(blogModel);
        }

        // GET: BlogModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogName,Description,Image")] BlogModel blogModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogModel.BlogUserId);
            return View(blogModel);
        }

        // GET: BlogModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs.FindAsync(id);
            if (blogModel == null)
            {
                return NotFound();
            }
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogModel.BlogUserId);
            return View(blogModel);
        }

        // POST: BlogModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogUserId,BlogName,Description,BlogCreated,BlogUpdated,ImageData,ContentType")] BlogModel blogModel)
        {
            if (id != blogModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogModelExists(blogModel.Id))
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
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogModel.BlogUserId);
            return View(blogModel);
        }

        // GET: BlogModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogModel = await _context.Blogs
                .Include(b => b.BlogUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogModel == null)
            {
                return NotFound();
            }

            return View(blogModel);
        }

        // POST: BlogModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogModel = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blogModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogModelExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
