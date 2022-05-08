using FunctionApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace FunctionTests
{
    public class FunctionTests
    {
        private readonly ILogger logger = TESTFactory.CreateLogger();
        

        [Fact]
        public async void Http_trigger_should_retun_known_string()
        {
            var request = TESTFactory.CreateHttpRequest("name", "Can");
            var response = (OkObjectResult)await Function1.Run(request, logger);
            Assert.Equal("Hello, Can. This HTTP triggered function executed successfully.", response.Value);
        }

        [Theory]
        [MemberData(nameof(TESTFactory.Data), MemberType = typeof(TESTFactory))]
        public async void Http_trigger_should_return_known_string_from_member_data(string queryStringKey, string queryStringValue)
        {
            var request = TESTFactory.CreateHttpRequest(queryStringKey, queryStringValue);
            var response = (ObjectResult)await Function1.Run(request, logger);
            Assert.Equal($"Hello, {queryStringValue}. This HTTP triggered function executed successfully.", response.Value);
        }

        /*
        [Fact]
        public async void Http_trigger_should_log_message()
        {
            var logger = (ListLogger)TESTFactory.CreateLogger(LoggerTypes.List);
            await Function1.Run(null, logger);

            var msg = logger.Logs[0];
            Assert.Contains("C# HTTP trigger function processed a request.", msg);
        }
        */

    }
}
