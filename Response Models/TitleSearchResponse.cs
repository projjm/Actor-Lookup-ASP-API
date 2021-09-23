using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorLookupREST.ResponseModels
{
    public class TitleSearchResponse
    {
        public string PrimaryTitle { get; set; }
        public string TitleType { get; set; }
        public string OriginalTitle { get; set; }
        public string TConst { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
    }
}
