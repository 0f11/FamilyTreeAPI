using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        
        private readonly IAppUnitOfWork _uow;

        public PersonController (IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _uow.Persons.Include(p => p.Gender);
            return View( applicationDbContext.ToList());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person =  _uow.Persons
                .Include(p => p.Gender)
                .FirstOrDefault(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public async Task<IActionResult> Create()
        {
            var vm = new PersonCreateEditViewModel();
            vm.GendersSelectList = new SelectList(await _uow.Genders.AllAsync(), nameof(Gender.Id), nameof(Gender.GenderValue));
            return View(vm);
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (Person person, PersonCreateEditViewModel vm)
        {
            person.AppUserId = User.UserGuidId();
            if (ModelState.IsValid)
            {
                _uow.Persons.Add(person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var vm = new PersonCreateEditViewModel();
            vm.GendersSelectList = new SelectList(await _uow.Genders.AllAsync(), nameof(Gender.Id), nameof(Gender.GenderValue), vm.Person.GenderId);
            return View(vm);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var vm = new PersonCreateEditViewModel();
            vm.Person = await _uow.Persons.FirstOrDefaultAsync(id.Value, User.UserGuidId());
            if (vm.Person == null)
            {
                return NotFound();
            }
            
            vm.GendersSelectList = new SelectList(await _uow.Genders.AllAsync(), nameof(Gender.Id), nameof(Gender.GenderValue), vm.Person.Gender);
            return View(vm);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonCreateEditViewModel vm)
        {
            vm.Person.AppUserId = User.UserGuidId();

            if (id != vm.Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Persons.Update(vm.Person);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            //var vm = new PersonCreateEditViewModel();
            vm.GendersSelectList = new SelectList(await _uow.Genders.AllAsync(), nameof(Gender.Id), nameof(Gender.GenderValue), vm.Person.Gender);
            return View(vm);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _uow.Persons
                .Include(p => p.Gender)
                .FirstOrDefault(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Persons.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
