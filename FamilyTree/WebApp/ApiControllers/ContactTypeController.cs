using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContactTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ContactType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactType>>> GetContactTypes()
        {
            return await _context.ContactTypes.ToListAsync();
        }

        // GET: api/ContactType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> GetContactType(Guid id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/ContactType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(Guid id, ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ContactType>> PostContactType(ContactType contactType)
        {
            _context.ContactTypes.Add(contactType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactType", new { id = contactType.Id }, contactType);
        }

        // DELETE: api/ContactType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactType>> DeleteContactType(Guid id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _context.ContactTypes.Remove(contactType);
            await _context.SaveChangesAsync();

            return contactType;
        }

        private bool ContactTypeExists(Guid id)
        {
            return _context.ContactTypes.Any(e => e.Id == id);
        }
    }
}
