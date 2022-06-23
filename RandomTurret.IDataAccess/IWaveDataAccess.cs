namespace RandomTurret.IDataAccess
{
    using System.Threading.Tasks;

    using RandomTurret.Entities;

    public interface IWaveDataAccess : IBaseDataAccess<WaveEntity>
    {
        Task<WaveEntity> GetFromWaveNumber(int waveNumber);
    }
}