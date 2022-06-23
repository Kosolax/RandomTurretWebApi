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

    public class GemStatBusiness : IGemStatBusiness
    {
        private readonly IGemStatDataAccess dataAccess;

        private readonly IRarityBusiness rarityBusiness;

        private readonly IStatBusiness statBusiness;

        public GemStatBusiness(IGemStatDataAccess dataAccess, IStatBusiness statBusiness, IRarityBusiness rarityBusiness)
        {
            this.dataAccess = dataAccess;
            this.statBusiness = statBusiness;
            this.rarityBusiness = rarityBusiness;
        }

        public async Task CreateList(int gemId, int rarityId, List<GemStat> gemStats)
        {
            List<GemStatEntity> gemStatEntities = new List<GemStatEntity>();

            foreach (GemStat gemStat in gemStats)
            {
                GemStatEntity entity = gemStat.CreateEntity();
                entity.Id = 0;
                entity.GemId = gemId;
                entity.RarityId = rarityId;
                gemStatEntities.Add(entity);
            }

            await this.dataAccess.CreateList(gemStatEntities);
        }

        public async Task<Dictionary<int, List<GemStat>>> DictionaryGemStatsByListGemIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs)
        {
            List<GemStatEntity> gemStatEntities = await this.dataAccess.ListByListGemIdAndRarityId(keyValuePairs);
            List<GemStat> gemStats = this.GetGemStatsFromEntity(gemStatEntities);

            return gemStats.GroupBy(x => x.GemId).ToDictionary(x => x.Key, x => x.ToList());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<GemStat>> ListByGemIdAndRarityId(int gemId, int rarityId)
        {
            List<GemStatEntity> gemStatEntities = await this.dataAccess.ListByGemIdAndRarityId(gemId, rarityId);
            return this.GetGemStatsFromEntity(gemStatEntities);
        }

        public async Task UpdateList(int gemId, int rarityId, List<GemStat> gemStats)
        {
            List<KeyValuePair<int, int>> objects = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(gemId, rarityId), };
            List<GemStatEntity> gemStatEntities = await this.dataAccess.ListByListGemIdAndRarityId(objects);
            List<int> listId = gemStatEntities.Select(x => x.Id).ToList();
            List<GemStatEntity> gemStatsToUpdate = new List<GemStatEntity>();
            List<GemStatEntity> gemStatsToCreate = new List<GemStatEntity>();

            foreach (GemStat gemStat in gemStats)
            {
                gemStat.GemId = gemId;
                if (gemStat.Id == default)
                {
                    gemStatsToCreate.Add(gemStat.CreateEntity());
                }
                else if (listId.Contains(gemStat.Id))
                {
                    gemStatsToUpdate.Add(gemStat.CreateEntity());
                    gemStatEntities.Remove(gemStatEntities.Where(x => x.Id == gemStat.Id).FirstOrDefault());
                }
            }

            List<GemStatEntity> gemStatsToDelete = gemStatEntities;

            await this.dataAccess.CreateList(gemStatsToCreate);
            await this.dataAccess.UpdateRangeAsync(gemStatsToUpdate);
            await this.dataAccess.DeleteList(gemStatsToDelete);
        }

        public async Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<GemStat> gemStats)
        {
            if (gemStats != null)
            {
                bool isValid = true;
                Dictionary<string, string> modelState = new Dictionary<string, string>();
                List<int> idStats = gemStats.Select(x => x.StatId).Distinct().ToList();
                List<int> idRarities = gemStats.Select(x => x.RarityId).Distinct().ToList();

                // Check that all stats exist in db && check that all rarities exist in db
                if (await this.statBusiness.DoStatsExist(idStats) && await this.rarityBusiness.DoRaritiesExist(idRarities))
                {
                    foreach (GemStat gemStat in gemStats)
                    {
                        if (gemStat != null)
                        {
                            GemStatEntity entity = gemStat.CreateEntity();
                            if (!gemStat.ValidationService.Validate(entity))
                            {
                                isValid = false;
                                foreach (string key in gemStat.ValidationService.ModelState.Keys)
                                {
                                    modelState.Add(key, gemStat.ValidationService.ModelState[key]);
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
                this.rarityBusiness?.Dispose();
            }
        }

        private List<GemStat> GetGemStatsFromEntity(List<GemStatEntity> gemStatEntities)
        {
            List<GemStat> gemStats = new List<GemStat>();

            foreach (GemStatEntity gemStatEntity in gemStatEntities)
            {
                gemStats.Add(new GemStat(gemStatEntity, false));
            }

            return gemStats;
        }
    }
}