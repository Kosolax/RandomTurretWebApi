namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class WaveMob : BaseBusinessObject<WaveMobEntity>
    {
        public WaveMob()
        {
            this.ValidationService = new WaveMobValidation();
        }

        public WaveMob(WaveMobEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new WaveMobValidation();
            }

            this.Id = entity.Id;
            this.Position = entity.Position;
            this.MobId = entity.MobEntityId;
            this.WaveId = entity.WaveEntityId;
        }

        public int Id { get; set; }

        public int MobId { get; set; }

        public int Position { get; set; }

        public int WaveId { get; set; }

        public override WaveMobEntity CreateEntity()
        {
            return new WaveMobEntity
            {
                Id = this.Id,
                MobEntityId = this.MobId,
                WaveEntityId = this.WaveId,
                Position = this.Position,
            };
        }
    }
}