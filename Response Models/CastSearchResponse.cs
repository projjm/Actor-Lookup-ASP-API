using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActorLookupREST.ResponseModels;

namespace ActorLookupREST.Response_Models
{
    public class CastSearchResponse
    {
        public string CharacterName { get; private set; }
        public Actor Actor { get; private set; }

        public CastSearchResponse(string name, Actor actor)
        {
            CharacterName = name;
            Actor = actor;
        }

    }
}
