using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class FieldDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeOfFieldId { get; set; }
        public bool IsArray { get; set; }
        public bool Required { get; set; }
        public string DefaultValue { get; set; }
        public int TypeId { get; set; }
    }
}
