using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AppInsightsDepTest.Working
{
    public class Function1
    {
        [FunctionName(nameof(Start))]
        public async Task<IActionResult> Start(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus("%queueName%")] IAsyncCollector<Foo> collector, ILogger logger)
        {
            logger.LogInformation("Let's get started");
            await collector.AddAsync(new Foo());
            return new OkResult();
        }

        [FunctionName(nameof(Finish))]
        public void Finish([ServiceBusTrigger("%QueueName%")] Foo foo, ILogger logger)
        {
            logger.LogInformation("Done");
        }
    }

    public class Foo { }
}