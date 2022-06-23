namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class RarityBusiness : IRarityBusiness
    {
        private readonly IRarityDataAccess dataAccess;

        public RarityBusiness(IRarityDataAccess IRarityDataAccess)
        {
            this.dataAccess = IRarityDataAccess;
        }

        public async Task<KeyValuePair<bool, Rarity>> CreateOrUpdate(Rarity itemToCreateOrUpdate)
        {
            if (itemToCreateOrUpdate != null)
            {
                RarityEntity entity = itemToCreateOrUpdate.CreateEntity();
                if (itemToCreateOrUpdate.ValidationService.Validate(entity))
                {
                    RarityEntity rarityEntityAlreadyExist = await this.dataAccess.GetByRarityType(entity.RarityType);
                    if (entity.Id == default)
                    {
                        if (rarityEntityAlreadyExist == null)
                        {
                            entity = await this.dataAccess.Create(entity);
                            itemToCreateOrUpdate = this.GetRarityFromEntity(entity);
                            return new KeyValuePair<bool, Rarity>(true, itemToCreateOrUpdate);
                        }
                    }
                    else
                    {
                        if (rarityEntityAlreadyExist == null || rarityEntityAlreadyExist.Id == entity.Id)
                        {
                            entity = await this.dataAccess.Update(entity, entity.Id);
                            itemToCreateOrUpdate = this.GetRarityFromEntity(entity);
                            return new KeyValuePair<bool, Rarity>(true, itemToCreateOrUpdate);
                        }
                    }
                }
            }

            return new KeyValuePair<bool, Rarity>(false, itemToCreateOrUpdate);
        }

        public async Task<Rarity> Delete(RarityType rarityType)
        {
            Rarity rarity = await this.Get(rarityType);

            if (rarity != null)
            {
                await this.dataAccess.Delete(rarity.Id);
                return rarity;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> DoRaritiesExist(List<int> idRarities)
        {
            int count = await this.dataAccess.DoRaritiesExist(idRarities);
            if (idRarities.Count == count)
            {
                return true;
            }

            return false;
        }

        public async Task<Rarity> Get(RarityType rarityType)
        {
            RarityEntity rarityEntity = await this.dataAccess.GetByRarityType(rarityType);

            if (rarityEntity != null)
            {
                return this.GetRarityFromEntity(rarityEntity);
            }

            return null;
        }

        public Rarity GetRarityFromEntity(RarityEntity rarityEntity)
        {
            if (rarityEntity != null)
            {
                return new Rarity(rarityEntity, false);
            }

            return null;
        }

        public async Task<List<Rarity>> List()
        {
            List<RarityEntity> rarityEntities = await this.dataAccess.List();
            return this.GetRaritiesFromEntity(rarityEntities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
            }
        }

        private List<Rarity> GetRaritiesFromEntity(List<RarityEntity> rarityEntities)
        {
            List<Rarity> rarities = new List<Rarity>();

            foreach (RarityEntity rarityEntity in rarityEntities)
            {
                rarities.Add(new Rarity(rarityEntity, false));
            }

            return rarities;
        }
    }
}