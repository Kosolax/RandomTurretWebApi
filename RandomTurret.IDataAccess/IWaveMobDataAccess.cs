namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface IWaveMobDataAccess : IBaseDataAccess<WaveMobEntity>
    {
        Task CreateList(List<WaveMobEntity> waveMobEntities);

        Task DeleteList(List<WaveMobEntity> waveMobEntities);

        Task<List<WaveMobEntity>> ListByListWaveId(List<int> listWaveId);

        Task<List<WaveMobEntity>> ListByWaveId(int towerId);

        Task<List<WaveMobEntity>> UpdateRangeAsync(List<WaveMobEntity> itemsToUpdate);
    }
}