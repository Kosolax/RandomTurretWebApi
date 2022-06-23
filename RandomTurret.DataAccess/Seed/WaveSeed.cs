namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public class WaveSeed : IContextSeed
    {
        public WaveSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Waves.Any() && !isProduction)
            {
                List<WaveEntity> waveEntities = new List<WaveEntity>
                {
                    new WaveEntity
                    {
                        WaveNumber = 1,
                        DifficultyMultiplier = 1,
                    },
                    new WaveEntity
                    {
                        WaveNumber = 2,
                        DifficultyMultiplier = 50,
                    },
                };

                await this.Context.Waves.AddRangeAsync(waveEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}