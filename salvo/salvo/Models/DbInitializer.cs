using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvo.Models
{
    public static class DbInitializer
    {
        public static void Initialize(SalvoContext context)
        {
            // Si no hay "Players" inserto nuevos registros para la tabla "Players"
            if (!context.Players.Any())
            {
                /*
                Jack Bauer j.bauer@ctu.gov 24
                Chloe O'Brian c.obrian@ctu.gov 42
                Kim Bauer kim_bauer@gmail.com kb
                Tony Almeida t.almeida@ctu.gov mole
                 */

                var players = new Player[]
                {
                    new Player{Name="Jack Bauer", Email="j.bauer@ctu.gov", Password="24"},
                    new Player{Name="Chloe O'Brian", Email="c.obrian@ctu.gov", Password="42"},
                    new Player{Name="Kim Bauer", Email="kim_bauer@gmail.com", Password="kb"},
                    new Player{Name="Tony Almeida", Email="t.almeida@ctu.gov", Password="mole"}
                };

                foreach (Player player in players)
                {
                    context.Players.Add(player);
                }
                context.SaveChanges();
            }

            if (!context.Games.Any())
            {
                /*
                1 j.bauer c.obrian Player 1
                2 j.bauer c.obrian Tie
                3 c.obrian t.almeida Player 1
                4 c.obrian j.bauer Tie
                5 t.almeida j.bauer N/A
                6 kim_bauer N/A N/A
                7 t.almeida N/A N/A
                8 kim_bauer t.almeida N/A
                 */
                DateTime now = DateTime.Now;

                var games = new Game[]
                {
                    new Game{CreationDate = now},
                    new Game{CreationDate = now.AddHours(1)},
                    new Game{CreationDate = now.AddHours(2)},
                    new Game{CreationDate = now.AddHours(3)},
                    new Game{CreationDate = now.AddHours(4)},
                    new Game{CreationDate = now.AddHours(5)},
                    new Game{CreationDate = now.AddHours(6)},
                    new Game{CreationDate = now.AddHours(7)}
                };

                foreach (Game game in games)
                {
                    context.Games.Add(game);
                }
                context.SaveChanges();
            }

            if (!context.GamePlayers.Any())
            {
                Game game1 = context.Games.Find(1L);
                Game game2 = context.Games.Find(2L);
                Game game3 = context.Games.Find(3L);
                Game game4 = context.Games.Find(4L);
                Game game5 = context.Games.Find(5L);
                Game game6 = context.Games.Find(6L);
                Game game7 = context.Games.Find(7L);
                Game game8 = context.Games.Find(8L);

                Player jbauer = context.Players.Find(1L);
                Player obrian = context.Players.Find(2L);
                Player kbauer = context.Players.Find(3L);
                Player almeida = context.Players.Find(4L);

                var gamesPlayers = new GamePlayer[]
                {
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game1,
                        Player = jbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game1,
                        Player = obrian
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game2,
                        Player = jbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game2,
                        Player = obrian
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game3,
                        Player = obrian
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game3,
                        Player = almeida
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game4,
                        Player = obrian
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game4,
                        Player = jbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game5,
                        Player = jbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game5,
                        Player = almeida
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game6,
                        Player = kbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game7,
                        Player = almeida
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game8,
                        Player = kbauer
                    },
                    new GamePlayer {
                        JoinDate=DateTime.Now,
                        Game = game8,
                        Player = almeida
                    }
                    //new GamePlayer {
                    //    JoinDate=DateTime.Now,
                    //    GameId = 1,
                    //    PlayerId = 1
                    //},
                    //new GamePlayer {
                    //    JoinDate=DateTime.Now,
                    //    GameId = 1,
                    //    PlayerId = 2
                    //}
                };

                foreach (GamePlayer gp in gamesPlayers)
                {
                    context.GamePlayers.Add(gp);
                }
                context.SaveChanges();
            }
        }
    }
}
