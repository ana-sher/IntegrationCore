using IntegrationCore.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class ConnectionGenerationResult
    {
        public IEnumerable<TypeDefinitionDto> Types { get; set; }
        public int TypeFromId { get; set; }
        public int TypeToId { get; set; }
        public IEnumerable<FieldConnection> Connections { get; set; }
    }
}
