using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorLookupREST.Response_Models
{
    public class ActorSearchResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public string BirthDate { get; set; }
        public string DeathDate { get; set; }
        public string Awards { get; set; }
        public string Height { get; set; }
        public List<KnownFor> KnownFors { get; set; }
        public List<CastMovie> CastMovies { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class CastMovie
    {
        public string Id { get; set; } // Movie Id
        public string Role { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
    }

    public class KnownFor
    {
        public string Id { get; set; } // Movie Id
        public string Title { get; set; }
        public string FullTitle { get; set; }
        public string Year { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
    }
    
}
