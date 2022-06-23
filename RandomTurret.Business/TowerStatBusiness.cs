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

    public class TowerStatBusiness : ITowerStatBusiness
    {
        private readonly ITowerStatDataAccess dataAccess;

        private readonly IStatBusiness statBusiness;

        public TowerStatBusiness(ITowerStatDataAccess towerStatDataAccess, IStatBusiness statBusiness)
        {
            this.dataAccess = towerStatDataAccess;
            this.statBusiness = statBusiness;
        }

        public async Task CreateList(int towerId, List<TowerStat> towerStats)
        {
            List<TowerStatEntity> towerStatEntities = new List<TowerStatEntity>();

            foreach (TowerStat towerStat in towerStats)
            {
                TowerStatEntity entity = towerStat.CreateEntity();
                entity.Id = 0;
                entity.TowerEntityId = towerId;
                towerStatEntities.Add(entity);
            }

            await this.dataAccess.CreateList(towerStatEntities);
        }

        public async Task<Dictionary<int, List<TowerStat>>> DictionaryTowersStatsByListTowerId(List<int> idTowers)
        {
            List<TowerStatEntity> towerStatEntities = await this.dataAccess.ListByListTowerId(idTowers);
            List<TowerStat> towerStats = this.GetTowerStatsFromEntity(towerStatEntities);

            return towerStats.GroupBy(x => x.TowerId).ToDictionary(x => x.Key, x => x.ToList());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<TowerStat>> ListByTowerId(int idTower)
        {
            List<TowerStatEntity> towerStatEntities = await this.dataAccess.ListByTowerId(idTower);
            return this.GetTowerStatsFromEntity(towerStatEntities);
        }

        public async Task UpdateList(int idTower, List<TowerStat> towerStats)
        {
            List<TowerStatEntity> towerStatEntities = await this.dataAccess.ListByTowerId(idTower);
            List<int> listId = towerStatEntities.Select(x => x.Id).ToList();
            List<TowerStatEntity> towerStatsToUpdate = new List<TowerStatEntity>();
            List<TowerStatEntity> towerStatsToCreate = new List<TowerStatEntity>();

            foreach (TowerStat towerStat in towerStats)
            {
                towerStat.TowerId = idTower;
                if (towerStat.Id == default)
                {
                    towerStatsToCreate.Add(towerStat.CreateEntity());
                }
                else if (listId.Contains(towerStat.Id))
                {
                    towerStatsToUpdate.Add(towerStat.CreateEntity());
                    towerStatEntities.Remove(towerStatEntities.Where(x => x.Id == towerStat.Id).FirstOrDefault());
                }
            }

            List<TowerStatEntity> towerStatsToDelete = towerStatEntities;

            await this.dataAccess.CreateList(towerStatsToCreate);
            await this.dataAccess.UpdateRangeAsync(towerStatsToUpdate);
            await this.dataAccess.DeleteList(towerStatsToDelete);
        }

        public async Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<TowerStat> towerStats)
        {
            if (towerStats != null)
            {
                bool isValid = true;
                Dictionary<string, string> modelState = new Dictionary<string, string>();
                List<int> idStats = towerStats.Select(x => x.StatId).Distinct().ToList();

                // Check that all stats exist in db
                if (await this.statBusiness.DoStatsExist(idStats))
                {
                    foreach (TowerStat towerStat in towerStats)
                    {
                        if (towerStat != null)
                        {
                            TowerStatEntity entity = towerStat.CreateEntity();
                            if (!towerStat.ValidationService.Validate(entity))
                            {
                                isValid = false;
                                foreach (string key in towerStat.ValidationService.ModelState.Keys)
                                {
                                    modelState.Add(key, towerStat.ValidationService.ModelState[key]);
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

        private List<TowerStat> GetTowerStatsFromEntity(List<TowerStatEntity> towerStatEntities)
        {
            List<TowerStat> towerStats = new List<TowerStat>();

            foreach (TowerStatEntity towerStatEntity in towerStatEntities)
            {
                towerStats.Add(new TowerStat(towerStatEntity, false));
            }

            return towerStats;
        }
    }
}