using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDBGames.Model;
using MongoDBGames.Repository;

namespace MongoDBGames.Controllers
{
    [Produces("application/json")]
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET: api/Game
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(await _gameRepository.GetAllGames());
        }

        // GET: api/Game/name
        [HttpGet("{name}", Name = "Get")]
        public async Task<IActionResult> Get(string name)
        {
            var game = await _gameRepository.GetGame(name);

            if (game == null)
                return new NotFoundResult();

            return new ObjectResult(game);
        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Game game)
        {
            await _gameRepository.Upsert(game);
            return new OkObjectResult(game);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Game game)
        {
            var gameId = new ObjectId(id);
            var gameFromDb = await _gameRepository.GetGame(gameId);

            if (gameFromDb == null)
                return new NotFoundResult();

            game.Id = gameId;

            await _gameRepository.Upsert(game, gameId);

            return new OkObjectResult(game);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var gameId = new ObjectId(id);
            var gameFromDb = await _gameRepository.GetGame(gameId);

            if (gameFromDb == null)
                return new NotFoundResult();

            await _gameRepository.Delete(gameId);

            return new OkResult();
        }
    }
}
