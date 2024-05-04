using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GalbenosFuncs
{
    public class GiveMeToken
    {
        private readonly ILogger<GiveMeToken> _logger;

        public GiveMeToken(ILogger<GiveMeToken> logger)
        {
            _logger = logger;
        }

        [Function("GiveMeToken")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            List<string> items = new();

            Dictionary<string, string> parametros = new Dictionary<string, string>();
            foreach (var queryParam in req.Query)
            {
                parametros.Add(queryParam.Key, queryParam.Value);
            }

            foreach (var parametro in parametros)
            {
                _logger.LogInformation($"{parametro.Key}: {parametro.Value}");
                items.Add($"{parametro.Key}: {parametro.Value}");
            }

            return new OkObjectResult(items);
        }
    }
}
