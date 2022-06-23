namespace RandomTurret.IBusiness
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;

    public interface IWaveMobBusiness : IBaseBusiness<WaveMob>
    {
        Task CreateList(int waveId, List<WaveMob> wavesMobs);

        Task<Dictionary<int, List<WaveMob>>> DictionaryWavesMobsByListWaveId(List<int> waveIds);

        Task<List<WaveMob>> ListByWaveId(int idTower);

        Task UpdateList(int waveId, List<WaveMob> wavesMobs);

        Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<WaveMob> wavesMobs);
    }
}