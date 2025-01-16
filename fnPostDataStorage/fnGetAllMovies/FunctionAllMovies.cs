using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace fnGetAllMovies
{
    public class FunctionAllMovies
    {
        private readonly ILogger<FunctionAllMovies> _logger;
        private readonly CosmosClient _cosmosClient;

        public FunctionAllMovies(ILogger<FunctionAllMovies> logger, CosmosClient cosmosClient)
        {
            _logger = logger;
            _cosmosClient = cosmosClient;
        }

        [Function("all")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Recuperando dados do filme...");

            var container = _cosmosClient.GetContainer("FlixDB", "movies");

            var query = new QueryDefinition("SELECT * FROM c");

            var result = container.GetItemQueryIterator<MovieResponse>(query);
            var results = new List<MovieResponse>();

            while (result.HasMoreResults)
            {
                foreach (var item in await result.ReadNextAsync())
                {
                    results.Add(item);
                }
            }

            var responseMessage = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await responseMessage.WriteAsJsonAsync(results);

            return responseMessage;
        }
    }
}
