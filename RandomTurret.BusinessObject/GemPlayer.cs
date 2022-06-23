namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class GemPlayer : BaseBusinessObject<GemPlayerEntity>
    {
        public GemPlayer()
        {
            this.ValidationService = new GemPlayerValidation();
        }

        public GemPlayer(GemPlayerEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new GemPlayerValidation();
            }

            this.Id = entity.Id;
            this.PlayerId = entity.PlayerId;
            this.GemId = entity.GemId;
        }

        public int GemId { get; set; }

        public int Id { get; set; }

        public int PlayerId { get; set; }

        public override GemPlayerEntity CreateEntity()
        {
            return new GemPlayerEntity
            {
                Id = this.Id,
                PlayerId = this.PlayerId,
                GemId = this.GemId,
            };
        }
    }
}