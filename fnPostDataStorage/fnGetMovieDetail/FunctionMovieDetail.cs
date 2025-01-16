using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace fnGetMovieDetail
{
    public class FunctionMovieDetail
    {
        private readonly ILogger<FunctionMovieDetail> _logger;
        private readonly CosmosClient _cosmosClient;

        public FunctionMovieDetail(ILogger<FunctionMovieDetail> logger, CosmosClient cosmosClient)
        {
            _logger = logger;
            _cosmosClient = cosmosClient;
        }

        [Function("detail")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Recuperando dados do filme...");

            var container = _cosmosClient.GetContainer("FlixDB", "movies");
            var id = req.Query["id"];
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id);
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
            await responseMessage.WriteAsJsonAsync(results.FirstOrDefault());

            return responseMessage;
        }
    }
}
