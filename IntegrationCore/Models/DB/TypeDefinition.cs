using System.Collections.Generic;

namespace IntegrationCore.Models.DB
{
    public class TypeDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlEnding { get; set; }
        public string GetByIdFieldWrapper { get; set; }
        public string GetFieldWrapper { get; set; }
        public string PostFieldWrapper { get; set; }
        public string PutFieldWrapper { get; set; }
        public bool IsBasic { get; set; }
        public IEnumerable<FieldDefinition> Fields { get; set; }
        public int? SystemId { get; set; }
        public SystemDefinition SystemDefinition { get; set; }
    }
}
