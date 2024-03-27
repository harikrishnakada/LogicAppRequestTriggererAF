using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LogicAppRequestTriggererAF.Services;

namespace LogicAppRequestTriggererAF
{
    public class Function1
    {
        private readonly DocumentReviewService _documentReviewService;
        public Function1(DocumentReviewService documentReviewService)
        {
            _documentReviewService = documentReviewService;
        }

        [FunctionName("InitiateReview")]
        public async Task<IActionResult> InitiateReview(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] object body, HttpRequest req, 
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await _documentReviewService.InitiateReviewProcess(body);

            return new OkObjectResult("responseMessage");
        }

        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
