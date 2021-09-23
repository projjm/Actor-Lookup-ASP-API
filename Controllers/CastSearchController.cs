using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ActorLookupREST.Response_Models;
using ActorLookupREST.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nest;

namespace ActorLookupREST.Controllers
{
    [Route("api/CastSearch")]
    [ApiController]
    public class CastSearchController : ControllerBase
    {
        private IHttpClientFactory _clientFactory;
        private readonly string ApiKey;

        public CastSearchController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            ApiKey = config["APIKey"];
        }

        
        // GET: api/CastSearch
        [HttpGet("{titleId}")]
        public async Task<ActionResult<CastSearchResponse[]>> GetCastSearchResult(string titleId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://imdb-api.com/en/API/FullCast/{ApiKey}/{titleId}");
            AllCastResponse data = JsonSerializer.Deserialize<AllCastResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var result = data.actors.Select(a => new CastSearchResponse(GetCharacterName(a.asCharacter, a.name), a));
            return result.ToArray();
        }

        private string GetCharacterName(string asCharacters, string actorName)
        {
            // Replace with regex solution, current solution is clunky.
            int seperator = asCharacters.IndexOf('/');
            string character = asCharacters;

            if (seperator != -1)
            {
                character = asCharacters.Substring(0, seperator);
            }

            int episodesIndex = character.IndexOf("episode");
            if (episodesIndex != -1)
            {
                int currentIndex = episodesIndex - 1;
                int targetCutoff = episodesIndex - 1;

                while (currentIndex >= 0)
                {
                    char currentChar = character[currentIndex];
                    if (Char.IsLetter(currentChar))
                    {
                        targetCutoff = currentIndex + 1;
                        break;
                    }
                    else
                    {
                        currentIndex--;
                    }
                }
                character = character.Substring(0, targetCutoff);
            }

            character = character.Replace("\n", String.Empty);
            character = character.Replace("(voice)", String.Empty);
            character = character.Replace("(uncredited)", String.Empty);
            character = character.Trim();

            if (character.ToLower() == "self")
                return actorName + " (Self)";

            return character;
        }

    }
}
