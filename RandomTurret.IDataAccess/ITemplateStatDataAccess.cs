namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface ITemplateStatDataAccess : IBaseDataAccess<TemplateStatEntity>
    {
        Task CreateList(List<TemplateStatEntity> templateStatEntities);

        Task DeleteList(List<TemplateStatEntity> templateStatEntities);

        Task<List<TemplateStatEntity>> ListByListTemplateIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs);

        Task<List<TemplateStatEntity>> ListByTemplateIdAndRarityId(int templateId, int rarityId);

        Task<List<TemplateStatEntity>> UpdateRangeAsync(List<TemplateStatEntity> itemsToUpdate);
    }
}