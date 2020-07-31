using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.App.EF;
using Domain;
using Extensions;

namespace WebApp.Controllers
{
    public class FamilyController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        public FamilyController( IAppUnitOfWork uow)
        {
            _uow = uow;
            
        }

        // GET: Family
        public async Task<IActionResult> Index()
        {
            var families = await _uow.Families.AllAsync();
            return View(families);
        }

        // GET: Family/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _uow.Families
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: Family/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Family/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Family family)
        {
            if (ModelState.IsValid)
            {
                family.Id = Guid.NewGuid();
                _uow.Families.Add(family);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(family);
        }

        // GET: Family/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _uow.Families.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: Family/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Family family)
        {
            if (id != family.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Families.Update(family);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Families.ExistsAsync(id, User.UserGuidId()))
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
            return View(family);
        }

        // GET: Family/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _uow.Families.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Family/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Families.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
