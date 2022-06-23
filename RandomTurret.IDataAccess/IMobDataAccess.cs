namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public interface IMobDataAccess : IBaseDataAccess<MobEntity>
    {
        Task<int> DoMobsExist(List<int> listMobId);

        Task<MobEntity> GetByMobType(MobType mobType);
    }
}