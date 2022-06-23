namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IGemStatBusiness : IBaseBusiness<GemStat>
    {
        Task CreateList(int gemId, int rarityId, List<GemStat> gemStats);

        Task<Dictionary<int, List<GemStat>>> DictionaryGemStatsByListGemIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs);

        Task<List<GemStat>> ListByGemIdAndRarityId(int gemId, int rarityId);

        Task UpdateList(int gemId, int rarityId, List<GemStat> gemStats);

        Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<GemStat> gemStats);
    }
}