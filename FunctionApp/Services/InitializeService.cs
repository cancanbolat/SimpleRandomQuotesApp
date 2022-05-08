using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace FunctionApp.Services
{
    public static class InitializeService
    {
        //public static IConfigurationSection configurationSection { get; set; }

        public static async Task<CosmosService> InitializeCosmosClientInstanceAsync()
        {
            string databaseName = "my-learn-database";
            string containerName = "my-learn-container";
            string account = "https://learnazurecosmos.documents.azure.com:443/";
            string key = "mlGzOrTdADcM91BuL6QL5aNwvtKIS61b3GqZCptcFxCXpxUMW4d6KlM0WOfg5IfEP6KQ1MdRiGJ3wgHJvVMHCA==";

            CosmosClient client = new CosmosClient(account, key);
            CosmosService cosmosService = new CosmosService(client, databaseName, containerName);

            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosService;
        }
    }
}
