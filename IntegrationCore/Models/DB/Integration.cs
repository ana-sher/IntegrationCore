using System.Collections.Generic;

namespace IntegrationCore.Models.DB
{
    public class Integration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ConnectionFieldValue> ConnectionFieldValues { get; set; }
        public IEnumerable<FieldConnection> FieldConnections { get; set; }
        public int TypeFromId { get; set; }
        public TypeDefinition TypeFrom { get; set; }
        public int TypeToId { get; set; }
        public TypeDefinition TypeTo { get; set; }
    }
}