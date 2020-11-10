using AutoMapper;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;
using IntegrationCore.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationCore.Services
{
    public class GeneratorService
    {
        private readonly IntegratorContext _context;
        private readonly HttpClient _mappingClient;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public GeneratorService(IntegratorContext context, IMapper mapper,
            IHttpClientFactory clientFactory)
        {
            _context = context;
            _mapper = mapper;
            _mappingClient = clientFactory.CreateClient("mapping-service");
        }

        public async Task<IEnumerable<TypeDefinitionDto>> GenerateTypes(int systemId, string data, string name, bool addDefaultValues)
        {
            var system = await _context.SystemDefinition.FindAsync(systemId);
            var types = await _context.TypeDefinition
                .Where(el => el.IsBasic)
                .Include(el => el.Fields).ToListAsync();

            var genReq = new
            {
                data = await TransformObject(data, system.DataType),
                name,
                types = _mapper.Map<IEnumerable<TypeDefinitionDto>>(types),
                addDefaultValues
            };
            var generateContent = new StringContent(JsonConvert.SerializeObject(genReq, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var generateResult = await _mappingClient.PostAsync("type-generator", generateContent);

            return JsonConvert.DeserializeObject<IEnumerable<TypeDefinitionDto>>(
                await generateResult.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<FieldConnection>> GenerateConnections(int typeFromId, int typeToId, string objFrom, string objTo)
        {
            var typeFrom = await _context.TypeDefinition
                .Include(el=>el.SystemDefinition)
                .FirstOrDefaultAsync(el=>el.Id == typeFromId);

            var typeTo = await _context.TypeDefinition
                .Include(el => el.SystemDefinition)
                .FirstOrDefaultAsync(el => el.Id == typeToId);

            var types = await _context.TypeDefinition
                .Where(el => el.SystemId == typeFrom.SystemId ||
                             el.SystemId == typeTo.SystemId || el.IsBasic)
                .Include(el => el.Fields).ToListAsync();

            var genReq = new
            {
                objFrom = await TransformObject(objFrom, typeFrom.SystemDefinition.DataType),
                objTo = await TransformObject(objTo, typeTo.SystemDefinition.DataType),
                types = _mapper.Map<IEnumerable<TypeDefinitionDto>>(types),
                typeFromId,
                typeToId
            };
            var generateContent = new StringContent(JsonConvert.SerializeObject(genReq, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var generateResult = await _mappingClient.PostAsync("connections-generator", generateContent);

            return JsonConvert.DeserializeObject<ConnectionGenerationResult>(
                await generateResult.Content.ReadAsStringAsync()).Connections;
        }

        private async Task<object> TransformObject(string obj, DataType dataType)
        {
            if (dataType == DataType.Xml)
            {
                obj = obj.Replace("\"", "\'");
            }
            var req = new
            {
                type = ((int)dataType).ToString(),
                data = obj
            };
            var parseContent = new StringContent(JsonConvert.SerializeObject(req, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var parseResult = await _mappingClient.PostAsync("parse-data", parseContent);

            return JsonConvert.DeserializeObject(await parseResult.Content.ReadAsStringAsync());
        }
    }
}
