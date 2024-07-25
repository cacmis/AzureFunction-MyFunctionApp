using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFunctionApp
{
    public static class MyHttpFunction
    {
        [FunctionName("MyHttpFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            log.LogInformation("Processing a new request...");

           

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            int age = data?.age ?? 0;

            log.LogInformation($"Received name: {name} and age: {age}.");
            string responseMessage = $"Hello, {name}. You are {age} years old.";

            return new OkObjectResult(responseMessage);
        }
    }
}
