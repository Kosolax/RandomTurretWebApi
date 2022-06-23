namespace RandomTurret.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class TowerDataAccess : BaseDataAccess<TowerEntity>, ITowerDataAccess
    {
        public TowerDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task<List<TowerEntity>> ListByPlayerId(int playerId)
        {
            return await this.Context.Towers.Where(x => x.PlayerEntityId == playerId).ToListAsync();
        }
    }
}