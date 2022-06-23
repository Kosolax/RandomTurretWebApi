namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class GemPlayerSeed : IContextSeed
    {
        public GemPlayerSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.GemPlayers.Any() && !isProduction)
            {
                List<GemEntity> gemEntities = await this.Context.Gems.ToListAsync();
                List<GemPlayerEntity> gemPlayerEntities = new List<GemPlayerEntity>();

                for (int i = 0; i < gemEntities.Count; i++)
                {
                    gemPlayerEntities.Add(
                        new GemPlayerEntity()
                        {
                            PlayerId = this.Context.Players.FirstOrDefault().Id,
                            GemId = gemEntities[i].Id,
                        }
                    );
                    gemPlayerEntities.Add(
                        new GemPlayerEntity()
                        {
                            PlayerId = this.Context.Players.FirstOrDefault().Id,
                            GemId = gemEntities[i].Id,
                        }
                    );
                    gemPlayerEntities.Add(
                        new GemPlayerEntity()
                        {
                            PlayerId = this.Context.Players.OrderByDescending(x => x.Id).FirstOrDefault().Id,
                            GemId = gemEntities[i].Id,
                        }
                    );
                }

                await this.Context.GemPlayers.AddRangeAsync(gemPlayerEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}