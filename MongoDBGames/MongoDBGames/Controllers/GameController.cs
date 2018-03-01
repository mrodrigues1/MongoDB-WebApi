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

        // GET: api/Game/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string name)
        {
            var movie = await _gameRepository.GetGame(name);
            if (movie == null)
                return new NotFoundResult();

            return new ObjectResult(movie);
        }

        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Game movie)
        {
            await _gameRepository.Upsert(movie);
            return new OkObjectResult(movie);
        }

        // PUT: api/Game/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]Game movie)
        {
            var movieId = new ObjectId(id);
            var movieFromDb = _gameRepository.GetGame(movieId);
            if (movieFromDb == null)
                return new NotFoundResult();

            movie.Id = movieId;

            await _gameRepository.Upsert(movie, movieId);

            return new OkObjectResult(movie);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var movieId = new ObjectId(id);
            var movieFromDb = _gameRepository.GetGame(movieId);
            if (movieFromDb == null)
                return new NotFoundResult();

            await _gameRepository.Delete(movieId);

            return new OkResult();
        }
    }
}
