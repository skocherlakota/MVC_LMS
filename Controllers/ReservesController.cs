using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_LMS.Data;
using MVC_LMS.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MVC_LMS.Controllers
{
    public class ReservesController : Controller
    {
        private readonly ApplicationDbContext _context;
        string userIdX;

        public ReservesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            userIdX = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        // GET: Reserves
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reserves.Include(r => r.Book).Where(x => x.UserID.Equals(userIdX));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reserves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.ReserveID == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // GET: Reserves/Create
        public IActionResult Create()
        {
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title");
            return View();
        }

        // POST: Reserves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveID,BookID,ReserveDate")] Reserve reserve)
        {
            reserve.UserID = userIdX;
            if (ModelState.IsValid)
            {
                reserve.DateLastUpdated = DateTime.Now;
                reserve.UserLastUpdated = User.Identity.Name;
                reserve.LogicalDeleted = false;
                //string userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                _context.Add(reserve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ConfigureViewModel(reserve);
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", reserve.BookID);
            return View(reserve);
        }

        // GET: Reserves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves.FindAsync(id);
            if (reserve == null)
            {
                return NotFound();
            }
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Author", reserve.BookID);
            return View(reserve);
        }

        // POST: Reserves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveID,BookID,ReserveDate")] Reserve reserve)
        {
            if (id != reserve.ReserveID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveExists(reserve.ReserveID))
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
            ViewData["BookID"] = new SelectList(_context.Book, "ID", "Title", reserve.BookID);
            return View(reserve);
        }

        // GET: Reserves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserve = await _context.Reserves
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.ReserveID == id);
            if (reserve == null)
            {
                return NotFound();
            }

            return View(reserve);
        }

        // POST: Reserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserve = await _context.Reserves.FindAsync(id);
            _context.Reserves.Remove(reserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveExists(int id)
        {
            return _context.Reserves.Any(e => e.ReserveID == id);
        }

        //private void ConfigureViewModel(Reserve model)
        //{
        //    IEnumerable<Book> products = _context.Book;
        //    model.ProductList = new SelectList(products, "ID", "Title",model.BookID);
        //}
    }
}
