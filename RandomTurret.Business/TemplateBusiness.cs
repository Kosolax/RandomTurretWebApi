namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class TemplateBusiness : ITemplateBusiness
    {
        private readonly ITemplateDataAccess dataAccess;

        private readonly IRarityBusiness rarityBusiness;

        private readonly ITemplateStatBusiness templateStatBusiness;

        public TemplateBusiness(ITemplateDataAccess dataAccess, ITemplateStatBusiness templateStatBusiness, IRarityBusiness rarityBusiness)
        {
            this.dataAccess = dataAccess;
            this.templateStatBusiness = templateStatBusiness;
            this.rarityBusiness = rarityBusiness;
        }

        public async Task<KeyValuePair<bool, Template>> CreateOrUpdate(Template itemToCreateOrUpdate)
        {
            if (itemToCreateOrUpdate != null)
            {
                TemplateEntity entity = itemToCreateOrUpdate.CreateEntity();
                KeyValuePair<bool, Dictionary<string, string>> validationTemplateStats = await this.templateStatBusiness.ValidateList(itemToCreateOrUpdate.TemplateStats);

                if (validationTemplateStats.Key)
                {
                    if (itemToCreateOrUpdate.ValidationService.Validate(entity))
                    {
                        TemplateEntity templateEntityAlreadyExist = await this.dataAccess.GetFromTemplateType(entity.TemplateType, entity.RarityId);
                        if (entity.Id == default)
                        {
                            entity = await this.dataAccess.Create(entity);

                            if (templateEntityAlreadyExist == null && entity != null)
                            {
                                await this.templateStatBusiness.CreateList(entity.Id, entity.RarityId, itemToCreateOrUpdate.TemplateStats);
                                itemToCreateOrUpdate = await this.GetTemplateFromEntity(entity);
                                return new KeyValuePair<bool, Template>(true, itemToCreateOrUpdate);
                            }
                        }
                        else
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);

                            if (entity != null && (templateEntityAlreadyExist == null || templateEntityAlreadyExist.Id == entity.Id))
                            {
                                await this.templateStatBusiness.UpdateList(entity.Id, entity.RarityId, itemToCreateOrUpdate.TemplateStats);
                                itemToCreateOrUpdate = await this.GetTemplateFromEntity(entity);
                                return new KeyValuePair<bool, Template>(true, itemToCreateOrUpdate);
                            }
                        }
                    }
                }
                else
                {
                    itemToCreateOrUpdate.ValidationService.IsValid = false;
                    itemToCreateOrUpdate.ValidationService.ModelState = validationTemplateStats.Value;
                }
            }

            return new KeyValuePair<bool, Template>(false, itemToCreateOrUpdate);
        }

        public async Task<Template> Delete(TemplateType templateType, RarityType rarityType)
        {
            Template template = await this.Get(templateType, rarityType);

            if (template != null)
            {
                await this.dataAccess.Delete(template.Id);
                return template;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Template> Get(TemplateType templateType, RarityType rarityType)
        {
            Rarity rarity = await this.rarityBusiness.Get(rarityType);

            if (rarity != null)
            {
                TemplateEntity entity = await this.dataAccess.GetFromTemplateType(templateType, rarity.Id);

                if (entity != null)
                {
                    return await this.GetTemplateFromEntity(entity);
                }
            }

            return null;
        }

        public async Task<List<Template>> List()
        {
            List<TemplateEntity> templateEntities = await this.dataAccess.List();
            return await this.GetTemplatesFromEntities(templateEntities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.templateStatBusiness?.Dispose();
                this.rarityBusiness?.Dispose();
            }
        }

        private async Task<Template> GetTemplateFromEntity(TemplateEntity entity)
        {
            if (entity != null)
            {
                Template template = new Template(entity, false);
                template.TemplateStats = await this.templateStatBusiness.ListByTemplateIdAndRarityId(template.Id, template.RarityId);
                return template;
            }

            return null;
        }

        private async Task<List<Template>> GetTemplatesFromEntities(List<TemplateEntity> templateEntities)
        {
            List<Template> templates = new List<Template>();
            List<KeyValuePair<int, int>> templatesEntitiesId = templateEntities.Select(x => new KeyValuePair<int, int>(x.Id, x.RarityId)).ToList();
            Dictionary<int, List<TemplateStat>> dictionaryTemplateStatsByListTemplateIdAndRarityId = await this.templateStatBusiness.DictionaryTemplateStatsByListTemplateIdAndRarityId(templatesEntitiesId);

            foreach (TemplateEntity templateEntity in templateEntities)
            {
                if (dictionaryTemplateStatsByListTemplateIdAndRarityId[templateEntity.Id] != null)
                {
                    templates.Add(new Template(templateEntity, false, dictionaryTemplateStatsByListTemplateIdAndRarityId[templateEntity.Id]));
                }
            }

            return templates;
        }
    }
}