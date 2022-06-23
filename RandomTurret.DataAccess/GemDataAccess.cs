namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IDataAccess;

    public class GemDataAccess : BaseDataAccess<GemEntity>, IGemDataAccess
    {
        public GemDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task<GemEntity> GetFromGemType(GemType gemType, int rarityId)
        {
            return await this.Context.Gems.Where(x => x.GemType == gemType && x.RarityId == rarityId).FirstOrDefaultAsync();
        }

        public async Task<List<GemEntity>> ListFromImpactType(ImpactType impactType)
        {
            return await this.Context.Gems.Where(x => x.ImpactType == impactType).ToListAsync();
        }

        public async Task<List<GemEntity>> ListFromMergeType(MergeType mergeType)
        {
            return await this.Context.Gems.Where(x => x.MergeType == mergeType).ToListAsync();
        }

        public async Task<List<GemEntity>> ListFromShootType(ShootType shootType)
        {
            return await this.Context.Gems.Where(x => x.ShootType == shootType).ToListAsync();
        }
    }
}