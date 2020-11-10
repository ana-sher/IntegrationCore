using System.Collections.Generic;

namespace IntegrationCore.Models.DB
{
    public class ConnectionFieldDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldTransferRole Role { get; set; }
        public int SystemId { get; set; }
        public SystemDefinition SystemDefinition { get; set; }
        public IEnumerable<ConnectionFieldValue> ConnectionFieldValues { get; set; }
    }

    public enum FieldTransferRole
    {
        Header,
        JsonBody,
        Query
    }
}
