namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class MobStat : BaseBusinessObject<MobStatEntity>
    {
        public MobStat()
        {
            this.ValidationService = new MobStatValidation();
        }

        public MobStat(MobStatEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new MobStatValidation();
            }

            this.Id = entity.Id;
            this.Value = entity.Value;
            this.StatId = entity.StatEntityId;
            this.MobId = entity.MobEntityId;
        }

        public int Id { get; set; }

        public int MobId { get; set; }

        public int StatId { get; set; }

        public float Value { get; set; }

        public override MobStatEntity CreateEntity()
        {
            return new MobStatEntity
            {
                Id = this.Id,
                StatEntityId = this.StatId,
                Value = this.Value,
                MobEntityId = this.MobId,
            };
        }
    }
}