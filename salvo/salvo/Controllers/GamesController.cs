using Microsoft.AspNetCore.Authorization;
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
    [Authorize("PlayerOnly")]
    public class GamesController : ControllerBase
    {
        private IGameRepository _repository;

        public GamesController(IGameRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<GamesController>
        [HttpGet(Name = "GetAllGames")]
        [AllowAnonymous]
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
                                },
                                Point = gamePlayer.GetScore() != null ? gamePlayer.GetScore().Point : null
                            }
                        ).ToList()
                    }
                ).ToList();

                GameListDTO gameList = new GameListDTO
                {
                    Email = User.FindFirst("Player") != null ? User.FindFirst("Player").Value : "Guest",
                    Games = gamesDto
                };

                //_logger.LogInfo($"Returned all owners from database.");
                return Ok(gameList);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllGames action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
