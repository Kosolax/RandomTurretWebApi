namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IDataAccess;

    public class MobDataAccess : BaseDataAccess<MobEntity>, IMobDataAccess
    {
        public MobDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task<int> DoMobsExist(List<int> listMobId)
        {
            return await this.Context.Mobs.Where(x => listMobId.Contains(x.Id)).CountAsync();
        }

        public async Task<MobEntity> GetByMobType(MobType mobType)
        {
            return await this.Context.Mobs.Where(x => x.MobType == mobType).FirstOrDefaultAsync();
        }
    }
}