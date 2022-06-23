namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class TemplateStat : BaseBusinessObject<TemplateStatEntity>
    {
        public TemplateStat()
        {
            this.ValidationService = new TemplateStatValidation();
        }

        public TemplateStat(TemplateStatEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new TemplateStatValidation();
            }

            this.Id = entity.Id;
            this.Value = entity.Value;
            this.StatId = entity.StatId;
            this.TemplateId = entity.TemplateId;
            this.RarityId = entity.RarityId;
        }

        public int Id { get; set; }

        public int RarityId { get; set; }

        public int StatId { get; set; }

        public int TemplateId { get; set; }

        public float Value { get; set; }

        public override TemplateStatEntity CreateEntity()
        {
            return new TemplateStatEntity
            {
                Id = this.Id,
                StatId = this.StatId,
                TemplateId = this.TemplateId,
                RarityId = this.RarityId,
                Value = this.Value,
            };
        }
    }
}