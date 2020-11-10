namespace IntegrationCore.Models.DB
{
    public class ConnectionFieldValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ConnectionFieldId { get; set; }
        public ConnectionFieldDefinition ConnectionField { get; set; }
        public int IntegrationId { get; set; }
        public Integration Integration { get; set; }
    }
}
