namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ContextInitializer
    {
        public async Task Seed(RandomTurretContext context, bool isProduction)
        {
            List<IContextSeed> listSeed = new List<IContextSeed>
            {
                new PlayerSeed(context),
                new StatSeed(context),
                new TowerSeed(context),
                new TowerStatSeed(context),
                new MobSeed(context),
                new MobStatSeed(context),
                new WaveSeed(context),
                new WaveMobSeed(context),
                new RaritySeed(context),
                new TemplateSeed(context),
                new TemplateStatSeed(context),
                new GemSeed(context),
                new GemStatSeed(context),
                new TemplatePlayerSeed(context),
                new GemPlayerSeed(context),
            };

            foreach (IContextSeed contextSeed in listSeed)
            {
                await contextSeed.Execute(isProduction);
            }
        }
    }
}