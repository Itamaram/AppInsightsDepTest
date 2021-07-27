using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AppInsightsDepTest.NotWorking
{
    public class Function1
    {
        [FunctionName(nameof(StartWithEF))]
        public async Task<IActionResult> StartWithEF(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [ServiceBus("%QueueName%")] IAsyncCollector<Foo> collector, ILogger logger)
        {
            logger.LogInformation("Let's get started");
            await collector.AddAsync(new Foo());
            return new OkResult();
        }

        [FunctionName(nameof(FinishWithEF))]
        public void FinishWithEF([ServiceBusTrigger("%QueueName%")] Foo foo, ILogger logger)
        {
            logger.LogInformation("Done");
        }
    }

    public class Foo { }
}
