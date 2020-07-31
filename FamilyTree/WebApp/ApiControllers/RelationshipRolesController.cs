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
    public class RelationshipRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RelationshipRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipRoles>>> GetRelationshipRoles()
        {
            return await _context.RelationshipRoles.ToListAsync();
        }

        // GET: api/RelationshipRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipRoles>> GetRelationshipRoles(Guid id)
        {
            var relationshipRoles = await _context.RelationshipRoles.FindAsync(id);

            if (relationshipRoles == null)
            {
                return NotFound();
            }

            return relationshipRoles;
        }

        // PUT: api/RelationshipRoles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationshipRoles(Guid id, RelationshipRoles relationshipRoles)
        {
            if (id != relationshipRoles.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationshipRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipRolesExists(id))
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

        // POST: api/RelationshipRoles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RelationshipRoles>> PostRelationshipRoles(RelationshipRoles relationshipRoles)
        {
            _context.RelationshipRoles.Add(relationshipRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationshipRoles", new { id = relationshipRoles.Id }, relationshipRoles);
        }

        // DELETE: api/RelationshipRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipRoles>> DeleteRelationshipRoles(Guid id)
        {
            var relationshipRoles = await _context.RelationshipRoles.FindAsync(id);
            if (relationshipRoles == null)
            {
                return NotFound();
            }

            _context.RelationshipRoles.Remove(relationshipRoles);
            await _context.SaveChangesAsync();

            return relationshipRoles;
        }

        private bool RelationshipRolesExists(Guid id)
        {
            return _context.RelationshipRoles.Any(e => e.Id == id);
        }
    }
}
