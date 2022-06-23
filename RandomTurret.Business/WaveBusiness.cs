namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class WaveBusiness : IWaveBusiness
    {
        private readonly IWaveDataAccess dataAccess;

        private readonly IWaveMobBusiness waveMobBusiness;

        public WaveBusiness(IWaveDataAccess dataAccess, IWaveMobBusiness waveMobBusiness)
        {
            this.dataAccess = dataAccess;
            this.waveMobBusiness = waveMobBusiness;
        }

        public async Task<KeyValuePair<bool, Wave>> CreateOrUpdate(Wave waveToCreateOrUpdate)
        {
            if (waveToCreateOrUpdate != null)
            {
                WaveEntity entity = waveToCreateOrUpdate.CreateEntity();
                KeyValuePair<bool, Dictionary<string, string>> validationWaveMobs = await this.waveMobBusiness.ValidateList(waveToCreateOrUpdate.WavesMobs);

                if (validationWaveMobs.Key)
                {
                    if (waveToCreateOrUpdate.ValidationService.Validate(entity))
                    {
                        WaveEntity waveEntity = await this.dataAccess.GetFromWaveNumber(entity.WaveNumber);
                        if (entity.Id == default)
                        {
                            if (waveEntity == null)
                            {
                                entity = await this.dataAccess.Create(entity);

                                if (entity != null)
                                {
                                    await this.waveMobBusiness.CreateList(entity.Id, waveToCreateOrUpdate.WavesMobs);
                                    waveToCreateOrUpdate = await this.GetWaveFromEntity(entity);
                                    return new KeyValuePair<bool, Wave>(true, waveToCreateOrUpdate);
                                }
                            }
                        }
                        else
                        {
                            if (waveEntity == null || waveEntity.Id == entity.Id)
                            {
                                entity = await this.dataAccess.Update(entity, entity.Id);

                                if (entity != null)
                                {
                                    await this.waveMobBusiness.UpdateList(entity.Id, waveToCreateOrUpdate.WavesMobs);
                                    waveToCreateOrUpdate = await this.GetWaveFromEntity(entity);
                                    return new KeyValuePair<bool, Wave>(true, waveToCreateOrUpdate);
                                }
                            }
                        }
                    }
                }
                else
                {
                    waveToCreateOrUpdate.ValidationService.IsValid = false;
                    waveToCreateOrUpdate.ValidationService.ModelState = validationWaveMobs.Value;
                }
            }

            return new KeyValuePair<bool, Wave>(false, waveToCreateOrUpdate);
        }

        public async Task<Wave> Delete(int waveNumber)
        {
            Wave wave = await this.GetFromWaveNumber(waveNumber);

            if (wave != null)
            {
                await this.dataAccess.Delete(wave.Id);
                return wave;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Wave> GetFromWaveNumber(int waveNumber)
        {
            WaveEntity entity = await this.dataAccess.GetFromWaveNumber(waveNumber);

            if (entity != null)
            {
                return await this.GetWaveFromEntity(entity);
            }

            return null;
        }

        public async Task<List<Wave>> List()
        {
            List<WaveEntity> waveMobEntities = await this.dataAccess.List();
            return (await this.GetWavesFromEntity(waveMobEntities)).OrderBy(x => x.WaveNumber).ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.waveMobBusiness?.Dispose();
            }
        }

        private async Task<Wave> GetWaveFromEntity(WaveEntity entity)
        {
            if (entity != null)
            {
                Wave wave = new Wave(entity, false);
                wave.WavesMobs = await this.waveMobBusiness.ListByWaveId(wave.Id);
                return wave;
            }

            return null;
        }

        private async Task<List<Wave>> GetWavesFromEntity(List<WaveEntity> waveEntities)
        {
            List<Wave> waves = new List<Wave>();
            List<int> waveEntitiesId = waveEntities.Select(x => x.Id).ToList();
            Dictionary<int, List<WaveMob>> dictionaryIdWaveListWaveMobs = await this.waveMobBusiness.DictionaryWavesMobsByListWaveId(waveEntitiesId);

            foreach (WaveEntity waveEntity in waveEntities)
            {
                if (dictionaryIdWaveListWaveMobs.ContainsKey(waveEntity.Id) && dictionaryIdWaveListWaveMobs[waveEntity.Id] != null)
                {
                    waves.Add(new Wave(waveEntity, false, dictionaryIdWaveListWaveMobs[waveEntity.Id]));
                }
                else
                {
                    waves.Add(new Wave(waveEntity, false));
                }
            }

            return waves;
        }
    }
}