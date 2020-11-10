using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;
using IntegrationCore.Services;
using IntegrationCore.Models.DTO;

namespace IntegrationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldConnectionsController : ControllerBase
    {
        private readonly IntegratorContext _context;
        private readonly GeneratorService _generatorService;

        public FieldConnectionsController(IntegratorContext context, GeneratorService generatorService)
        {
            _context = context;
            _generatorService = generatorService;
        }

        // GET: api/FieldConnections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldConnection>>> GetFieldConnection()
        {
            return await _context.FieldConnection.ToListAsync();
        }

        // GET: api/FieldConnections/GenerateConnections
        [HttpPost("GenerateConnections")]
        public async Task<ActionResult<IEnumerable<FieldConnection>>> GenerateConnections([FromBody] GenerateConnectionsRequest req)
        {
            return Ok(await _generatorService.GenerateConnections(req.TypeFromId, req.TypeToId, req.ObjFrom, req.ObjTo));
        }

        // GET: api/FieldConnections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FieldConnection>> GetFieldConnection(int id)
        {
            var fieldConnection = await _context.FieldConnection.FindAsync(id);

            if (fieldConnection == null)
            {
                return NotFound();
            }

            return fieldConnection;
        }

        // PUT: api/FieldConnections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFieldConnection(int id, FieldConnection fieldConnection)
        {
            if (id != fieldConnection.Id)
            {
                return BadRequest();
            }

            _context.Entry(fieldConnection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldConnectionExists(id))
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

        // POST: api/FieldConnections
        [HttpPost]
        public async Task<ActionResult<FieldConnection>> PostFieldConnection(FieldConnection fieldConnection)
        {
            _context.FieldConnection.Add(fieldConnection);
            await _context.SaveChangesAsync();

            return Ok(fieldConnection);
        }

        // DELETE: api/FieldConnections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FieldConnection>> DeleteFieldConnection(int id)
        {
            var fieldConnection = await _context.FieldConnection.FindAsync(id);
            if (fieldConnection == null)
            {
                return NotFound();
            }

            _context.FieldConnection.Remove(fieldConnection);
            await _context.SaveChangesAsync();

            return fieldConnection;
        }

        private bool FieldConnectionExists(int id)
        {
            return _context.FieldConnection.Any(e => e.Id == id);
        }
    }
}
