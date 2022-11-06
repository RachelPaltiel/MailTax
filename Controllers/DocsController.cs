using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MailTax.Models;

namespace MailTax.Controllers
{
    public class DocsController : Controller
    {
        private readonly MailTaxContext _context;

        public DocsController(MailTaxContext context)
        {
            _context = context;
        }

        // GET: Docs
        public async Task<IActionResult> Index()
        {
            var mailTaxContext = _context.Docs.Include(d => d.Folder);
            return View(await mailTaxContext.ToListAsync());
        }

        // GET: Docs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Docs == null)
            {
                return NotFound();
            }

            var doc = await _context.Docs
                .Include(d => d.Folder)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // GET: Docs/Create
        public IActionResult Create()
        {
            ViewData["FolderId"] = new SelectList(_context.Folders, "FolderId", "FolderId");
            return View();
        }

        // POST: Docs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,FolderId,DocName,DocDesc,DocPath")] Doc doc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FolderId"] = new SelectList(_context.Folders, "FolderId", "FolderId", doc.FolderId);
            return View(doc);
        }

        // GET: Docs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Docs == null)
            {
                return NotFound();
            }

            var doc = await _context.Docs.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }
            ViewData["FolderId"] = new SelectList(_context.Folders, "FolderId", "FolderId", doc.FolderId);
            return View(doc);
        }

        // POST: Docs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocId,FolderId,DocName,DocDesc,DocPath")] Doc doc)
        {
            if (id != doc.DocId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocExists(doc.DocId))
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
            ViewData["FolderId"] = new SelectList(_context.Folders, "FolderId", "FolderId", doc.FolderId);
            return View(doc);
        }

        // GET: Docs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Docs == null)
            {
                return NotFound();
            }

            var doc = await _context.Docs
                .Include(d => d.Folder)
                .FirstOrDefaultAsync(m => m.DocId == id);
            if (doc == null)
            {
                return NotFound();
            }

            return View(doc);
        }

        // POST: Docs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Docs == null)
            {
                return Problem("Entity set 'MailTaxContext.Docs'  is null.");
            }
            var doc = await _context.Docs.FindAsync(id);
            if (doc != null)
            {
                _context.Docs.Remove(doc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocExists(int id)
        {
          return (_context.Docs?.Any(e => e.DocId == id)).GetValueOrDefault();
        }
    }
}
