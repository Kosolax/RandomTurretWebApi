namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public class WaveMobSeed : IContextSeed
    {
        public WaveMobSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.WavesMobs.Any() && !isProduction)
            {
                List<WaveMobEntity> waveMobEntities = new List<WaveMobEntity>
                {
                    new WaveMobEntity
                    {
                        Position = 1,
                        MobEntityId = this.Context.Mobs.FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.FirstOrDefault().Id,
                    },
                    new WaveMobEntity
                    {
                        Position = 2,
                        MobEntityId = this.Context.Mobs.FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.FirstOrDefault().Id,
                    },
                    new WaveMobEntity
                    {
                        Position = 3,
                        MobEntityId = this.Context.Mobs.FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.FirstOrDefault().Id,
                    },
                    new WaveMobEntity
                    {
                        Position = 1,
                        MobEntityId = this.Context.Mobs.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                    },
                    new WaveMobEntity
                    {
                        Position = 2,
                        MobEntityId = this.Context.Mobs.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                    },
                    new WaveMobEntity
                    {
                        Position = 3,
                        MobEntityId = this.Context.Mobs.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                        WaveEntityId = this.Context.Waves.OrderByDescending(x=>x.Id).FirstOrDefault().Id,
                    },
                };

                await this.Context.WavesMobs.AddRangeAsync(waveMobEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}