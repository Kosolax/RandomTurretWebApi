namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class TowerStat : BaseBusinessObject<TowerStatEntity>
    {
        public TowerStat()
        {
            this.ValidationService = new TowerStatValidation();
        }

        public TowerStat(TowerStatEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new TowerStatValidation();
            }

            this.Id = entity.Id;
            this.Value = entity.Value;
            this.StatId = entity.StatEntityId;
            this.TowerId = entity.TowerEntityId;
        }

        public int Id { get; set; }

        public int StatId { get; set; }

        public int TowerId { get; set; }

        public float Value { get; set; }

        public override TowerStatEntity CreateEntity()
        {
            return new TowerStatEntity
            {
                Id = this.Id,
                StatEntityId = this.StatId,
                Value = this.Value,
                TowerEntityId = this.TowerId,
            };
        }
    }
}