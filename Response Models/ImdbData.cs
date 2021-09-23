using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorLookupREST.ResponseModels
{
    public class AllCastResponse
    {
        public string imDbId { get; set; }
        public string title { get; set; }
        public string fullTitle { get; set; }
        public string type { get; set; }
        public string year { get; set; }

        public Directors directors { get; set; }
        public Writers writers { get; set; }
        public Actor[] actors { get; set; }

        public string errorMessage { get; set; }
    }

    public class Directors
    {
        public string job { get; set; }
        public Director[] items { get; set; }
    }

    public class Director
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Writers
    {
        public string job { get; set; }
        public Writer[] items { get; set; }
    }

    public class Writer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class Actor
    {
        public string id { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string asCharacter { get; set; }

    }

    public class Other
    {
        public string job { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class TitleSearchFull
    {
        public string PrimaryTitle { get; set; }
        public string RuntimeMinutes { get; set; }
        public string TitleType { get; set; }
        public string OriginalTitle { get; set; }
        public string Genres { get; set; }
        public string TConst { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string IsAdult { get; set; }
        public int Relevancy { get; set; }
    }

    public class TitleCharacters
    {
        public string tConst { get; set; }
        public List<CharacterActor> characters { get; set; }

        public TitleCharacters(string tconst)
        {
            tConst = tconst;
            characters = new List<CharacterActor>();
        }
    }

    public class CharacterActor
    {
        public string characterName { get; set; }
        public string nConst { get; set; }

        public CharacterActor(string nconst, string charactername)
        {
            nConst = nconst;
            characterName = charactername;
        }
    }

}
