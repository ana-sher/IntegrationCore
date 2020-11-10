using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class GenerateTypesRequest
    {
        public int SystemId { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
        public bool AddDefaultValues { get; set; }
    }
}
