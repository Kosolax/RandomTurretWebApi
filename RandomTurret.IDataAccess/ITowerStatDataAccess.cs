namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface ITowerStatDataAccess : IBaseDataAccess<TowerStatEntity>
    {
        Task CreateList(List<TowerStatEntity> towerStatEntities);

        Task DeleteList(List<TowerStatEntity> towerStatEntities);

        Task<List<TowerStatEntity>> ListByListTowerId(List<int> listTowerId);

        Task<List<TowerStatEntity>> ListByTowerId(int towerId);

        Task<List<TowerStatEntity>> UpdateRangeAsync(List<TowerStatEntity> itemsToUpdate);
    }
}