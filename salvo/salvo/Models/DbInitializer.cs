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
            // Si hay "Players" es que la base ya fue inicializada.
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
        }
    }
}
