namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class GemStat : BaseBusinessObject<GemStatEntity>
    {
        public GemStat()
        {
            this.ValidationService = new GemStatValidation();
        }

        public GemStat(GemStatEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new GemStatValidation();
            }

            this.Id = entity.Id;
            this.Value = entity.Value;
            this.StatId = entity.StatId;
            this.GemId = entity.GemId;
            this.RarityId = entity.RarityId;
        }

        public int GemId { get; set; }

        public int Id { get; set; }

        public int RarityId { get; set; }

        public int StatId { get; set; }

        public float Value { get; set; }

        public override GemStatEntity CreateEntity()
        {
            return new GemStatEntity
            {
                Id = this.Id,
                StatId = this.StatId,
                GemId = this.GemId,
                RarityId = this.RarityId,
                Value = this.Value,
            };
        }
    }
}