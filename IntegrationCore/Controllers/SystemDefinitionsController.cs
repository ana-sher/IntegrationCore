using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;
using IntegrationCore.Models.DTO;

namespace IntegrationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemDefinitionsController : ControllerBase
    {
        private readonly IntegratorContext _context;
        private readonly IMapper _mapper;

        public SystemDefinitionsController(IntegratorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SystemDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SystemDefinitionDto>>> GetSystemDefinition()
        {
            var result = await _context.SystemDefinition
                .Include(el=>el.ConnectionFields).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<SystemDefinitionDto>>(result));
        }

        // GET: api/SystemDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemDefinition>> GetSystemDefinition(int id)
        {
            var systemDefinition = await _context.SystemDefinition
                .Include(el => el.ConnectionFields)
                .FirstAsync(el=>el.Id == id);

            if (systemDefinition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SystemDefinitionDto>(systemDefinition));
        }

        // PUT: api/SystemDefinitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSystemDefinition(int id, SystemDefinition systemDefinition)
        {
            if (id != systemDefinition.Id)
            {
                return BadRequest();
            }

            _context.Update(systemDefinition);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemDefinitionExists(id))
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

        // POST: api/SystemDefinitions
        [HttpPost]
        public async Task<ActionResult<SystemDefinition>> PostSystemDefinition(SystemDefinition systemDefinition)
        {
            _context.SystemDefinition.Add(systemDefinition);
            await _context.SaveChangesAsync();

            return Ok(systemDefinition);
        }

        // DELETE: api/SystemDefinitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SystemDefinition>> DeleteSystemDefinition(int id)
        {
            var systemDefinition = await _context.SystemDefinition.FindAsync(id);
            if (systemDefinition == null)
            {
                return NotFound();
            }

            _context.SystemDefinition.Remove(systemDefinition);
            await _context.SaveChangesAsync();

            return systemDefinition;
        }

        private bool SystemDefinitionExists(int id)
        {
            return _context.SystemDefinition.Any(e => e.Id == id);
        }
    }
}
