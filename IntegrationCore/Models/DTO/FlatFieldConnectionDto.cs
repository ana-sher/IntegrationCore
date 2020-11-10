using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class FlatFieldConnectionDto
    {
        public int Id { get; set; }
        public int IntegrationId { get; set; }
        public int FirstFieldId { get; set; }
        public int SecondFieldId { get; set; }
        public FieldDefinitionDto FirstField { get; set; }
        public FieldDefinitionDto SecondField { get; set; }
        public string FirstFieldFilterFunction { get; set; }
        public string SecondFieldFilterFunction { get; set; }
    }
}
