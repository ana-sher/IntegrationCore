using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Models.DTO
{
    public class SystemDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string GetUrlEnding { get; set; }
        public string PostUrlEnding { get; set; }
        public string PutUrlEnding { get; set; }
        public string DeleteUrlEnding { get; set; }
        public string GetByIdentifierUrlEnding { get; set; }
        public TransferType TransferType { get; set; }
        public DataType DataType { get; set; }
        public IEnumerable<ConnectionFieldDefinitionDto> ConnectionFields { get; set; }
    }
}
