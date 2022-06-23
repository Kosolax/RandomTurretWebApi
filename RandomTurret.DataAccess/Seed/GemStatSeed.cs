namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class GemStatSeed : IContextSeed
    {
        public GemStatSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.GemStats.Any() && !isProduction)
            {
                List<GemStatEntity> gemStatEntities = new List<GemStatEntity>();
                List<GemEntity> gemEntities = await this.Context.Gems.ToListAsync();
                List<StatEntity> stats = await this.Context.Stats.ToListAsync();
                Dictionary<StatType, int> usefullStats = stats.Where(x => x.StatType == StatType.AttackSpeed || x.StatType == StatType.Damage).ToDictionary(x => x.StatType, x => x.Id);

                for (int i = 0; i < gemEntities.Count; i++)
                {
                    GemStatEntity attackSpeed = new GemStatEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        StatId = usefullStats[StatType.AttackSpeed],
                        GemId = gemEntities[i].Id,
                        Value = 1.5f + (i * 0.05f),
                    };

                    GemStatEntity damage = new GemStatEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        StatId = usefullStats[StatType.Damage],
                        GemId = gemEntities[i].Id,
                        Value = 20 + (i * 2),
                    };

                    gemStatEntities.Add(attackSpeed);
                    gemStatEntities.Add(damage);
                }

                await this.Context.GemStats.AddRangeAsync(gemStatEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}