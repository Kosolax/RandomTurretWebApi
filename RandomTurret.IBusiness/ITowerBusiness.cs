namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface ITowerBusiness : IBaseBusiness<Tower>
    {
        Task<KeyValuePair<bool, Tower>> CreateOrUpdate(Tower towerToCreateOrUpdate);

        Task<Tower> Delete(int id);

        Task<Tower> Get(int id);

        Task<List<Tower>> ListByPlayerId(int playerId);
    }
}