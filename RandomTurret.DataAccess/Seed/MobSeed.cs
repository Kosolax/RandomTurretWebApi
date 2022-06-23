namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class MobSeed : IContextSeed
    {
        public MobSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Mobs.Any() && !isProduction)
            {
                List<MobEntity> listMob = new List<MobEntity>
                {
                    new MobEntity
                    {
                        MobType = MobType.Dragon,
                    },
                    new MobEntity
                    {
                        MobType = MobType.Knight,
                    },
                };

                await this.Context.Mobs.AddRangeAsync(listMob);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}