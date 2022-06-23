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

    public class StatBusiness : IStatBusiness
    {
        private readonly IStatDataAccess dataAccess;

        public StatBusiness(IStatDataAccess statDataAccess)
        {
            this.dataAccess = statDataAccess;
        }

        public async Task<KeyValuePair<bool, Stat>> CreateOrUpdate(Stat itemToCreateOrUpdate)
        {
            if (itemToCreateOrUpdate != null)
            {
                StatEntity entity = itemToCreateOrUpdate.CreateEntity();

                if (itemToCreateOrUpdate.ValidationService.Validate(entity))
                {
                    StatEntity statEntityAlreadyExist = await this.dataAccess.GetByStatType(entity.StatType);
                    if (entity.Id == default)
                    {
                        if (statEntityAlreadyExist == null)
                        {
                            entity = await this.dataAccess.Create(entity);
                            itemToCreateOrUpdate = this.GetFromEntity(entity);

                            return new KeyValuePair<bool, Stat>(true, itemToCreateOrUpdate);
                        }

                    }
                    else
                    {
                        if (statEntityAlreadyExist == null || statEntityAlreadyExist.Id == entity.Id)
                        {
                            entity = await this.dataAccess.Update(entity);
                            itemToCreateOrUpdate = this.GetFromEntity(entity);

                            return new KeyValuePair<bool, Stat>(true, itemToCreateOrUpdate);
                        }

                    }
                }
            }

            return new KeyValuePair<bool, Stat>(false, itemToCreateOrUpdate);
        }

        public async Task<Stat> Delete(StatType statType)
        {
            Stat stat = await this.Get(statType);

            if (stat != null)
            {
                await this.dataAccess.Delete(stat.Id);
                return stat;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> DoStatsExist(List<int> idStats)
        {
            int count = await this.dataAccess.DoStatsExist(idStats);
            if (idStats.Count == count)
            {
                return true;
            }

            return false;
        }

        public async Task<Stat> Get(StatType statType)
        {
            StatEntity statEntity = await this.dataAccess.GetByStatType(statType);

            if (statEntity != null)
            {
                Stat stat = this.GetFromEntity(statEntity);
                return stat;
            }

            return null;
        }

        public async Task<List<Stat>> List()
        {
            return this.GetFromEntities(await this.dataAccess.List());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
            }
        }

        private List<Stat> GetFromEntities(List<StatEntity> statEntities)
        {
            List<Stat> stats = new List<Stat>();
            foreach (StatEntity statEntity in statEntities)
            {
                stats.Add(new Stat(statEntity, false));
            }

            return stats;
        }

        private Stat GetFromEntity(StatEntity statEntity)
        {
            return new Stat(statEntity, false);
        }
    }
}