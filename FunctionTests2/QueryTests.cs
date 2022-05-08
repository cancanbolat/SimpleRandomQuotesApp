using FunctionApp;
using FunctionApp.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Shared;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FunctionTests2
{
    public class QueryTests
    {
        private readonly ICosmosService cosmosService;
        private readonly QueryFunctions queryFunctions;

        public QueryTests(ICosmosService cosmosService)
        {
            this.cosmosService = cosmosService;
            queryFunctions = new QueryFunctions(cosmosService);
        }

        [Fact]
        public async Task GetAll_should_return_ok_result()
        {
            var result = cosmosService.GetAllItem("SELECT * FROM c");

            var func = queryFunctions.GetAll(null, null);

            Assert.Equal(result.IsCompletedSuccessfully, func.IsCompletedSuccessfully);
        }

    }
}
