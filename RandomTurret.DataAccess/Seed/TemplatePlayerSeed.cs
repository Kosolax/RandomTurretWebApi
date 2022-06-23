namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;

    public class TemplatePlayerSeed : IContextSeed
    {
        public TemplatePlayerSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.TemplatePlayers.Any() && !isProduction)
            {
                List<TemplateEntity> templateEntities = await this.Context.Templates.ToListAsync();
                List<TemplatePlayerEntity> templatePlayerEntities = new List<TemplatePlayerEntity>();

                for (int i = 0; i < templateEntities.Count; i++)
                {
                    templatePlayerEntities.Add(
                        new TemplatePlayerEntity()
                        {
                            PlayerId = this.Context.Players.FirstOrDefault().Id,
                            TemplateId = templateEntities[i].Id,
                        }
                    );
                    templatePlayerEntities.Add(
                        new TemplatePlayerEntity()
                        {
                            PlayerId = this.Context.Players.FirstOrDefault().Id,
                            TemplateId = templateEntities[i].Id,
                        }
                    );
                    templatePlayerEntities.Add(
                        new TemplatePlayerEntity()
                        {
                            PlayerId = this.Context.Players.OrderByDescending(x => x.Id).FirstOrDefault().Id,
                            TemplateId = templateEntities[i].Id,
                        }
                    );
                }

                await this.Context.TemplatePlayers.AddRangeAsync(templatePlayerEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}