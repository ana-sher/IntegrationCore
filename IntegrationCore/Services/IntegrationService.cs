using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IntegrationCore.Data;
using IntegrationCore.Models.DB;
using IntegrationCore.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace IntegrationCore.Services
{
    public class IntegrationService
    {
        private readonly IntegratorContext _context;
        private readonly HttpClient _mappingClient;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public IntegrationService(IntegratorContext context, IMapper mapper,
            IHttpClientFactory clientFactory)
        {
            _context = context;
            _mapper = mapper;
            _mappingClient = clientFactory.CreateClient("mapping-service");
        }

        public async Task RunIntegration(int id)
        {
            var integration = await _context.Integration
                .Include("TypeFrom.SystemDefinition.ConnectionFields.ConnectionFieldValues")
                .Include("TypeTo.SystemDefinition.ConnectionFields.ConnectionFieldValues")
                .FirstAsync(el => el.Id == id);

            var fetchReqObj = new FetchRequest()
            {
                Type = integration.TypeFrom.SystemDefinition.TransferType,
                FetchType = new FetchData()
            };

            var filesFrom = new List<string>();

            fetchReqObj.FetchType.Action = ActionType.Get;
            fetchReqObj.FetchType.Path = integration.TypeFrom.SystemDefinition.Url + integration.TypeFrom.UrlEnding + integration.TypeFrom.SystemDefinition.GetUrlEnding;
            fetchReqObj.FetchType.HeaderParams = integration.TypeFrom.SystemDefinition.ConnectionFields
                .Where(el=>el.Role == FieldTransferRole.Header)
                .Select(el=> new FetchSimpleField()
                {
                    Name = el.Name,
                    Value = el.ConnectionFieldValues.First(el=>el.IntegrationId == integration.Id).Value
                }).ToList();
            fetchReqObj.FetchType.QueryParams = integration.TypeFrom.SystemDefinition.ConnectionFields
                .Where(el => el.Role == FieldTransferRole.Query)
                .Select(el => new FetchSimpleField()
                {
                    Name = el.Name,
                    Value = el.ConnectionFieldValues.First(el => el.IntegrationId == integration.Id).Value
                }).ToList();


            var fetchContent = new StringContent(JsonConvert.SerializeObject(fetchReqObj, _jsonSerializerSettings), Encoding.UTF8, "application/json");
            var fetchResult = await _mappingClient.PostAsync("fetch-data", fetchContent);
            filesFrom.Add(await fetchResult.Content.ReadAsStringAsync());

            if (integration.TypeFrom.SystemDefinition.DataType == DataType.Xml)
            {
                for(var i = 0; i< filesFrom.Count; i++)
                {
                    filesFrom[i] = filesFrom[i].Replace("\"", "\'");
                }
            }
            var parsedData = new List<string>();

            foreach (var file in filesFrom)
            {
                var req = new
                {
                    type = ((int)integration.TypeFrom.SystemDefinition.DataType).ToString(),
                    data = file
                };
                var parseContent = new StringContent(JsonConvert.SerializeObject(req, _jsonSerializerSettings), Encoding.UTF8, "application/json");
                var parseResult = await _mappingClient.PostAsync("parse-data", parseContent);
                parsedData.Add(await parseResult.Content.ReadAsStringAsync());
            }

            if (!string.IsNullOrEmpty(integration.TypeFrom.GetFieldWrapper))
            {
                var newParsedData = new List<string>();
                foreach (var item in parsedData)
                {
                    var cont = JObject.Parse(item);
                    if (cont.ContainsKey(integration.TypeFrom.GetFieldWrapper))
                    {
                        var values = cont.GetValue(integration.TypeFrom.GetFieldWrapper).ToObject<IEnumerable<dynamic>>();
                        var arrayValues = values.Select(el=> (string) JsonConvert.SerializeObject(el));

                        newParsedData
                            .AddRange(arrayValues);
                    }
                }

                parsedData = newParsedData;
            }

            var mappedData = new List<string>();
            var types = await _context.TypeDefinition
                .Where(el => el.SystemId == integration.TypeFrom.SystemId ||
                             el.SystemId == integration.TypeTo.SystemId || el.IsBasic)
                .Include(el=>el.Fields).ToListAsync();

            var connections = await _context
                .FieldConnection.Where(el => el.IntegrationId == integration.Id)
                .ToListAsync();

            foreach (var item in parsedData)
            {
                var req = new
                {
                    objFrom = JsonConvert.DeserializeObject(item),
                    types = _mapper.Map<IEnumerable<TypeDefinitionDto>>(types),
                    typeFromId = integration.TypeFromId,
                    typeToId = integration.TypeToId,
                    connections = _mapper.Map<IEnumerable<FlatFieldConnectionDto>>(connections)
                };

                var mapContent = new StringContent(JsonConvert.SerializeObject(req, _jsonSerializerSettings), Encoding.UTF8, "application/json");
                var mapResult = await _mappingClient.PostAsync("map-data", mapContent);
                mappedData.Add(await mapResult.Content.ReadAsStringAsync());
            }

            var writeData = new List<string>();
            foreach (var item in mappedData)
            {
                var req = new
                {
                    data = JsonConvert.DeserializeObject(item),
                    type = integration.TypeTo.SystemDefinition.DataType
                };

                var writeContent = new StringContent(JsonConvert.SerializeObject(req, _jsonSerializerSettings), Encoding.UTF8, "application/json");
                var writeResult = await _mappingClient.PostAsync("write-data", writeContent);
                writeData.Add(await writeResult.Content.ReadAsStringAsync());
            }

            var postReqObj = new FetchRequest()
            {
                Type = integration.TypeTo.SystemDefinition.TransferType,
                FetchType = new FetchData(),
            };

            postReqObj.FetchType.Action = ActionType.Create;
            postReqObj.FetchType.Path = integration.TypeTo.SystemDefinition.Url + integration.TypeTo.UrlEnding + integration.TypeTo.SystemDefinition.PostUrlEnding;
            postReqObj.FetchType.HeaderParams = integration.TypeTo.SystemDefinition.ConnectionFields
                .Where(el => el.Role == FieldTransferRole.Header)
                .Select(el => new FetchSimpleField()
                {
                    Name = el.Name,
                    Value = el.ConnectionFieldValues.FirstOrDefault(el => el.IntegrationId == integration.Id).Value
                })
                .Where(el=>el.Value!= null)
                .ToList();
            postReqObj.FetchType.QueryParams = integration.TypeTo.SystemDefinition.ConnectionFields
                .Where(el => el.Role == FieldTransferRole.Query)
                .Select(el => new FetchSimpleField()
                {
                    Name = el.Name,
                    Value = el.ConnectionFieldValues.FirstOrDefault(el => el.IntegrationId == integration.Id).Value
                })
                .Where(el => el.Value != null)
                .ToList();

            foreach (var item in writeData)
            {
                postReqObj.Data = item;
                var name = $"{integration.Id}-{Guid.NewGuid()}-{DateTime.Now.Ticks}.{integration.TypeTo.SystemDefinition.DataType.ToString().ToLower()}";
                if (postReqObj.Type == TransferType.Ftp)
                {
                    postReqObj.FetchType.QueryParams.Add(new FetchSimpleField()
                    {
                        Name = "name",
                        Value = name
                    });
                }
                
                var postContent = new StringContent(JsonConvert.SerializeObject(postReqObj, _jsonSerializerSettings), Encoding.UTF8, "application/json");
                var postResult = await _mappingClient.PostAsync("fetch-data", postContent);
                var result = await postResult.Content.ReadAsStringAsync();

                if (postReqObj.Type == TransferType.Ftp)
                {
                    postReqObj.FetchType.QueryParams.Remove(postReqObj.FetchType.QueryParams.First(el=>el.Value == name));
                }

                await _context.Transaction.AddAsync(new Transaction()
                {
                    Content = item,
                    GivenName = name,
                    IntegrationId = integration.Id,
                    Timestamp = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
