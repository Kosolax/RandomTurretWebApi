namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class StatSeed : IContextSeed
    {
        public StatSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Stats.Any() && !isProduction)
            {
                List<StatEntity> listStat = new List<StatEntity>
                {
                    new StatEntity
                    {
                        StatType = StatType.MaxHealth,
                    },
                    new StatEntity
                    {
                        StatType = StatType.Speed,
                    },
                    new StatEntity
                    {
                        StatType = StatType.Resistance,
                    },
                    new StatEntity
                    {
                        StatType = StatType.Damage,
                    },
                    new StatEntity
                    {
                        StatType = StatType.AttackSpeed,
                    },
                };

                await this.Context.Stats.AddRangeAsync(listStat);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}