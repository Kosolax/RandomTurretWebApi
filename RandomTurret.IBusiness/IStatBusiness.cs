namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities.Enum;

    public interface IStatBusiness : IBaseBusiness<Stat>
    {
        Task<KeyValuePair<bool, Stat>> CreateOrUpdate(Stat itemToCreateOrUpdate);

        Task<Stat> Delete(StatType statType);

        Task<bool> DoStatsExist(List<int> idStats);

        Task<Stat> Get(StatType statType);

        Task<List<Stat>> List();
    }
}