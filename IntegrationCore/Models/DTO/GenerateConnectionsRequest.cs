using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationCore.Models.DTO
{
    public class GenerateConnectionsRequest
    {
        public int TypeFromId { get; set; }
        public int TypeToId { get; set; }
        public string ObjFrom { get; set; }
        public string ObjTo { get; set; }
    }
}
