namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class GemPlayerBusiness : IGemPlayerBusiness
    {
        private readonly IGemPlayerDataAccess dataAccess;

        public GemPlayerBusiness(IGemPlayerDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<KeyValuePair<bool, GemPlayer>> Create(GemPlayer itemToCreate)
        {
            if (itemToCreate != null)
            {
                GemPlayerEntity entity = itemToCreate.CreateEntity();

                if (itemToCreate.ValidationService.Validate(entity))
                {
                    if (entity.Id == default)
                    {
                        entity = await this.dataAccess.Create(entity);
                        itemToCreate = this.GetGemPlayerFromEntity(entity);
                        return new KeyValuePair<bool, GemPlayer>(true, itemToCreate);
                    }
                }
            }

            return new KeyValuePair<bool, GemPlayer>(false, itemToCreate);
        }

        public async Task<GemPlayer> Delete(int id)
        {
            if (id != default)
            {
                GemPlayer gemPlayer = await this.Get(id);

                if (gemPlayer != null)
                {
                    await this.dataAccess.Delete(id);
                    return gemPlayer;
                }
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
            }
        }

        private async Task<GemPlayer> Get(int id)
        {
            if (id != default)
            {
                GemPlayerEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    return this.GetGemPlayerFromEntity(entity);
                }
            }

            return null;
        }

        private GemPlayer GetGemPlayerFromEntity(GemPlayerEntity entity)
        {
            if (entity != null)
            {
                GemPlayer gemPlayer = new GemPlayer(entity, false);
                return gemPlayer;
            }

            return null;
        }
    }
}