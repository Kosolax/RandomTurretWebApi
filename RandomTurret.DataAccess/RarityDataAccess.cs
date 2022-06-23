namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IDataAccess;

    public class RarityDataAccess : BaseDataAccess<RarityEntity>, IRarityDataAccess
    {
        public RarityDataAccess(RandomTurretContext context)
           : base(context)
        {
        }

        public async Task<int> DoRaritiesExist(List<int> idRarities)
        {
            return await this.Context.Rarities.Where(x => idRarities.Contains(x.Id)).CountAsync();
        }

        public async Task<RarityEntity> GetByRarityType(RarityType rarityType)
        {
            return await this.Context.Rarities.Where(x => x.RarityType == rarityType).FirstOrDefaultAsync();
        }
    }
}