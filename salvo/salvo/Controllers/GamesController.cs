using Microsoft.AspNetCore.Mvc;
using salvo.Models;
using salvo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace salvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGameRepository _repository;

        public GamesController(IGameRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<GamesController>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetAllGames()
        {
            try
            {
                //Antes
                //var games = _repository.GetAllGames();
                //Ahora
                var gamesDto = _repository.GetAllGamesWithPlayers().Select(
                    game => new GameDTO
                    {
                        Id = game.Id,
                        CreationDate = game.CreationDate,
                        GamePlayers = game.GamePlayers.Select(
                                           gamePlayer => new GamePlayerDTO
                                           {
                                               Id = gamePlayer.Id,
                                               JoinDate = gamePlayer.JoinDate,
                                               Player = new PlayerDTO
                                               {
                                                   Id = gamePlayer.Player.Id,
                                                   Name = gamePlayer.Player.Name,
                                                   Email = gamePlayer.Player.Email
                                               }
                                           }).ToList()
                    }).ToList();

                //_logger.LogInfo($"Returned all owners from database.");
                return Ok(gamesDto);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllGames action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/<GamesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GamesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
