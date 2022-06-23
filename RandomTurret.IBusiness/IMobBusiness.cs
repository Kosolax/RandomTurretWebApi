namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities.Enum;

    public interface IMobBusiness : IBaseBusiness<Mob>
    {
        Task<KeyValuePair<bool, Mob>> CreateOrUpdate(Mob mobToCreateOrUpdate);

        Task<Mob> Delete(MobType mobType);

        Task<bool> DoMobsExist(List<int> listMobId);

        Task<Mob> Get(MobType mobType);

        Task<List<Mob>> List();
    }
}