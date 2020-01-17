using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_LMS.Data;
using MVC_LMS.Models;

namespace MVC_LMS.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            var copies = _context.Book.Include(c => c.Copies).SingleOrDefault(c => c.ID == book.ID);
            book.Copies = book.Copies.ToList();
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ISBN,Author,Title,Price,Published,IsAvailable,Noc")] Book book,int Noc = 1)
        {
            if (ModelState.IsValid)
            {
                book.DateLastUpdated = DateTime.Now;
                book.UserLastUpdated = User.Identity.Name;
                book.LogicalDeleted = false;
                _context.Add(book);
                book.Copies = new List<Copi>();
                Copi c;
                for (int i = 0; i < Noc; i++)
                {
                    c = new Copi
                    {
                        ISBN = book.ISBN,
                        IsBorrowed = false,
                        PurchasePrice = book.Price,
                        AccessionNo = book.ISBN + "." + (i + 1).ToString(),
                        DateLastUpdated = book.DateLastUpdated,
                        UserLastUpdated = book.UserLastUpdated,
                        LogicalDeleted = book.LogicalDeleted
                    };
                    book.Copies.Add(c);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ISBN,Author,Title,Price,Published,IsAvailable")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    book.DateLastUpdated = DateTime.Now;
                    book.UserLastUpdated = User.Identity.Name;
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            //_context.Copi.Remove(_context.Copi.Find().ISBN.Equals(book.ISBN));
            var copies = _context.Book.Include(c => c.Copies).SingleOrDefault(c => c.ID == book.ID);
            foreach (var item in book.Copies.ToList())
            {
                _context.Copi.Remove(item);
            }
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }

   }
}
