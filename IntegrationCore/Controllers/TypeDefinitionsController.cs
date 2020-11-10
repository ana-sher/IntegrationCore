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
using IntegrationCore.Services;

namespace IntegrationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeDefinitionsController : ControllerBase
    {
        private readonly IntegratorContext _context;
        private readonly IMapper _mapper;
        private readonly GeneratorService _generatorService;

        public TypeDefinitionsController(IntegratorContext context, IMapper mapper, GeneratorService generatorService)
        {
            _context = context;
            _mapper = mapper;
            _generatorService = generatorService;
        }

        // GET: api/TypeDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TypeDefinitionDto>>> GetTypeDefinition(int id, [FromQuery] bool showBasic = false)
        {
            var typeDefinition = await _context.TypeDefinition
                .Where(el=>el.SystemId == id || (showBasic && el.IsBasic))
                .Include(el=>el.Fields)
                .ToListAsync();

            if (typeDefinition == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<TypeDefinitionDto>>(typeDefinition));
        }

        // POST: api/FieldConnections/GenerateTypes
        [HttpPost("GenerateTypes")]
        public async Task<ActionResult<IEnumerable<TypeDefinitionDto>>> GenerateTypes([FromBody] GenerateTypesRequest req)
        {
            return Ok(await _generatorService.GenerateTypes(req.SystemId, req.Data, req.Name, req.AddDefaultValues));
        }

        // PUT: api/TypeDefinitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeDefinition(int id, TypeDefinition typeDefinition)
        {
            if (id != typeDefinition.Id)
            {
                return BadRequest();
            }

            _context.Update(typeDefinition);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeDefinitionExists(id))
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

        // POST: api/TypeDefinitions
        [HttpPost]
        public async Task<ActionResult<TypeDefinition>> PostTypeDefinition(TypeDefinition typeDefinition)
        {
            _context.TypeDefinition.Add(typeDefinition);
            await _context.SaveChangesAsync();

            return Ok(typeDefinition);
        }

        // DELETE: api/TypeDefinitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeDefinition>> DeleteTypeDefinition(int id)
        {
            var typeDefinition = await _context.TypeDefinition.FindAsync(id);
            if (typeDefinition == null)
            {
                return NotFound();
            }

            _context.TypeDefinition.Remove(typeDefinition);
            await _context.SaveChangesAsync();

            return typeDefinition;
        }

        private bool TypeDefinitionExists(int id)
        {
            return _context.TypeDefinition.Any(e => e.Id == id);
        }
    }
}
