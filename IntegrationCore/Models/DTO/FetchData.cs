using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Models.DTO
{
    public class FetchData
    {
        public string TypeId { get; set; }
        public TypeDefinition Type { get; set; }
        public string Path { get; set; }
        public string PathEndingPath { get; set; }
        public ActionType Action { get; set; }
        public List<FetchSimpleField> QueryParams { get; set; }
        public List<FetchSimpleField> HeaderParams { get; set; }
    }
    public class FetchSimpleField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public enum ActionType
    {
        Get,
        GetByIdentifier,
        Create,
        Remove,
        Update,
    }
}
