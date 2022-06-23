namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities.Enum;

    public interface IRarityBusiness : IBaseBusiness<Rarity>
    {
        Task<KeyValuePair<bool, Rarity>> CreateOrUpdate(Rarity itemToCreateOrUpdate);

        Task<Rarity> Delete(RarityType rarityType);

        Task<bool> DoRaritiesExist(List<int> idRarities);

        Task<Rarity> Get(RarityType rarityType);

        Task<List<Rarity>> List();
    }
}