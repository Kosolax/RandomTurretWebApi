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

    public class TowerBusiness : ITowerBusiness
    {
        private readonly ITowerDataAccess dataAccess;

        private readonly ITowerStatBusiness towerStatBusiness;

        public TowerBusiness(ITowerDataAccess towerDataAccess, ITowerStatBusiness towerStatBusiness)
        {
            this.dataAccess = towerDataAccess;
            this.towerStatBusiness = towerStatBusiness;
        }

        public async Task<KeyValuePair<bool, Tower>> CreateOrUpdate(Tower towerToCreateOrUpdate)
        {
            if (towerToCreateOrUpdate != null)
            {
                TowerEntity entity = towerToCreateOrUpdate.CreateEntity();
                KeyValuePair<bool, Dictionary<string, string>> validationTowerStats = await this.towerStatBusiness.ValidateList(towerToCreateOrUpdate.TowerStats);

                if (validationTowerStats.Key)
                {
                    if (towerToCreateOrUpdate.ValidationService.Validate(entity))
                    {
                        if (entity.Id == default)
                        {
                            entity = await this.dataAccess.Create(entity);

                            if (entity != null)
                            {
                                await this.towerStatBusiness.CreateList(entity.Id, towerToCreateOrUpdate.TowerStats);
                                towerToCreateOrUpdate = await this.GetTowerFromEntity(entity);
                                return new KeyValuePair<bool, Tower>(true, towerToCreateOrUpdate);
                            }
                        }
                        else
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);

                            if (entity != null)
                            {
                                await this.towerStatBusiness.UpdateList(entity.Id, towerToCreateOrUpdate.TowerStats);
                                towerToCreateOrUpdate = await this.GetTowerFromEntity(entity);
                                return new KeyValuePair<bool, Tower>(true, towerToCreateOrUpdate);
                            }
                        }
                    }
                }
                else
                {
                    towerToCreateOrUpdate.ValidationService.IsValid = false;
                    towerToCreateOrUpdate.ValidationService.ModelState = validationTowerStats.Value;
                }
            }

            return new KeyValuePair<bool, Tower>(false, towerToCreateOrUpdate);
        }

        public async Task<Tower> Delete(int id)
        {
            if (id != default)
            {
                Tower tower = await this.Get(id);

                if (tower != null)
                {
                    await this.dataAccess.Delete(id);
                    return tower;
                }
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Tower> Get(int id)
        {
            if (id != default)
            {
                TowerEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    return await this.GetTowerFromEntity(entity);
                }
            }

            return null;
        }

        public async Task<List<Tower>> ListByPlayerId(int playerId)
        {
            List<TowerEntity> towerEntities = await this.dataAccess.ListByPlayerId(playerId);
            return await this.GetTowersFromEntity(towerEntities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.towerStatBusiness?.Dispose();
            }
        }

        private async Task<Tower> GetTowerFromEntity(TowerEntity entity)
        {
            if (entity != null)
            {
                Tower tower = new Tower(entity, false);
                tower.TowerStats = await this.towerStatBusiness.ListByTowerId(tower.Id);
                return tower;
            }

            return null;
        }

        private async Task<List<Tower>> GetTowersFromEntity(List<TowerEntity> towersEntities)
        {
            List<Tower> towers = new List<Tower>();
            List<int> towersEntitiesId = towersEntities.Select(x => x.Id).ToList();
            Dictionary<int, List<TowerStat>> dictionaryIdTowerListTowerStats = await this.towerStatBusiness.DictionaryTowersStatsByListTowerId(towersEntitiesId);

            foreach (TowerEntity towerEntity in towersEntities)
            {
                if (dictionaryIdTowerListTowerStats[towerEntity.Id] != null)
                {
                    towers.Add(new Tower(towerEntity, false, dictionaryIdTowerListTowerStats[towerEntity.Id]));
                }
            }

            return towers;
        }
    }
}