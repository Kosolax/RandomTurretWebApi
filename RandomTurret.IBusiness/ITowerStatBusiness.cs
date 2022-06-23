namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface ITowerStatBusiness : IBaseBusiness<TowerStat>
    {
        Task CreateList(int towerId, List<TowerStat> towerStats);

        Task<Dictionary<int, List<TowerStat>>> DictionaryTowersStatsByListTowerId(List<int> idTowers);

        Task<List<TowerStat>> ListByTowerId(int idTower);

        Task UpdateList(int idTower, List<TowerStat> towerStats);

        Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<TowerStat> towerStats);
    }
}