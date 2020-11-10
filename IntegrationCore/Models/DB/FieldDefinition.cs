namespace IntegrationCore.Models.DB
{
    public class FieldDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeOfFieldId { get; set; }
        public bool IsArray { get; set; }
        public bool Required { get; set; }
        public string DefaultValue { get; set; }
        public int TypeId { get; set; }
        public TypeDefinition Type { get; set; }
    }
}
