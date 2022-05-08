using FunctionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.DependencyInjection;

namespace FunctionTests2
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICosmosService>(
                InitializeService.InitializeCosmosClientInstanceAsync().GetAwaiter().GetResult()
             );
        }
    }
}
