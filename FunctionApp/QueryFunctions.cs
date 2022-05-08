using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp.Services;
using System.Collections.Generic;

namespace FunctionApp
{
    public class QueryFunctions
    {
        private readonly ICosmosService cosmosService;

        public QueryFunctions(ICosmosService cosmosService)
        {
            this.cosmosService = cosmosService;
        }

        [FunctionName("GetAll")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "quotes")] HttpRequest req, ILogger log)
        {
            log.LogInformation("GetAll function processed a request.");

            var query = await cosmosService.GetAllItem("SELECT * FROM c");

            return new OkObjectResult(query);
        }

        [FunctionName("Get")]
        public async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "quotes/{id}")] HttpRequest req, ILogger log,
            string id)
        {
            log.LogInformation("Get function processed a request. Id: {id}", id);

            var query = await cosmosService.GetItemAsync(id);

            return new OkObjectResult(query);
        }
    }
}
