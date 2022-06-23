namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IGemPlayerBusiness : IBaseBusiness<GemPlayer>
    {
        Task<KeyValuePair<bool, GemPlayer>> Create(GemPlayer itemToCreate);

        Task<GemPlayer> Delete(int id);
    }
}