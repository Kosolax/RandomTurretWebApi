namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public interface IRarityDataAccess : IBaseDataAccess<RarityEntity>
    {
        Task<int> DoRaritiesExist(List<int> idRarities);

        Task<RarityEntity> GetByRarityType(RarityType rarityType);
    }
}