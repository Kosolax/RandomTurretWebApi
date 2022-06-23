namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public interface IGemDataAccess : IBaseDataAccess<GemEntity>
    {
        Task<GemEntity> GetFromGemType(GemType gemType, int rarityId);

        Task<List<GemEntity>> ListFromImpactType(ImpactType impactType);

        Task<List<GemEntity>> ListFromMergeType(MergeType mergeType);

        Task<List<GemEntity>> ListFromShootType(ShootType shootType);
    }
}