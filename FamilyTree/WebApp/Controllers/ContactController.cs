using System;
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
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ContactController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _uow.Contacts.Include(z => z.ContactType, z => z.Person);

            return View(applicationDbContext);
        }

        // GET: Contact/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _uow.Contacts.Include(z => z.ContactType, z => z.Person)
                .ToList();
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contact/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ContactCreateEditViewModel();
            vm.ContactTypesSelectList = new SelectList(await _uow.Contacts.AllAsync(User.UserGuidId()),
                nameof(ContactType.Id), nameof(ContactType.ContactTypeValue));
            vm.PersonsSelectList = new SelectList(await _uow.Contacts.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            return View(vm);
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //contact.Id = Guid.NewGuid();
                _uow.Contacts.Add(vm.Contact);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // var vm = new ContactCreateEditViewModel();
            // vm.ContactTypesSelectList = new SelectList(_context.ContactTypes,nameof(ContactType.Id), nameof(ContactType.ContactTypeValue));
            // vm.PersonsSelectList = new SelectList(_context.Persons, nameof(Person.Id), nameof(Person.FirstName));
            return View(vm);
        }

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vm = new ContactCreateEditViewModel();
            vm.Contact = await _uow.Contacts.FirstOrDefaultAsync(id.Value, User.UserGuidId());

            if (vm.Contact == null)
            {
                return NotFound();
            }

            vm.ContactTypesSelectList = new SelectList(await _uow.ContactTypes.AllAsync(User.UserGuidId()),
                nameof(ContactType.Id), nameof(ContactType.ContactTypeValue));
            vm.PersonsSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id),
                nameof(Person.FirstName));
            return View(vm);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactCreateEditViewModel vm)
        {
            if (id != vm.Contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Contacts.Update(vm.Contact);
                await _uow.Contacts.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //var vm = new ContactCreateEditViewModel();
            vm.ContactTypesSelectList = new SelectList(await _uow.ContactTypes.AllAsync(User.UserGuidId()), nameof(ContactType.Id), nameof(
                ContactType.ContactTypeValue));

            vm.PersonsSelectList = new SelectList(await _uow.Persons.AllAsync(User.UserGuidId()), nameof(Person.Id), nameof(Person.FirstName));
            return View(vm);
        }

        // GET: Contact/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = _uow.Contacts.Include(c => c.ContactType, c => c.Person)
                .FirstOrDefault(m => m.Id == id);
            
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Contacts.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}