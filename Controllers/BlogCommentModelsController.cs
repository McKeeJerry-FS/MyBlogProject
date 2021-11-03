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
    public class BlogCommentModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogCommentModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogCommentModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(b => b.BlogUser).Include(b => b.Moderator).Include(b => b.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogCommentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCommentModel = await _context.Comments
                .Include(b => b.BlogUser)
                .Include(b => b.Moderator)
                .Include(b => b.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCommentModel == null)
            {
                return NotFound();
            }

            return View(blogCommentModel);
        }

        // GET: BlogCommentModels/Create
        public IActionResult Create()
        {
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract");
            return View();
        }

        // POST: BlogCommentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,BlogUserId,ModeratorId,Body,CommentCreated,CommentUpdate,CommentModerated,CommentDeleted,CommentModeratedBody,ModerationType")] BlogCommentModel blogCommentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogCommentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.BlogUserId);
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.ModeratorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogCommentModel.PostId);
            return View(blogCommentModel);
        }

        // GET: BlogCommentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCommentModel = await _context.Comments.FindAsync(id);
            if (blogCommentModel == null)
            {
                return NotFound();
            }
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.BlogUserId);
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.ModeratorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogCommentModel.PostId);
            return View(blogCommentModel);
        }

        // POST: BlogCommentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,BlogUserId,ModeratorId,Body,CommentCreated,CommentUpdate,CommentModerated,CommentDeleted,CommentModeratedBody,ModerationType")] BlogCommentModel blogCommentModel)
        {
            if (id != blogCommentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCommentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCommentModelExists(blogCommentModel.Id))
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
            ViewData["BlogUserId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.BlogUserId);
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id", blogCommentModel.ModeratorId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Abstract", blogCommentModel.PostId);
            return View(blogCommentModel);
        }

        // GET: BlogCommentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCommentModel = await _context.Comments
                .Include(b => b.BlogUser)
                .Include(b => b.Moderator)
                .Include(b => b.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCommentModel == null)
            {
                return NotFound();
            }

            return View(blogCommentModel);
        }

        // POST: BlogCommentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCommentModel = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(blogCommentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCommentModelExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
