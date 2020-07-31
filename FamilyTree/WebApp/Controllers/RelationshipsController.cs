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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class RelationshipsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public RelationshipsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Relationships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _uow.Relationships.Include(r => r.Family,
                r => r.PersonOne,
                r => r.PersonTwo,
                r => r.RelationshipTypes,
                r => r.RoleOne,
                r => r.RoleTwo);
            
            return View(applicationDbContext.ToList());
        }

        // GET: Relationships/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationships = _uow.Relationships.Include(r => r.Family,
                    r => r.PersonOne,
                    r => r.PersonTwo,
                    r => r.RelationshipTypes,
                    r => r.RoleOne,
                    r => r.RoleTwo)
                .FirstOrDefault(m => m.Id == id);
            if (relationships == null)
            {
                return NotFound();
            }

            return View(relationships);
        }

        // GET: Relationships/Create
        public async Task<IActionResult> Create()
        {
            var vm = new RelationshipsCreateEditViewModel();
            vm.FamiliesSelectList = new SelectList(await _uow.Families.AllAsync(), nameof(Family.Id), nameof(Family.Name));
            vm.Persons1SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.Persons2SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.RelationshipRoles1SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            vm.RelationshipRoles2SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            return View(vm);
        }

        // POST: Relationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( RelationshipsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.Relationships.Add(vm.Relationships);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var vm = new RelationshipsCreateEditViewModel();
            vm.FamiliesSelectList = new SelectList(await _uow.Families.AllAsync(), nameof(Family.Id), nameof(Family.Name));
            vm.Persons1SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.Persons2SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.RelationshipRoles1SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            vm.RelationshipRoles2SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            return View(vm);
        }

        // GET: Relationships/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new RelationshipsCreateEditViewModel();
            vm.Relationships = await _uow.Relationships.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.Relationships == null)
            {
                return NotFound();
            }
            
            vm.FamiliesSelectList = new SelectList(await _uow.Families.AllAsync(), nameof(Family.Id), nameof(Family.Name));
            vm.Persons1SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.Persons2SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.RelationshipRoles1SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            vm.RelationshipRoles2SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            return View(vm);
        }

        // POST: Relationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RelationshipsCreateEditViewModel vm)
        {
            if (id != vm.Relationships.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Relationships.Update(vm.Relationships);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Relationships.ExistsAsync(id, User.UserGuidId()))
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
            //var vm = new RelationshipsCreateEditViewModel();
            vm.FamiliesSelectList = new SelectList(await _uow.Families.AllAsync(), nameof(Family.Id), nameof(Family.Name));
            vm.Persons1SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.Persons2SelectList = new SelectList(await _uow.Persons.AllAsync(), nameof(Person.Id), nameof(Person.FirstLastName));
            vm.RelationshipRoles1SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            vm.RelationshipRoles2SelectList = new SelectList(await _uow.RelationshipRoles.AllAsync(), nameof(RelationshipRoles.Id), nameof(RelationshipRoles.RelationshipRolesValue));
            return View(vm);
        }

        // GET: Relationships/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationships = _uow.Relationships.Include(r => r.Family,
                    r => r.PersonOne,
                    r => r.PersonTwo,
                    r => r.RelationshipTypes,
                    r => r.RoleOne,
                    r => r.RoleTwo)
                .FirstOrDefault(m => m.Id == id);
            if (relationships == null)
            {
                return NotFound();
            }

            return View(relationships);
        }

        // POST: Relationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Relationships.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
