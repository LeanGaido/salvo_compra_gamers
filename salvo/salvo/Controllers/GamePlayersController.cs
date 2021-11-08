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
    public class GamePlayersController : ControllerBase
    {
        private IGamePlayerRepository _repository;

        public GamePlayersController(IGamePlayerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<GamePlayersController>
        [HttpGet("{id}", Name = "GetGameView")]
        public IActionResult GetGameView(long id)
        {
            try
            {
                var gamePlayers = _repository.GetGamePlayerView(id);
                var gameView = new GameViewDTO
                {
                    Id = gamePlayers.Id,
                    CreactionDate = gamePlayers.Game.CreationDate,
                    Ships = gamePlayers.Ships.Select(ship => new ShipDTO
                    {
                        Id = ship.Id,
                        Type = ship.Type,
                        Locations = ship.Locations.Select(location => new ShipLocationDTO
                        {
                            Id = location.Id,
                            Location = location.Location
                        }).ToList()
                    }).ToList(),
                    GamePlayers = gamePlayers.Game.GamePlayers.Select(gamePlayer => new GamePlayerDTO 
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
                };
               
                //_logger.LogInfo($"Returned all owners from database.");
                return Ok(gameView);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong inside GetAllGames action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
