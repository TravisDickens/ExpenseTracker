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
        public IActionResult AddOrEdit(int id = 0)
        {
           PopulateCategories();
            if (id == 0)
            {
                return View(new Transacation());
            }
            else
            {
                return View(_context.Transacations.Find(id));
            }
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
                if(transacation.TransactionId == 0)
                    _context.Add(transacation);
                else
                 _context.Update(transacation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
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

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a category"};
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
       
    }
}
