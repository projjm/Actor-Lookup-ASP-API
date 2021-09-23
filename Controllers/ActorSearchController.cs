using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ActorLookupREST.Response_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ActorLookupREST.Controllers
{
    [Route("api/ActorSearch")]
    [ApiController]
    public class ActorSearchController : ControllerBase
    {
        private IHttpClientFactory _clientFactory;
        private readonly string ApiKey;

        public ActorSearchController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            ApiKey = config["APIKey"];
        }

        // GET: api/TitleSearch/searchTerm
        [HttpGet("{actorId}")]
        public async Task<ActionResult<ActorSearchResponse>> GetActorSearchResult(string actorId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://imdb-api.com/en/API/Name/{ApiKey}/{actorId}");
            ActorSearchResponse data = JsonSerializer.Deserialize<ActorSearchResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return data;
        }

    }
}
