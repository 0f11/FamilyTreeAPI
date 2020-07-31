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
    public class RelationshipTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RelationshipTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipTypes>>> GetRelationshipTypes()
        {
            return await _context.RelationshipTypes.ToListAsync();
        }

        // GET: api/RelationshipTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipTypes>> GetRelationshipTypes(Guid id)
        {
            var relationshipTypes = await _context.RelationshipTypes.FindAsync(id);

            if (relationshipTypes == null)
            {
                return NotFound();
            }

            return relationshipTypes;
        }

        // PUT: api/RelationshipTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationshipTypes(Guid id, RelationshipTypes relationshipTypes)
        {
            if (id != relationshipTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationshipTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipTypesExists(id))
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

        // POST: api/RelationshipTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RelationshipTypes>> PostRelationshipTypes(RelationshipTypes relationshipTypes)
        {
            _context.RelationshipTypes.Add(relationshipTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationshipTypes", new { id = relationshipTypes.Id }, relationshipTypes);
        }

        // DELETE: api/RelationshipTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipTypes>> DeleteRelationshipTypes(Guid id)
        {
            var relationshipTypes = await _context.RelationshipTypes.FindAsync(id);
            if (relationshipTypes == null)
            {
                return NotFound();
            }

            _context.RelationshipTypes.Remove(relationshipTypes);
            await _context.SaveChangesAsync();

            return relationshipTypes;
        }

        private bool RelationshipTypesExists(Guid id)
        {
            return _context.RelationshipTypes.Any(e => e.Id == id);
        }
    }
}
