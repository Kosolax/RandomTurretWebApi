namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class TemplateStatDataAccess : BaseDataAccess<TemplateStatEntity>, ITemplateStatDataAccess
    {
        public TemplateStatDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task CreateList(List<TemplateStatEntity> templateStatEntities)
        {
            await this.Context.TemplateStats.AddRangeAsync(templateStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteList(List<TemplateStatEntity> templateStatEntities)
        {
            this.Context.TemplateStats.RemoveRange(templateStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<TemplateStatEntity>> ListByListTemplateIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs)
        {
            List<TemplateStatEntity> templateStatEntities = new List<TemplateStatEntity>();
            foreach (KeyValuePair<int, int> keyValuePair in keyValuePairs)
            {
                templateStatEntities.AddRange(this.Context.TemplateStats.Where(x => x.TemplateId == keyValuePair.Key && x.RarityId == keyValuePair.Value).ToList());
            }

            await this.Context.SaveChangesAsync();
            return templateStatEntities;
        }

        public async Task<List<TemplateStatEntity>> ListByTemplateIdAndRarityId(int templateId, int rarityId)
        {
            return await this.Context.TemplateStats.Where(x => x.TemplateId == templateId && x.RarityId == rarityId).ToListAsync();
        }

        public async Task<List<TemplateStatEntity>> UpdateRangeAsync(List<TemplateStatEntity> itemsToUpdate)
        {
            foreach (TemplateStatEntity itemToUpdate in itemsToUpdate)
            {
                TemplateStatEntity item = await this.Context.Set<TemplateStatEntity>().FindAsync(itemToUpdate.Id);
                this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            }

            await this.Context.SaveChangesAsync();
            return itemsToUpdate;
        }
    }
}