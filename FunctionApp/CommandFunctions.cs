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
using Shared;

namespace FunctionApp
{
    public class CommandFunctions
    {
        private readonly ICosmosService cosmosService;

        public CommandFunctions(ICosmosService cosmosService)
        {
            this.cosmosService = cosmosService;
        }

        [FunctionName("Create")]
        public async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "quotes")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Create function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Quotes>(requestBody);

            await cosmosService.AddItemAsync(data);

            return new OkObjectResult(data);
        }

        [FunctionName("Update")]
        public async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "quotes/{id}")] HttpRequest req, ILogger log, string id)
        {
            log.LogInformation("Update function processed a request. Id: {id}", id);

            var quote = cosmosService.GetItemAsync(id);

            if (quote == null)
                return new NotFoundResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Quotes>(requestBody);

            await cosmosService.UpdateItem(id, data);

            return new OkObjectResult(data);
        }

        [FunctionName("Delete")]
        public async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "quotes/{id}")] HttpRequest req, ILogger log, string id)
        {
            log.LogInformation("Delete function processed a request. Id: {id}", id);

            var quote = cosmosService.GetItemAsync(id);

            if (quote == null)
                return new NotFoundResult();

            await cosmosService.DeleteItemAsync(id);

            return new NoContentResult();
        }
    }
}
