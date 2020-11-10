using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IntegratorContext _context;

        public TransactionsController(IntegratorContext context)
        {
            _context = context;
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransaction(int id)
        {
            var transactions = await _context.Transaction
                .Where(el=>el.IntegrationId == id).ToListAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            return transactions;
        }

    }
}
