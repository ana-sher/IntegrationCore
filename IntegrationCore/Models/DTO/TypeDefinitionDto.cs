using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Models.DTO
{
    public class TypeDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlEnding { get; set; }
        public string GetByIdFieldWrapper { get; set; }
        public string GetFieldWrapper { get; set; }
        public string PostFieldWrapper { get; set; }
        public string PutFieldWrapper { get; set; }
        public bool IsBasic { get; set; }
        public IEnumerable<FieldDefinitionDto> Fields { get; set; }
        public int? SystemId { get; set; }
    }
}
