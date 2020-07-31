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
    public class RelationshipRolesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public RelationshipRolesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: RelationshipRoles
        public async Task<IActionResult> Index()
        {
            var relationshipRoles = await _uow.RelationshipRoles.AllAsync();
            return View(relationshipRoles);
        }

        // GET: RelationshipRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRoles = await _uow.RelationshipRoles
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (relationshipRoles == null)
            {
                return NotFound();
            }

            return View(relationshipRoles);
        }

        // GET: RelationshipRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelationshipRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RelationshipRoles relationshipRoles)
        {
            if (ModelState.IsValid)
            {
                _uow.RelationshipRoles.Add(relationshipRoles);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipRoles);
        }

        // GET: RelationshipRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRoles = await _uow.RelationshipRoles.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (relationshipRoles == null)
            {
                return NotFound();
            }
            return View(relationshipRoles);
        }

        // POST: RelationshipRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,RelationshipRoles relationshipRoles)
        {
            if (id != relationshipRoles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.RelationshipRoles.Update(relationshipRoles);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.RelationshipRoles.ExistsAsync(id, User.UserGuidId()))
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
            return View(relationshipRoles);
        }

        // GET: RelationshipRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRoles = await _uow.RelationshipRoles
                .FirstOrDefaultAsync(id.Value,User.UserGuidId());
            if (relationshipRoles == null)
            {
                return NotFound();
            }

            return View(relationshipRoles);
        }

        // POST: RelationshipRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.RelationshipRoles.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
