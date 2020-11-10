using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Models.DTO
{
    public class FetchRequest
    {
        public FetchData FetchType { get; set; }
        public TransferType Type { get; set; }
        public string Data { get; set; }
    }
}
