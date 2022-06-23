namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IWaveBusiness : IBaseBusiness<Wave>
    {
        Task<KeyValuePair<bool, Wave>> CreateOrUpdate(Wave waveToCreateOrUpdate);

        Task<Wave> Delete(int waveNumber);

        Task<Wave> GetFromWaveNumber(int waveNumber);

        Task<List<Wave>> List();
    }
}