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

    public class MobBusiness : IMobBusiness
    {
        private readonly IMobDataAccess dataAccess;

        private readonly IMobStatBusiness mobStatBusiness;

        public MobBusiness(IMobDataAccess mobDataAccess, IMobStatBusiness mobStatBusiness)
        {
            this.dataAccess = mobDataAccess;
            this.mobStatBusiness = mobStatBusiness;
        }

        public async Task<KeyValuePair<bool, Mob>> CreateOrUpdate(Mob mobToCreateOrUpdate)
        {
            if (mobToCreateOrUpdate != null)
            {
                MobEntity entity = mobToCreateOrUpdate.CreateEntity();
                KeyValuePair<bool, Dictionary<string, string>> validationMobStats = await this.mobStatBusiness.ValidateList(mobToCreateOrUpdate.MobStats);

                if (validationMobStats.Key)
                {
                    if (mobToCreateOrUpdate.ValidationService.Validate(entity))
                    {
                        MobEntity mobEntityAlreadyExist = await this.dataAccess.GetByMobType(entity.MobType);
                        if (entity.Id == default)
                        {
                            if (mobEntityAlreadyExist == null)
                            {
                                entity = await this.dataAccess.Create(entity);

                                if (entity != null)
                                {
                                    await this.mobStatBusiness.CreateList(entity.Id, mobToCreateOrUpdate.MobStats);
                                    mobToCreateOrUpdate = await this.GetMobFromEntity(entity);
                                    return new KeyValuePair<bool, Mob>(true, mobToCreateOrUpdate);
                                }
                            }
                        }
                        else
                        {
                            if (mobEntityAlreadyExist == null || mobEntityAlreadyExist.Id == entity.Id)
                            {
                                entity = await this.dataAccess.Update(entity, entity.Id);

                                if (entity != null)
                                {
                                    await this.mobStatBusiness.UpdateList(entity.Id, mobToCreateOrUpdate.MobStats);
                                    mobToCreateOrUpdate = await this.GetMobFromEntity(entity);
                                    return new KeyValuePair<bool, Mob>(true, mobToCreateOrUpdate);
                                }
                            }
                        }
                    }
                }
                else
                {
                    mobToCreateOrUpdate.ValidationService.IsValid = false;
                    mobToCreateOrUpdate.ValidationService.ModelState = validationMobStats.Value;
                }
            }

            return new KeyValuePair<bool, Mob>(false, mobToCreateOrUpdate);
        }

        public async Task<Mob> Delete(MobType mobType)
        {
            Mob mob = await this.Get(mobType);

            if (mob != null)
            {
                await this.dataAccess.Delete(mob.Id);
                return mob;
            }

            return null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<bool> DoMobsExist(List<int> listMobId)
        {
            int count = await this.dataAccess.DoMobsExist(listMobId);
            if (listMobId.Count == count)
            {
                return true;
            }

            return false;
        }

        public async Task<Mob> Get(MobType mobType)
        {
            MobEntity entity = await this.dataAccess.GetByMobType(mobType);

            if (entity != null)
            {
                return await this.GetMobFromEntity(entity);
            }

            return null;
        }

        public async Task<List<Mob>> List()
        {
            List<MobEntity> mobEntities = await this.dataAccess.List();
            return await this.GetMobsFromEntity(mobEntities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dataAccess?.Dispose();
                this.mobStatBusiness?.Dispose();
            }
        }

        private async Task<Mob> GetMobFromEntity(MobEntity entity)
        {
            if (entity != null)
            {
                Mob mob = new Mob(entity, false);
                mob.MobStats = await this.mobStatBusiness.ListByMobId(mob.Id);
                return mob;
            }

            return null;
        }

        private async Task<List<Mob>> GetMobsFromEntity(List<MobEntity> mobsEntities)
        {
            List<Mob> mobs = new List<Mob>();
            List<int> mobsEntitiesId = mobsEntities.Select(x => x.Id).ToList();
            Dictionary<int, List<MobStat>> dictionaryIdMobListMobStats = await this.mobStatBusiness.DictionaryMobsStatsByListMobId(mobsEntitiesId);

            foreach (MobEntity mobEntity in mobsEntities)
            {
                if (dictionaryIdMobListMobStats.ContainsKey(mobEntity.Id))
                {
                    mobs.Add(new Mob(mobEntity, false, dictionaryIdMobListMobStats[mobEntity.Id]));
                }
                else
                {
                    mobs.Add(new Mob(mobEntity, false));
                }
            }

            return mobs;
        }
    }
}