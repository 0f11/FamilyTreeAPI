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
    public class RelationshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Relationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationships>>> GetRelationships()
        {
            return await _context.Relationships.ToListAsync();
        }

        // GET: api/Relationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relationships>> GetRelationships(Guid id)
        {
            var relationships = await _context.Relationships.FindAsync(id);

            if (relationships == null)
            {
                return NotFound();
            }

            return relationships;
        }

        // PUT: api/Relationships/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationships(Guid id, Relationships relationships)
        {
            if (id != relationships.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationships).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipsExists(id))
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

        // POST: api/Relationships
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Relationships>> PostRelationships(Relationships relationships)
        {
            _context.Relationships.Add(relationships);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationships", new { id = relationships.Id }, relationships);
        }

        // DELETE: api/Relationships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Relationships>> DeleteRelationships(Guid id)
        {
            var relationships = await _context.Relationships.FindAsync(id);
            if (relationships == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationships);
            await _context.SaveChangesAsync();

            return relationships;
        }

        private bool RelationshipsExists(Guid id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
