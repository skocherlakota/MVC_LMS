using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_LMS.Data;
using MVC_LMS.Models;

namespace MVC_LMS.Controllers
{
    public class BorrowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        string userIdX;

        public BorrowsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            userIdX = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        // GET: Borrows
        public async Task<IActionResult> Index()
        {
            return View(await GetBorrows());
        }

        // GET: Borrows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await GetBorrowDetails(id);
            if (borrow.BorrowID == 0)
            {
                return NotFound();
            }

            return View(borrow);
        }

        // GET: Borrows/Create
        public IActionResult Create()
        {
            //var copies = _context.Book.SelectMany(b => b.Copies, (b, s1) => new {b.Title,s1.AccessionNo });
            //var copiesAvialable = _context.Copi.Include(b => b.IsBorrowed == false);

            var availableCopies =
                from e in _context.Book
                from s in e.Copies
                where s.IsBorrowed == false
                select new { e.Title, s.ID };

            var availableCopiesGroupByTitle = availableCopies.GroupBy(s => s.Title, (c, i) => new
            {
                Title = c,
                ID = i.Max(s => s.ID)
            });

            ViewData["CopyID"] = new SelectList(availableCopiesGroupByTitle, "ID", "Title");
            return View();
        }

        // POST: Borrows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowID,CopyID,BorrowDate,ActualReturnDate")] Borrow borrow)
        {
            borrow.UserID = userIdX;
            if (ModelState.IsValid)
            {
                borrow.DateLastUpdated = DateTime.Now;
                borrow.UserLastUpdated = User.Identity.Name;
                borrow.LogicalDeleted = false;
                _context.Add(borrow);
                var copi = await _context.Copi
                .FirstOrDefaultAsync(m => m.ID == borrow.CopyID);
                copi.IsBorrowed = true;
                copi.DateLastUpdated = DateTime.Now;
                copi.UserLastUpdated = User.Identity.Name;
                _context.Update(copi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var availableCopies =
                from e in _context.Book
                from s in e.Copies
                where s.IsBorrowed == false
                select new { e.Title, s.ID };

            var availableCopiesGroupByTitle = availableCopies.GroupBy(s => s.Title, (c, i) => new
            {
                Title = c,
                ID = i.Max(s => s.ID)
            });

            ViewData["CopyID"] = new SelectList(availableCopiesGroupByTitle, "ID", "Title", borrow.CopyID);

            return View(borrow);
        }

        // GET: Borrows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrow = await GetBorrowDetails(id); //await _context.Borrows.FindAsync(id);
            if (borrow.BorrowID == 0)
            {
                return NotFound();
            }

            var borrowedCopi =
                from bk in _context.Book
                from c in bk.Copies
                from br in _context.Borrows
                where c.ID == br.CopyID
                && br.UserID == userIdX
                && br.BorrowID == id
                select new { bk.Title, c.ID };

            ViewData["CopyID"] = new SelectList(borrowedCopi, "ID", "Title", id);
            return View(borrow);
        }

        // POST: Borrows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowID,UserID,CopyID,BorrowDate,ReturnDate,ActualReturnDate,Fine")] Borrow borrow)
        {
            borrow.UserID = userIdX;
            if (id != borrow.BorrowID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrow);
                    var copi = await _context.Copi.FirstOrDefaultAsync(m => m.ID == borrow.CopyID);
                    copi.IsBorrowed = false;
                    copi.DateLastUpdated = DateTime.Now;
                    copi.UserLastUpdated = User.Identity.Name;
                    _context.Update(copi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowExists(borrow.BorrowID))
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
            ViewData["CopyID"] = new SelectList(_context.Copi, "ID", "ID", borrow.CopyID);
            return View(borrow);
        }

        // GET: Borrows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return NotFound();
            }

            var borrow = await GetBorrowDetails(id);

            if (borrow.BorrowID == 0)
            {
                return NotFound();
            }
            return View(borrow);
        }

        // POST: Borrows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrow = await _context.Borrows.FindAsync(id);
            _context.Borrows.Remove(borrow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowExists(int id)
        {
            return _context.Borrows.Any(e => e.BorrowID == id);
        }

        public Task<List<BorrowsViewModel>> GetBorrows()
        {
            //IQueryable<BorrowsViewModel> borrowedBooks = (IQueryable < BorrowsViewModel >)
            List<BorrowsViewModel> borrowslist = new List<BorrowsViewModel>();
            var bb = from b in _context.Book
                     from c in b.Copies
                     from br in _context.Borrows
                     where c.ID == br.CopyID
                     && br.UserID == userIdX
                     orderby (br.BorrowDate)
                     select new { br.BorrowID, b.Title, br.BorrowDate, br.DueDate, br.ActualReturnDate, br.Fine };
            //_context.Borrows.Include(c => c.Copi).Where(x =>x.UserID == userIdX);

            foreach (var item in bb)
            {
                BorrowsViewModel b = new BorrowsViewModel();
                b.BorrowID = item.BorrowID;
                b.Title = item.Title;
                b.BorrowDate = item.BorrowDate;
                b.DueDate = (DateTime)item.DueDate;
                b.ActualReturnDate = item.ActualReturnDate;
                b.Fine = item.Fine;
                borrowslist.Add(b);
            }
            return Task.FromResult(borrowslist);

        }

        public Task<BorrowsViewModel> GetBorrowDetails(int? id)
        {

            BorrowsViewModel borrowslist = new BorrowsViewModel();
            var item = (from bk in _context.Book
                        from c in bk.Copies
                        from br in _context.Borrows
                        where c.ID == br.CopyID
                        && br.UserID == userIdX
                        && br.BorrowID == id
                        orderby (br.BorrowDate)
                        select new { br.BorrowID, bk.Title, br.BorrowDate, br.DueDate, br.ActualReturnDate, br.Fine }).FirstOrDefault();
            BorrowsViewModel b = new BorrowsViewModel();
            if (item == null)
            {
                return Task.FromResult(b);
            }
            else
            {
                b.BorrowID = item.BorrowID;
                b.Title = item.Title;
                b.BorrowDate = item.BorrowDate;
                b.DueDate = (DateTime)item.DueDate;
                b.ActualReturnDate = item.ActualReturnDate;
                b.Fine = item.Fine;
                return Task.FromResult(b);
            }
        }
    }
}
