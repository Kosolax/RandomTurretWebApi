namespace RandomTurret.IDataAccess
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface ITowerDataAccess : IBaseDataAccess<TowerEntity>
    {
        Task<List<TowerEntity>> ListByPlayerId(int playerId);
    }
}