using Microsoft.Azure.Cosmos;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp.Services
{
    public class CosmosService : ICosmosService
    {
        private Container container;
        public CosmosService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(Quotes item)
        {
            await container.CreateItemAsync<Quotes>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteItemAsync(string id)
        {
            await container.DeleteItemAsync<Quotes>(id, new PartitionKey(id));
        }

        public async Task<IEnumerable<Quotes>> GetAllItem(string queryString)
        {
            var query = container.GetItemQueryIterator<Quotes>(new QueryDefinition(queryString));
            List<Quotes> results = new List<Quotes>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Quotes> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Quotes> response = await container.ReadItemAsync<Quotes>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateItem(string id, Quotes item)
        {
            await container.UpsertItemAsync<Quotes>(item, new PartitionKey(id));
        }
    }
}
