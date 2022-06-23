namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class WaveMobDataAccess : BaseDataAccess<WaveMobEntity>, IWaveMobDataAccess
    {
        public WaveMobDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task CreateList(List<WaveMobEntity> waveMobEntities)
        {
            await this.Context.WavesMobs.AddRangeAsync(waveMobEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteList(List<WaveMobEntity> waveMobEntities)
        {
            this.Context.WavesMobs.RemoveRange(waveMobEntities);
            await this.Context.SaveChangesAsync();
        }

        public async Task<List<WaveMobEntity>> ListByListWaveId(List<int> listWaveId)
        {
            return await this.Context.WavesMobs.Where(x => listWaveId.Contains(x.WaveEntityId)).ToListAsync();
        }

        public async Task<List<WaveMobEntity>> ListByWaveId(int waveId)
        {
            return await this.Context.WavesMobs.Where(x => x.WaveEntityId == waveId).ToListAsync();
        }

        public async Task<List<WaveMobEntity>> UpdateRangeAsync(List<WaveMobEntity> itemsToUpdate)
        {
            foreach (WaveMobEntity itemToUpdate in itemsToUpdate)
            {
                WaveMobEntity item = await this.Context.Set<WaveMobEntity>().FindAsync(itemToUpdate.Id);
                this.Context.Entry(item).CurrentValues.SetValues(itemToUpdate);
            }

            await this.Context.SaveChangesAsync();
            return itemsToUpdate;
        }
    }
}