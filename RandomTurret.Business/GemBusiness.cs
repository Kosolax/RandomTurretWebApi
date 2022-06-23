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

    public class GemBusiness : IGemBusiness
    {
        private readonly IGemDataAccess dataAccess;

        private readonly IGemStatBusiness gemStatBusiness;

        private readonly IRarityBusiness rarityBusiness;

        public GemBusiness(IGemDataAccess dataAccess, IGemStatBusiness gemStatBusiness, IRarityBusiness rarityBusiness)
        {
            this.dataAccess = dataAccess;
            this.gemStatBusiness = gemStatBusiness;
            this.rarityBusiness = rarityBusiness;
        }

        public async Task<KeyValuePair<bool, Gem>> CreateOrUpdate(Gem itemToCreateOrUpdate)
        {
            if (itemToCreateOrUpdate != null)
            {
                GemEntity entity = itemToCreateOrUpdate.CreateEntity();
                KeyValuePair<bool, Dictionary<string, string>> validationGemStats = await this.gemStatBusiness.ValidateList(itemToCreateOrUpdate.GemStats);

                if (validationGemStats.Key)
                {
                    if (itemToCreateOrUpdate.ValidationService.Validate(entity))
                    {
                        GemEntity gemEntityAlreadyExist = await this.dataAccess.GetFromGemType(entity.GemType, entity.RarityId);
                        if (entity.Id == default)
                        {
                            entity = await this.dataAccess.Create(entity);

                            if (gemEntityAlreadyExist == null && entity != null)
                            {
                                await this.gemStatBusiness.CreateList(entity.Id, entity.RarityId, itemToCreateOrUpdate.GemStats);
                                itemToCreateOrUpdate = await this.GetGemFromEntity(entity);
                                return new KeyValuePair<bool, Gem>(true, itemToCreateOrUpdate);
                            }
                        }
                        else
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);

                            if (entity != null && (gemEntityAlreadyExist == null || gemEntityAlreadyExist.Id == entity.Id))
                            {
                                await this.gemStatBusiness.UpdateList(entity.Id, entity.RarityId, itemToCreateOrUpdate.GemStats);
                                itemToCreateOrUpdate = await this.GetGemFromEntity(entity);
                                return new KeyValuePair<bool, Gem>(true, itemToCreateOrUpdate);
                            }
                        }
                    }
                }
                else
                {
                    itemToCreateOrUpdate.ValidationService.IsValid = false;
                    itemToCreateOrUpdate.ValidationService.ModelState = validationGemStats.Value;
                }
            }

            return new KeyValuePair<bool, Gem>(false, itemToCreateOrUpdate);
        }

        public async Task<Gem> Delete(GemType gemType, RarityType rarityType)
        {
            Gem gem = await this.Get(gemType, rarityType);

            if (gem != null)
            {
                await this.dataAccess.Delete(gem.Id);
                return gem;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<Gem> Get(GemType gemType, RarityType rarityType)
        {
            Rarity rarity = await this.rarityBusiness.Get(rarityType);

            if (rarity != null)
            {
                GemEntity entity = await this.dataAccess.GetFromGemType(gemType, rarity.Id);

                if (entity != null)
                {
                    return await this.GetGemFromEntity(entity);
                }
            }

            return null;
        }

        public async Task<List<Gem>> List()
        {
            List<GemEntity> gemEntities = await this.dataAccess.List();
            return await this.GetGemsFromEntities(gemEntities);
        }

        public async Task<List<Gem>> ListFromImpactType(ImpactType impactType)
        {
            List<GemEntity> gemEntities = await this.dataAccess.ListFromImpactType(impactType);
            return await this.GetGemsFromEntities(gemEntities);
        }

        public async Task<List<Gem>> ListFromMergeType(MergeType mergeType)
        {
            List<GemEntity> gemEntities = await this.dataAccess.ListFromMergeType(mergeType);
            return await this.GetGemsFromEntities(gemEntities);
        }

        public async Task<List<Gem>> ListFromShootType(ShootType shootType)
        {
            List<GemEntity> gemEntities = await this.dataAccess.ListFromShootType(shootType);
            return await this.GetGemsFromEntities(gemEntities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.gemStatBusiness?.Dispose();
                this.rarityBusiness?.Dispose();
            }
        }

        private async Task<Gem> GetGemFromEntity(GemEntity entity)
        {
            if (entity != null)
            {
                Gem gem = new Gem(entity, false);
                gem.GemStats = await this.gemStatBusiness.ListByGemIdAndRarityId(gem.Id, gem.RarityId);
                return gem;
            }

            return null;
        }

        private async Task<List<Gem>> GetGemsFromEntities(List<GemEntity> gemEntities)
        {
            List<Gem> gems = new List<Gem>();
            List<KeyValuePair<int, int>> gemsEntitiesId = gemEntities.Select(x => new KeyValuePair<int, int>(x.Id, x.RarityId)).ToList();
            Dictionary<int, List<GemStat>> dictionaryGemStatsByListGemIdAndRarityId = await this.gemStatBusiness.DictionaryGemStatsByListGemIdAndRarityId(gemsEntitiesId);

            foreach (GemEntity gemEntity in gemEntities)
            {
                if (dictionaryGemStatsByListGemIdAndRarityId[gemEntity.Id] != null)
                {
                    gems.Add(new Gem(gemEntity, false, dictionaryGemStatsByListGemIdAndRarityId[gemEntity.Id]));
                }
            }

            return gems;
        }
    }
}