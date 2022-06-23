namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IMobStatBusiness : IBaseBusiness<MobStat>
    {
        Task CreateList(int idMob, List<MobStat> mobStats);

        Task<Dictionary<int, List<MobStat>>> DictionaryMobsStatsByListMobId(List<int> idMobs);

        Task<List<MobStat>> ListByMobId(int idMob);

        Task UpdateList(int idMob, List<MobStat> mobStats);

        Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<MobStat> mobStats);
    }
}