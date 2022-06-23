namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Mob : BaseBusinessObject<MobEntity>
    {
        public Mob()
        {
            this.ValidationService = new MobValidation();
        }

        public Mob(MobEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new MobValidation();
            }

            this.Id = entity.Id;
            this.MobType = entity.MobType;
        }

        public Mob(MobEntity entity, bool hasError, List<MobStat> mobStats) : this(entity, hasError)
        {
            this.MobStats = mobStats;
        }

        public int Id { get; set; }

        public List<MobStat> MobStats { get; set; }

        public MobType MobType { get; set; }

        public override MobEntity CreateEntity()
        {
            return new MobEntity
            {
                Id = this.Id,
                MobType = this.MobType,
            };
        }
    }
}