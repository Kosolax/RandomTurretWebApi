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

    public class TemplateStatBusiness : ITemplateStatBusiness
    {
        private readonly ITemplateStatDataAccess dataAccess;

        private readonly IRarityBusiness rarityBusiness;

        private readonly IStatBusiness statBusiness;

        public TemplateStatBusiness(ITemplateStatDataAccess dataAccess, IStatBusiness statBusiness, IRarityBusiness rarityBusiness)
        {
            this.dataAccess = dataAccess;
            this.statBusiness = statBusiness;
            this.rarityBusiness = rarityBusiness;
        }

        public async Task CreateList(int templateId, int rarityId, List<TemplateStat> templateStats)
        {
            List<TemplateStatEntity> templateStatEntities = new List<TemplateStatEntity>();

            foreach (TemplateStat templateStat in templateStats)
            {
                TemplateStatEntity entity = templateStat.CreateEntity();
                entity.Id = 0;
                entity.TemplateId = templateId;
                entity.RarityId = rarityId;
                templateStatEntities.Add(entity);
            }

            await this.dataAccess.CreateList(templateStatEntities);
        }

        public async Task<Dictionary<int, List<TemplateStat>>> DictionaryTemplateStatsByListTemplateIdAndRarityId(List<KeyValuePair<int, int>> keyValuePairs)
        {
            List<TemplateStatEntity> templateStatEntities = await this.dataAccess.ListByListTemplateIdAndRarityId(keyValuePairs);
            List<TemplateStat> templateStats = this.GetTemplateStatsFromEntity(templateStatEntities);

            return templateStats.GroupBy(x => x.TemplateId).ToDictionary(x => x.Key, x => x.ToList());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<TemplateStat>> ListByTemplateIdAndRarityId(int templateId, int rarityId)
        {
            List<TemplateStatEntity> templateStatEntities = await this.dataAccess.ListByTemplateIdAndRarityId(templateId, rarityId);
            return this.GetTemplateStatsFromEntity(templateStatEntities);
        }

        public async Task UpdateList(int templateId, int rarityId, List<TemplateStat> templateStats)
        {
            List<KeyValuePair<int, int>> objects = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(templateId, rarityId), };
            List<TemplateStatEntity> templateStatEntities = await this.dataAccess.ListByListTemplateIdAndRarityId(objects);
            List<int> listId = templateStatEntities.Select(x => x.Id).ToList();
            List<TemplateStatEntity> templateStatsToUpdate = new List<TemplateStatEntity>();
            List<TemplateStatEntity> templateStatsToCreate = new List<TemplateStatEntity>();

            foreach (TemplateStat templateStat in templateStats)
            {
                templateStat.TemplateId = templateId;
                if (templateStat.Id == default)
                {
                    templateStatsToCreate.Add(templateStat.CreateEntity());
                }
                else if (listId.Contains(templateStat.Id))
                {
                    templateStatsToUpdate.Add(templateStat.CreateEntity());
                    templateStatEntities.Remove(templateStatEntities.Where(x => x.Id == templateStat.Id).FirstOrDefault());
                }
            }

            List<TemplateStatEntity> templateStatsToDelete = templateStatEntities;

            await this.dataAccess.CreateList(templateStatsToCreate);
            await this.dataAccess.UpdateRangeAsync(templateStatsToUpdate);
            await this.dataAccess.DeleteList(templateStatsToDelete);
        }

        public async Task<KeyValuePair<bool, Dictionary<string, string>>> ValidateList(List<TemplateStat> templateStats)
        {
            if (templateStats != null)
            {
                bool isValid = true;
                Dictionary<string, string> modelState = new Dictionary<string, string>();
                List<int> idStats = templateStats.Select(x => x.StatId).Distinct().ToList();
                List<int> idRarities = templateStats.Select(x => x.RarityId).Distinct().ToList();

                // Check that all stats exist in db && check that all rarities exist in db
                if (await this.statBusiness.DoStatsExist(idStats) && await this.rarityBusiness.DoRaritiesExist(idRarities))
                {
                    foreach (TemplateStat templateStat in templateStats)
                    {
                        if (templateStat != null)
                        {
                            TemplateStatEntity entity = templateStat.CreateEntity();
                            if (!templateStat.ValidationService.Validate(entity))
                            {
                                isValid = false;
                                foreach (string key in templateStat.ValidationService.ModelState.Keys)
                                {
                                    modelState.Add(key, templateStat.ValidationService.ModelState[key]);
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

        private List<TemplateStat> GetTemplateStatsFromEntity(List<TemplateStatEntity> templateStatEntities)
        {
            List<TemplateStat> templateStats = new List<TemplateStat>();

            foreach (TemplateStatEntity templateStatEntity in templateStatEntities)
            {
                templateStats.Add(new TemplateStat(templateStatEntity, false));
            }

            return templateStats;
        }
    }
}