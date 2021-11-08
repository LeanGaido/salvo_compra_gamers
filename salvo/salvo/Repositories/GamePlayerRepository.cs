using Microsoft.EntityFrameworkCore;
using salvo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvo.Repositories
{
    public class GamePlayerRepository : RepositoryBase<GamePlayer>, IGamePlayerRepository
    {
        public GamePlayerRepository(SalvoContext repositoryContext) : base(repositoryContext)
        {

        }

        public GamePlayer GetGamePlayerView(long idGamePlayer)
        {
            return FindAll(source => source.Include(gamePlayer => gamePlayer.Ships)
                                               .ThenInclude(ship => ship.Locations)
                                           .Include(gamePlayer => gamePlayer.Game)
                                               .ThenInclude(game => game.GamePlayers)
                                                   .ThenInclude(gp => gp.Player))
                                           .Where(gamePlayer => gamePlayer.Id == idGamePlayer)
                                           .OrderBy(game => game.JoinDate)
                                           .FirstOrDefault();
        }
    }
}
