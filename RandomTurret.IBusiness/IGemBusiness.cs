namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities.Enum;

    public interface IGemBusiness : IBaseBusiness<Gem>
    {
        Task<KeyValuePair<bool, Gem>> CreateOrUpdate(Gem itemToCreateOrUpdate);

        Task<Gem> Delete(GemType gemType, RarityType rarityType);

        Task<Gem> Get(GemType gemType, RarityType rarityType);

        Task<List<Gem>> List();

        Task<List<Gem>> ListFromImpactType(ImpactType impactType);

        Task<List<Gem>> ListFromMergeType(MergeType mergeType);

        Task<List<Gem>> ListFromShootType(ShootType shootType);
    }
}