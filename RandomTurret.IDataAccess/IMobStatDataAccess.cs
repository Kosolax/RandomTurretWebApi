namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface IMobStatDataAccess : IBaseDataAccess<MobStatEntity>
    {
        Task CreateList(List<MobStatEntity> mobStatEntities);

        Task DeleteList(List<MobStatEntity> mobStatEntities);

        Task<List<MobStatEntity>> ListByListMobId(List<int> listMobId);

        Task<List<MobStatEntity>> ListByMobId(int mobId);

        Task<List<MobStatEntity>> UpdateRangeAsync(List<MobStatEntity> itemsToUpdate);
    }
}