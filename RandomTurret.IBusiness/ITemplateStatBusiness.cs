namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface ITemplateStatBusiness : IBaseBusiness<TemplateStat>
    {
        Task CreateList(int templateId, int rarityId, List<TemplateStat> templateStats);

        Task<Dictionary<int, List<TemplateStat>>> DictionaryTemplateStatsByListTemplateIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs);

        Task<List<TemplateStat>> ListByTemplateIdAndRarityId(int templateId, int rarityId);

        Task UpdateList(int templateId, int rarityId, List<TemplateStat> templateStats);

        Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<TemplateStat> templateStats);
    }
}