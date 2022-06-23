namespace RandomTurret.DataAccess.Seed
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TemplateSeed : IContextSeed
    {
        public TemplateSeed(RandomTurretContext context)
        {
            this.Context = context;
        }

        public RandomTurretContext Context { get; set; }

        public async Task Execute(bool isProduction)
        {
            if (!this.Context.Templates.Any() && !isProduction)
            {
                List<TemplateEntity> templateEntities = new List<TemplateEntity>
                {
                    new TemplateEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        TemplateType = TemplateType.A,
                    },
                    new TemplateEntity
                    {
                        RarityId = this.Context.Rarities.FirstOrDefault().Id,
                        TemplateType = TemplateType.B,
                    },
                };

                await this.Context.Templates.AddRangeAsync(templateEntities);
                await this.Context.SaveChangesAsync();
            }
        }
    }
}