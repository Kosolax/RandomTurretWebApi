namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class MobStatDataAccess : BaseDataAccess<MobStatEntity>, IMobStatDataAccess
    {
        public MobStatDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task CreateList(List<MobStatEntity> mobStatEntities)
        {
            await this.Context.MobStats.AddRangeAsync(mobStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteList(List<MobStatEntity> mobStatEntities)
        {
            this.Context.MobStats.RemoveRange(mobStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<MobStatEntity>> ListByListMobId(List<int> listMobId)
        {
            return await this.Context.MobStats.Where(x => listMobId.Contains(x.MobEntityId)).ToListAsync();
        }

        public async Task<List<MobStatEntity>> ListByMobId(int mobId)
        {
            return await this.Context.MobStats.Where(x => x.MobEntityId == mobId).ToListAsync();
        }

        public async Task<List<MobStatEntity>> UpdateRangeAsync(List<MobStatEntity> itemsToUpdate)
        {
            foreach (MobStatEntity itemToUpdate in itemsToUpdate)
            {
                MobStatEntity item = await this.Context.Set<MobStatEntity>().FindAsync(itemToUpdate.Id);
                this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            }

            await this.Context.SaveChangesAsync();
            return itemsToUpdate;
        }
    }
}