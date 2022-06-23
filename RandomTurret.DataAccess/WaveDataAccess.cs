namespace RandomTurret.DataAccess
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using RandomTurret.Entities;
    using RandomTurret.IDataAccess;

    public class WaveDataAccess : BaseDataAccess<WaveEntity>, IWaveDataAccess
    {
        public WaveDataAccess(RandomTurretContext context) : base(context)
        {
        }

        public async Task<WaveEntity> GetFromWaveNumber(int waveNumber)
        {
            return await this.Context.Waves.Where(x => x.WaveNumber == waveNumber).FirstOrDefaultAsync();
        }
    }
}