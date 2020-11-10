using System.Collections.Generic;

namespace IntegrationCore.Models.DB
{
    public class SystemDefinition
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
        public IEnumerable<ConnectionFieldDefinition> ConnectionFields { get; set; }
        public IEnumerable<TypeDefinition> TypeDefinitions { get; set; }
    }

    public enum TransferType
    {
        Ftp,
        Api,
    }

    public enum DataType
    {
        Json,
        Xml,
    }
}
