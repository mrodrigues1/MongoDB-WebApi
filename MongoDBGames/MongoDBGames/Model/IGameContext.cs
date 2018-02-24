using MongoDB.Driver;

namespace MongoDBGames.Model
{
    public interface IGameContext
    {
        IMongoCollection<Game> Games { get; }
    }
}