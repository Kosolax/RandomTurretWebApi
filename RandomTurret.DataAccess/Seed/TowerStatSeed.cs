namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TowerStatSeed : IContextSeed
    {
        public TowerStatSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.TowerStats.Any() && !isProduction)
            {
                List<TowerStatEntity> towerStatEntities = new List<TowerStatEntity>();
                List<TowerEntity> towerEntities = await this.Context.Towers.ToListAsync();
                List<StatEntity> stats = await this.Context.Stats.ToListAsync();
                Dictionary<StatType, int> usefullStats = stats.Where(x => x.StatType == StatType.AttackSpeed || x.StatType == StatType.Damage).ToDictionary(x => x.StatType, x => x.Id);

                for (int i = 0; i < towerEntities.Count; i++)
                {
                    TowerStatEntity attackSpeed = new TowerStatEntity
                    {
                        StatEntityId = usefullStats[StatType.AttackSpeed],
                        TowerEntityId = towerEntities[i].Id,
                        Value = 1.5f + (i * 0.05f),
                    };

                    TowerStatEntity damage = new TowerStatEntity
                    {
                        StatEntityId = usefullStats[StatType.Damage],
                        TowerEntityId = towerEntities[i].Id,
                        Value = 20 + (i * 2),
                    };

                    towerStatEntities.Add(attackSpeed);
                    towerStatEntities.Add(damage);
                }

                await this.Context.TowerStats.AddRangeAsync(towerStatEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}