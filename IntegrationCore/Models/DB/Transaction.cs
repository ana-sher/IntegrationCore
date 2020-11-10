using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DB
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string GivenName { get; set; }
        public int IntegrationId { get; set; }
        public Integration Integration { get; set; }
    }
}
