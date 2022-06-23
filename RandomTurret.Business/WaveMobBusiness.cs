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

    public class WaveMobBusiness : IWaveMobBusiness
    {
        private readonly IWaveMobDataAccess dataAccess;

        private readonly IMobBusiness mobBusiness;

        public WaveMobBusiness(IWaveMobDataAccess dataAccess, IMobBusiness mobBusiness)
        {
            this.dataAccess = dataAccess;
            this.mobBusiness = mobBusiness;
        }

        public async Task CreateList(int waveId, List<WaveMob> waveMobs)
        {
            List<WaveMobEntity> waveMobEntities = new List<WaveMobEntity>();

            foreach (WaveMob waveMob in waveMobs)
            {
                WaveMobEntity entity = waveMob.CreateEntity();
                entity.Id = 0;
                entity.WaveEntityId = waveId;
                waveMobEntities.Add(entity);
            }

            await this.dataAccess.CreateList(waveMobEntities);
        }

        public async Task<Dictionary<int, List<WaveMob>>> DictionaryWavesMobsByListWaveId(List<int> listWaveId)
        {
            List<WaveMobEntity> waveMobEntities = await this.dataAccess.ListByListWaveId(listWaveId);
            List<WaveMob> waveMobs = this.GetWaveMobsFromEntity(waveMobEntities);

            return waveMobs.GroupBy(x => x.WaveId).ToDictionary(x => x.Key, x => x.OrderBy(y => y.Position).ToList());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<WaveMob>> ListByWaveId(int waveId)
        {
            List<WaveMobEntity> waveMobEntities = await this.dataAccess.ListByWaveId(waveId);
            return this.GetWaveMobsFromEntity(waveMobEntities);
        }

        public async Task UpdateList(int waveId, List<WaveMob> waveMobs)
        {
            List<WaveMobEntity> waveMobEntities = await this.dataAccess.ListByWaveId(waveId);
            List<int> listId = waveMobEntities.Select(x => x.Id).ToList();
            List<WaveMobEntity> waveMobToUpdate = new List<WaveMobEntity>();
            List<WaveMobEntity> waveMobToCreate = new List<WaveMobEntity>();

            foreach (WaveMob waveMob in waveMobs)
            {
                waveMob.WaveId = waveId;
                if (waveMob.Id == default)
                {
                    waveMobToCreate.Add(waveMob.CreateEntity());
                }
                else if (listId.Contains(waveMob.Id))
                {
                    waveMobToUpdate.Add(waveMob.CreateEntity());
                    waveMobEntities.Remove(waveMobEntities.Where(x => x.Id == waveMob.Id).FirstOrDefault());
                }
            }

            List<WaveMobEntity> waveMobToDelete = waveMobEntities;

            await this.dataAccess.CreateList(waveMobToCreate);
            await this.dataAccess.UpdateRangeAsync(waveMobToUpdate);
            await this.dataAccess.DeleteList(waveMobToDelete);
        }

        public async Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<WaveMob> waveMobs)
        {
            if (waveMobs != null)
            {
                bool isValid = true;
                Dictionary<string, string> modelState = new Dictionary<string, string>();
                List<int> listMobId = waveMobs.Select(x => x.MobId).Distinct().ToList();

                // Check that all mobs exist in db
                if (await this.mobBusiness.DoMobsExist(listMobId))
                {
                    foreach (WaveMob waveMob in waveMobs)
                    {
                        if (waveMob != null)
                        {
                            WaveMobEntity entity = waveMob.CreateEntity();
                            if (!waveMob.ValidationService.Validate(entity))
                            {
                                isValid = false;
                                foreach (string key in waveMob.ValidationService.ModelState.Keys)
                                {
                                    modelState.Add(key, waveMob.ValidationService.ModelState[key]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    isValid = false;
                }

                return new KeyValuePair<bool, Dictionary<string, string>>(isValid, modelState);
            }

            return new KeyValuePair<bool, Dictionary<string, string>>(false, new Dictionary<string, string>());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.mobBusiness?.Dispose();
            }
        }

        private List<WaveMob> GetWaveMobsFromEntity(List<WaveMobEntity> waveMobEntities)
        {
            List<WaveMob> waveMobs = new List<WaveMob>();

            foreach (WaveMobEntity waveMobEntity in waveMobEntities)
            {
                waveMobs.Add(new WaveMob(waveMobEntity, false));
            }

            return waveMobs;
        }
    }
}