namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class RaritySeed : IContextSeed
    {
        public RaritySeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Rarities.Any() && !isProduction)
            {
                List<RarityEntity> rarityList = new List<RarityEntity>
                {
                    new RarityEntity
                    {
                        RarityType = RarityType.Common,
                        LootValue = 1,
                    },
                    new RarityEntity
                    {
                        RarityType = RarityType.Rare,
                        LootValue = 2,
                    },
                    new RarityEntity
                    {
                        RarityType = RarityType.Epic,
                        LootValue = 3,
                    },
                   new RarityEntity
                    {
                        RarityType = RarityType.Legendary,
                        LootValue = 4,
                    },
                };

                await this.Context.Rarities.AddRangeAsync(rarityList);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}