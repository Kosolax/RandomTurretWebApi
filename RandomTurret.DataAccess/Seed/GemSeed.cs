namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class GemSeed : IContextSeed
    {
        public GemSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Gems.Any() && !isProduction)
            {
                List<GemEntity> gemEntities = new List<GemEntity>
                {
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.A,
                        ShootType = ShootType.First,
                    },
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.B,
                        ShootType = ShootType.BiggestHp,
                    },
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.C,
                        ImpactType = ImpactType.BreakDef,
                    },
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.D,
                        ImpactType = ImpactType.Slow,
                    },
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.E,
                        MergeType = MergeType.Slow,
                    },
                    new GemEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        GemType = GemType.F,
                        MergeType = MergeType.AttackSpeedPlus,
                    },
                };

                await this.Context.Gems.AddRangeAsync(gemEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}