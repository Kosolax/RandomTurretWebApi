namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IPlayerBusiness : IBaseBusiness<Player>
    {
        Task<Player> Connection(Player player);

        Task<KeyValuePair<bool, Player>> CreateOrUpdate(Player playerToCreateOrUpdate);

        Task<Player> Delete(int id);

        Task<Player> Get(int id);

        Task<KeyValuePair<bool, Player>> UpdateElo(Player player);
    }
}