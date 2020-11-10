using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Models.DTO
{
    public class ConnectionFieldDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldTransferRole Role { get; set; }
        public int SystemId { get; set; }
    }
}
