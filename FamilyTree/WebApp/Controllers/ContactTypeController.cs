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
    public class ContactTypeController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ContactTypeController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ContactType
        public async Task<IActionResult> Index()
        {
            var contactTypes = await _uow.ContactTypes.AllAsync();
            return View(contactTypes);
        }

        // GET: ContactType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _uow.ContactTypes
                .FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // GET: ContactType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                _uow.ContactTypes.Add(contactType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactType);
        }

        // GET: ContactType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _uow.ContactTypes.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (contactType == null)
            {
                return NotFound();
            }
            return View(contactType);
        }

        // POST: ContactType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ContactTypes.Update(contactType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _uow.ContactTypes.ExistsAsync(id, User.UserGuidId()))
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
            return View(contactType);
        }

        // GET: ContactType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactType = await _uow.ContactTypes
                .FirstOrDefaultAsync(id.Value,User.UserGuidId());
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // POST: ContactType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ContactTypes.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
