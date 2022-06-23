namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IDataAccess;

    public class StatDataAccess : BaseDataAccess<StatEntity>, IStatDataAccess
    {
        public StatDataAccess(RandomTurretContext context)
            : base(context)
        {
        }

        public async Task<int> DoStatsExist(List<int> idStats)
        {
            return await this.Context.Stats.Where(x => idStats.Contains(x.Id)).CountAsync();
        }

        public async Task<List<StatEntity>> FindAllStatEntityByListId(List<int> listStatEntityId)
        {
            return await this.Context.Stats.Where(statEntity => listStatEntityId.Contains(statEntity.Id)).ToListAsync();
        }

        public async Task<StatEntity> GetByStatType(StatType statType)
        {
            return await this.Context.Stats.Where(x => x.StatType == statType).FirstOrDefaultAsync();
        }
    }
}