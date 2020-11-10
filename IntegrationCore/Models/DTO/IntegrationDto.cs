using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class IntegrationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeFromId { get; set; }
        public IntegrationTypeDefinitionDto TypeFrom { get; set; }
        public int TypeToId { get; set; }
        public IntegrationTypeDefinitionDto TypeTo { get; set; }
    }
}
