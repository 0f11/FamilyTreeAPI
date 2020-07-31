using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;
using Extensions;

namespace WebApp.Controllers
{
    public class GenderController : Controller
    {
        private readonly IAppUnitOfWork _uow;
        public GenderController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Gender
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Genders.AllAsync());
        }

        // GET: Gender/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _uow.Genders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // GET: Gender/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gender/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                _uow.Genders.Add(gender);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // GET: Gender/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _uow.Genders.FindAsync(id.Value, User.UserGuidId());
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // POST: Gender/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GenderValue,Start,End,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Gender gender)
        {
            if (id != gender.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Genders.Update(gender);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Genders.ExistsAsync(id, User.UserGuidId()))
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
            return View(gender);
        }

        // GET: Gender/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _uow.Genders.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        // POST: Gender/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Genders.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
