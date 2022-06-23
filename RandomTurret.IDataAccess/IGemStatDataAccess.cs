namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface IGemStatDataAccess : IBaseDataAccess<GemStatEntity>
    {
        Task CreateList(List<GemStatEntity> gemStatEntities);

        Task DeleteList(List<GemStatEntity> gemStatEntities);

        Task<List<GemStatEntity>> ListByGemIdAndRarityId(int gemId, int rarityId);

        Task<List<GemStatEntity>> ListByListGemIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs);

        Task<List<GemStatEntity>> UpdateRangeAsync(List<GemStatEntity> itemsToUpdate);
    }
}