using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api;

public class WelcomeAzureFunctions(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<WelcomeAzureFunctions>();

    class NameObject
    {
        public string? Name { get; set; }
    }

    [Function(nameof(WelcomeAzureFunctions))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] 
        HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        string? name;
        // if there is a name query parameter, get the value
        // and return a greeting message
        if (req.TryGetQueryParameter("name", out name))
            response.WriteString($"Welcome to Azure Functions, {name}!");
        else if ((await req.ReadFromJsonAsync<NameObject>()) is { } nameObject)
            response.WriteString($"Welcome to Azure Functions, {nameObject.Name}!");
        else
            response.WriteString("Welcome to Azure Functions! Welcome!");

        return response;
    }
}
