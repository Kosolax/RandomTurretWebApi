namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class TowerStatDataAccess : BaseDataAccess<TowerStatEntity>, ITowerStatDataAccess
    {
        public TowerStatDataAccess(RandomTurretContext context)
            : base(context)
        {
        }

        public async Task CreateList(List<TowerStatEntity> towerStatEntities)
        {
            await this.Context.TowerStats.AddRangeAsync(towerStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteList(List<TowerStatEntity> towerStatEntities)
        {
            this.Context.TowerStats.RemoveRange(towerStatEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<TowerStatEntity>> ListByListTowerId(List<int> listTowerId)
        {
            return await this.Context.TowerStats.Where(x => listTowerId.Contains(x.TowerEntityId)).ToListAsync();
        }

        public async Task<List<TowerStatEntity>> ListByTowerId(int towerId)
        {
            return await this.Context.TowerStats.Where(x => x.TowerEntityId == towerId).ToListAsync();
        }

        public async Task<List<TowerStatEntity>> UpdateRangeAsync(List<TowerStatEntity> itemsToUpdate)
        {
            foreach (TowerStatEntity itemToUpdate in itemsToUpdate)
            {
                TowerStatEntity item = await this.Context.Set<TowerStatEntity>().FindAsync(itemToUpdate.Id);
                this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            }

            await this.Context.SaveChangesAsync();
            return itemsToUpdate;
        }
    }
}