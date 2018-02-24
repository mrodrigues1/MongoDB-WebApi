using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBGames.Model
{
    public class Game
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public List<string> Platforms { get; set; }
    }
}