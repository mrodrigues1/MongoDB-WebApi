using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDBGames.Model
{
    public class GameContext : IGameContext
    {
        private readonly IMongoDatabase _db;

        public GameContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Game> Games => _db.GetCollection<Game>("Games");
    }
}