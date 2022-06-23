namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class MobStatSeed : IContextSeed
    {
        public MobStatSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.MobStats.Any() && !isProduction)
            {
                List<MobStatEntity> listMobStat = new List<MobStatEntity>();
                List<MobEntity> listMobs = await this.Context.Mobs.ToListAsync();
                List<StatEntity> allStats = await this.Context.Stats.ToListAsync();
                Dictionary<StatType, int> usefullStats = allStats.Where(x => x.StatType == StatType.MaxHealth || x.StatType == StatType.Resistance || x.StatType == StatType.Speed || x.StatType == StatType.Damage).ToDictionary(x => x.StatType, x => x.Id);

                for (int mobIndex = 0; mobIndex < listMobs.Count(); mobIndex++)
                {
                    MobStatEntity maxHealth = new MobStatEntity
                    {
                        MobEntityId = listMobs[mobIndex].Id,
                        Value = 200 + (mobIndex * 200),
                        StatEntityId = usefullStats[StatType.MaxHealth]
                    };

                    MobStatEntity speed = new MobStatEntity
                    {
                        MobEntityId = listMobs[mobIndex].Id,
                        Value = 40 + (mobIndex * 40),
                        StatEntityId = usefullStats[StatType.Speed]
                    };

                    MobStatEntity damage = new MobStatEntity
                    {
                        MobEntityId = listMobs[mobIndex].Id,
                        Value = 1,
                        StatEntityId = usefullStats[StatType.Damage]
                    };

                    MobStatEntity resistance = new MobStatEntity
                    {
                        MobEntityId = listMobs[mobIndex].Id,
                        Value = 5 + mobIndex,
                        StatEntityId = usefullStats[StatType.Resistance]
                    };

                    listMobStat.Add(maxHealth);
                    listMobStat.Add(speed);
                    listMobStat.Add(damage);
                    listMobStat.Add(resistance);
                }


                await this.Context.MobStats.AddRangeAsync(listMobStat);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}