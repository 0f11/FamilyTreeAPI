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
    public class RelationshipTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public RelationshipTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: RelationshipTypes
        public async Task<IActionResult> Index()
        {
            var relationshipTypes = await _uow.RelationshipTypes.AllAsync();
            return View(relationshipTypes);
        }

        // GET: RelationshipTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipTypes = await _uow.RelationshipTypes
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (relationshipTypes == null)
            {
                return NotFound();
            }

            return View(relationshipTypes);
        }

        // GET: RelationshipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelationshipTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RelationshipTypes relationshipTypes)
        {
            if (ModelState.IsValid)
            {
                _uow.RelationshipTypes.Add(relationshipTypes);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipTypes);
        }

        // GET: RelationshipTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipTypes = await _uow.RelationshipTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (relationshipTypes == null)
            {
                return NotFound();
            }
            return View(relationshipTypes);
        }

        // POST: RelationshipTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RelationshipTypes relationshipTypes)
        {
            if (id != relationshipTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.RelationshipTypes.Update(relationshipTypes);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.RelationshipTypes.ExistsAsync(id, User.UserGuidId()))
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
            return View(relationshipTypes);
        }

        // GET: RelationshipTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipTypes = await _uow.RelationshipTypes
                .FirstOrDefaultAsync(id.Value,User.UserGuidId());
            if (relationshipTypes == null)
            {
                return NotFound();
            }

            return View(relationshipTypes);
        }

        // POST: RelationshipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.RelationshipTypes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
