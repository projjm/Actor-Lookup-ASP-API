using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ActorLookupREST.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ActorLookupREST.Controllers
{
    [Route("api/TitleSearch")]
    [ApiController]
    public class TitleSearchController : ControllerBase
    {
        private ElasticClient _elastic;
        public TitleSearchController(ElasticClient elasticClient)
        {
            _elastic = elasticClient;
        }

        // GET: api/TitleSearch/searchTerm
        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<TitleSearchResponse[]>> GetTitleSearchResult(string searchTerm)
        {
            string exact = $"\"{searchTerm}\"";
            var response = await _elastic.SearchAsync<TitleSearchFull>(s =>
                    s.Query(q => q
                        .Bool(b => b
                            .Should(
                                s => s.MatchPhrase(mp => mp.Field(f => f.PrimaryTitle).Query(searchTerm)),
                                s => s.FunctionScore(fs => fs
                                    .Query(q => q.QueryString(qs => qs.Query($"*{searchTerm}*").Fields(f => f.Field(fe => fe.PrimaryTitle)))).Boost(1.5)
                                    .Functions(fu => fu
                                        .FieldValueFactor(fvf => fvf
                                            .Field(f => f.Relevancy)
                                            .Factor(0.5)
                                            .Modifier(FieldValueFactorModifier.Log1P)
                                        )
                                    )))))
                    .Size(15));

            IEnumerable<TitleSearchResponse> results = response.Documents.Select(doc => new TitleSearchResponse()
            {
                PrimaryTitle = doc.PrimaryTitle,
                TitleType = NormalizeTitleType(doc.TitleType),
                OriginalTitle = doc.OriginalTitle,
                TConst = doc.TConst,
                StartYear = NormalizeYear(doc.StartYear),
                EndYear = NormalizeYear(doc.EndYear)
            });

            return results.ToArray();
        }

        private string NormalizeTitleType(string titleType)
        {
            switch(titleType)
            {
                case "tvSeries":
                    return "Series";
                case "movie":
                    return "Movie";
                case "short":
                    return "Short";
                case "tvMovie":
                    return "TV Movie";
                case "videoGame":
                    return "Video Game";
                case "tvSpecial":
                    return "TV Special";
                case "tvMiniSeries":
                    return "Mini-Series";
            }

            return titleType;
        }

        private string NormalizeYear(string year)
        {
            if (year == "\\N")
                return "";
            else
                return year;
        }
    }
}
