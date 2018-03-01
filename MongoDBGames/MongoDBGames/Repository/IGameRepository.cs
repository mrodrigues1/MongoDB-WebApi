using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDBGames.Model;

namespace MongoDBGames.Repository
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGame(string name);
        Task<Game> GetGame(ObjectId id);
        Task<bool> Upsert(Game game, ObjectId id = new ObjectId());
        Task<bool> Delete(ObjectId id);
    }
}