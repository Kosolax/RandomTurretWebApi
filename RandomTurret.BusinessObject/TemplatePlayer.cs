namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class TemplatePlayer : BaseBusinessObject<TemplatePlayerEntity>
    {
        public TemplatePlayer()
        {
            this.ValidationService = new TemplatePlayerValidation();
        }

        public TemplatePlayer(TemplatePlayerEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new TemplatePlayerValidation();
            }

            this.Id = entity.Id;
            this.PlayerId = entity.PlayerId;
            this.TemplateId = entity.TemplateId;
        }

        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int TemplateId { get; set; }

        public override TemplatePlayerEntity CreateEntity()
        {
            return new TemplatePlayerEntity
            {
                Id = this.Id,
                PlayerId = this.PlayerId,
                TemplateId = this.TemplateId,
            };
        }
    }
}