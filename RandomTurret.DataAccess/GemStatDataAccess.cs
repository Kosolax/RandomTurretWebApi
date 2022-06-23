namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class GemStatDataAccess : BaseDataAccess<GemStatEntity>, IGemStatDataAccess
    {
        public GemStatDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task CreateList(List<GemStatEntity> gemStatEntities)
        {
            await this.Context.GemStats.AddRangeAsync(gemStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteList(List<GemStatEntity> gemStatEntities)
        {
            this.Context.GemStats.RemoveRange(gemStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<GemStatEntity>> ListByGemIdAndRarityId(int gemId, int rarityId)
        {
            return await this.Context.GemStats.Where(x => x.GemId == gemId && x.RarityId == rarityId).ToListAsync();
        }

        public async Task<List<GemStatEntity>> ListByListGemIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs)
        {
            List<GemStatEntity> gemStatEntities = new List<GemStatEntity>();
            foreach (KeyValuePair<int, int> keyValuePair in keyValuePairs)
            {
                gemStatEntities.AddRange(this.Context.GemStats.Where(x => x.GemId == keyValuePair.Key && x.RarityId == keyValuePair.Value).ToList());
            }

            await this.Context.SaveChangesAsync();
            return gemStatEntities;
        }

        public async Task<List<GemStatEntity>> UpdateRangeAsync(List<GemStatEntity> itemsToUpdate)
        {
            foreach (GemStatEntity itemToUpdate in itemsToUpdate)
            {
                GemStatEntity item = await this.Context.Set<GemStatEntity>().FindAsync(itemToUpdate.Id);
                this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            }

            await this.Context.SaveChangesAsync();
            return itemsToUpdate;
        }
    }
}