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

    public class MobStatBusiness : IMobStatBusiness
    {
        private readonly IMobStatDataAccess dataAccess;

        private readonly IStatBusiness statBusiness;

        public MobStatBusiness(IMobStatDataAccess mobStatDataAccess, IStatBusiness statBusiness)
        {
            this.dataAccess = mobStatDataAccess;
            this.statBusiness = statBusiness;
        }

        public async Task CreateList(int idMob, List<MobStat> mobStats)
        {
            List<MobStatEntity> mobStatEntities = new List<MobStatEntity>();

            foreach (MobStat mobStat in mobStats)
            {
                MobStatEntity entity = mobStat.CreateEntity();
                entity.Id = 0;
                entity.MobEntityId = idMob;
                mobStatEntities.Add(entity);
            }

            await this.dataAccess.CreateList(mobStatEntities);
        }

        public async Task<Dictionary<int, List<MobStat>>> DictionaryMobsStatsByListMobId(List<int> idMobs)
        {
            List<MobStatEntity> mobStatEntities = await this.dataAccess.ListByListMobId(idMobs);
            List<MobStat> mobStats = this.GetMobStatsFromEntity(mobStatEntities);

            return mobStats.GroupBy(x => x.MobId).ToDictionary(x => x.Key, x => x.ToList());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<MobStat>> ListByMobId(int idMob)
        {
            List<MobStatEntity> mobStatEntities = await this.dataAccess.ListByMobId(idMob);
            return this.GetMobStatsFromEntity(mobStatEntities);
        }

        public async Task UpdateList(int idMob, List<MobStat> mobStats)
        {
            List<MobStatEntity> mobStatEntities = await this.dataAccess.ListByMobId(idMob);
            List<int> listId = mobStatEntities.Select(x => x.Id).ToList();
            List<MobStatEntity> mobStatsToUpdate = new List<MobStatEntity>();
            List<MobStatEntity> mobStatsToCreate = new List<MobStatEntity>();

            foreach (MobStat mobStat in mobStats)
            {
                mobStat.MobId = idMob;
                if (mobStat.Id == default)
                {
                    mobStatsToCreate.Add(mobStat.CreateEntity());
                }
                else if (listId.Contains(mobStat.Id))
                {
                    mobStatsToUpdate.Add(mobStat.CreateEntity());
                    mobStatEntities.Remove(mobStatEntities.Where(x => x.Id == mobStat.Id).FirstOrDefault());
                }
            }

            List<MobStatEntity> mobStatsToDelete = mobStatEntities;

            await this.dataAccess.CreateList(mobStatsToCreate);
            await this.dataAccess.UpdateRangeAsync(mobStatsToUpdate);
            await this.dataAccess.DeleteList(mobStatsToDelete);
        }

        public async Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<MobStat> mobStats)
        {
            if (mobStats != null)
            {
                bool isValid = true;
                Dictionary<string, string> modelState = new Dictionary<string, string>();
                List<int> idStats = mobStats.Select(x => x.StatId).Distinct().ToList();

                // Check that all stats exist in db
                if (await this.statBusiness.DoStatsExist(idStats))
                {
                    foreach (MobStat mobStat in mobStats)
                    {
                        if (mobStat != null)
                        {
                            MobStatEntity entity = mobStat.CreateEntity();
                            if (!mobStat.ValidationService.Validate(entity))
                            {
                                isValid = false;
                                foreach (string key in mobStat.ValidationService.ModelState.Keys)
                                {
                                    modelState.Add(key, mobStat.ValidationService.ModelState[key]);
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
                this.statBusiness?.Dispose();
            }
        }

        private List<MobStat> GetMobStatsFromEntity(List<MobStatEntity> mobStatEntities)
        {
            List<MobStat> mobStats = new List<MobStat>();

            foreach (MobStatEntity mobStatEntity in mobStatEntities)
            {
                mobStats.Add(new MobStat(mobStatEntity, false));
            }

            return mobStats;
        }
    }
}