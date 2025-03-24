using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class TransacationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransacationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transacation
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transacations.Include(t => t.category);
            return View(await applicationDbContext.ToListAsync());
        }



        // GET: Transacation/AddOrEdit
        public IActionResult AddOrEdit()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Transacation/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] Transacation transacation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", transacation.CategoryId);
            return View(transacation);
        }

        

        

        // POST: Transacation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transacation = await _context.Transacations.FindAsync(id);
            if (transacation != null)
            {
                _context.Transacations.Remove(transacation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
