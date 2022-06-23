namespace RandomTurret.Business
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RandomTurret.BusinessObject;
    using RandomTurret.Entities;
    using RandomTurret.IBusiness;
    using RandomTurret.IDataAccess;

    public class TemplatePlayerBusiness : ITemplatePlayerBusiness
    {
        private readonly ITemplatePlayerDataAccess dataAccess;

        public TemplatePlayerBusiness(ITemplatePlayerDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<KeyValuePair<bool, TemplatePlayer>> Create(TemplatePlayer itemToCreate)
        {
            if (itemToCreate != null)
            {
                TemplatePlayerEntity entity = itemToCreate.CreateEntity();

                if (itemToCreate.ValidationService.Validate(entity))
                {
                    if (entity.Id == default)
                    {
                        entity = await this.dataAccess.Create(entity);
                        itemToCreate = this.GetTemplatePlayerFromEntity(entity);
                        return new KeyValuePair<bool, TemplatePlayer>(true, itemToCreate);
                    }
                }
            }

            return new KeyValuePair<bool, TemplatePlayer>(false, itemToCreate);
        }

        public async Task<TemplatePlayer> Delete(int id)
        {
            if (id != default)
            {
                TemplatePlayer templatePlayer = await this.Get(id);

                if (templatePlayer != null)
                {
                    await this.dataAccess.Delete(id);
                    return templatePlayer;
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

        private async Task<TemplatePlayer> Get(int id)
        {
            if (id != default)
            {
                TemplatePlayerEntity entity = await this.dataAccess.Find(id);

                if (entity != null)
                {
                    return this.GetTemplatePlayerFromEntity(entity);
                }
            }

            return null;
        }

        private TemplatePlayer GetTemplatePlayerFromEntity(TemplatePlayerEntity entity)
        {
            if (entity != null)
            {
                TemplatePlayer templatePlayer = new TemplatePlayer(entity, false);
                return templatePlayer;
            }

            return null;
        }
    }
}