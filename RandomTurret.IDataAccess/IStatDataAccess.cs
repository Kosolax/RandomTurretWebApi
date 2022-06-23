namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public interface IStatDataAccess : IBaseDataAccess<StatEntity>
    {
        Task<int> DoStatsExist(List<int> idStats);

        Task<List<StatEntity>> FindAllStatEntityByListId(List<int> listStatEntityId);

        Task<StatEntity> GetByStatType(StatType statType);
    }
}