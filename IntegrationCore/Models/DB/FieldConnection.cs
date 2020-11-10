namespace IntegrationCore.Models.DB
{
    public class FieldConnection
    {
        public int Id { get; set; }
        public int IntegrationId { get; set; }
        public int FirstFieldId { get; set; }
        public int SecondFieldId { get; set; }
        public FieldDefinition FirstField { get; set; }
        public FieldDefinition SecondField { get; set; }
        public string FirstFieldFilterFunction { get; set; }
        public string SecondFieldFilterFunction { get; set; }

        public Integration Integration { get; set; }
    }
}
