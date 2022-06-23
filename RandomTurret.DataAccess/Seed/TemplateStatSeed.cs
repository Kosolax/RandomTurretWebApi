namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TemplateStatSeed : IContextSeed
    {
        public TemplateStatSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.TemplateStats.Any() && !isProduction)
            {
                List<TemplateStatEntity> templateStatEntities = new List<TemplateStatEntity>();
                List<TemplateEntity> templateEntities = await this.Context.Templates.ToListAsync();
                List<StatEntity> stats = await this.Context.Stats.ToListAsync();
                Dictionary<StatType, int> usefullStats = stats.Where(x => x.StatType == StatType.AttackSpeed || x.StatType == StatType.Damage).ToDictionary(x => x.StatType, x => x.Id);

                for (int i = 0; i < templateEntities.Count; i++)
                {
                    TemplateStatEntity attackSpeed = new TemplateStatEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        StatId = usefullStats[StatType.AttackSpeed],
                        TemplateId = templateEntities[i].Id,
                        Value = 1.5f + (i * 0.05f),
                    };

                    TemplateStatEntity damage = new TemplateStatEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        StatId = usefullStats[StatType.Damage],
                        TemplateId = templateEntities[i].Id,
                        Value = 20 + (i * 2),
                    };

                    templateStatEntities.Add(attackSpeed);
                    templateStatEntities.Add(damage);
                }

                await this.Context.TemplateStats.AddRangeAsync(templateStatEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}