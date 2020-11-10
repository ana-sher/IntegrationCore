using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;
using IntegrationCore.Models.DTO;
using IntegrationCore.Services;
using Newtonsoft.Json;

namespace IntegrationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationsController : ControllerBase
    {
        private readonly IntegratorContext _context;
        private readonly IMapper _mapper;
        private readonly HttpClient _mappingClient;
        private readonly IntegrationService _integrationService;


        public IntegrationsController(IntegratorContext context, IMapper mapper,
            IntegrationService service,
            IHttpClientFactory clientFactory)
        {
            _context = context;
            _mapper = mapper;
            _mappingClient = clientFactory.CreateClient("mapping-service");
            _integrationService = service;

        }

        // GET: api/Integrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IntegrationDto>>> GetIntegration()
        {
            var result = await _context.Integration
                .Include(el => el.TypeTo)
                .Include(el=>el.TypeFrom)
                .ToListAsync();
            return Ok(_mapper.Map<IEnumerable<IntegrationDto>>(result));
        }

        

        // GET: api/Integrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Integration>> GetIntegration(int id)
        {
            var integration = await _context.Integration.FindAsync(id);

            if (integration == null)
            {
                return NotFound();
            }

            return integration;
        }

        // GET: api/Integrations/Run/5
        [HttpGet("Run/{id}")]
        public async Task<ActionResult<Integration>> RunIntegration(int id)
        {
            await _integrationService.RunIntegration(id);
            return Ok();
        }

        // PUT: api/Integrations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntegration(int id, Integration integration)
        {
            if (id != integration.Id)
            {
                return BadRequest();
            }

            _context.Entry(integration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntegrationExists(id))
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

        // POST: api/Integrations
        [HttpPost]
        public async Task<ActionResult<Integration>> PostIntegration(Integration integration)
        {
            _context.Integration.Add(integration);
            await _context.SaveChangesAsync();

            return Ok(integration);
        }

        // DELETE: api/Integrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Integration>> DeleteIntegration(int id)
        {
            var integration = await _context.Integration.FindAsync(id);
            if (integration == null)
            {
                return NotFound();
            }

            _context.Integration.Remove(integration);
            await _context.SaveChangesAsync();

            return integration;
        }

        private bool IntegrationExists(int id)
        {
            return _context.Integration.Any(e => e.Id == id);
        }
    }
}
