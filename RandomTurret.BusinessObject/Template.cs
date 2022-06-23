namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Template : BaseBusinessObject<TemplateEntity>
    {
        public Template()
        {
            this.ValidationService = new TemplateValidation();
        }

        public Template(TemplateEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new TemplateValidation();
            }

            this.Id = entity.Id;
            this.RarityId = entity.RarityId;
            this.TemplateType = entity.TemplateType;
        }

        public Template(TemplateEntity entity, bool hasError, List<TemplateStat> templateStats) : this(entity, hasError)
        {
            this.TemplateStats = templateStats;
        }

        public int Id { get; set; }

        public int RarityId { get; set; }

        public List<TemplateStat> TemplateStats { get; set; }

        public TemplateType TemplateType { get; set; }

        public override TemplateEntity CreateEntity()
        {
            return new TemplateEntity
            {
                Id = this.Id,
                RarityId = this.RarityId,
                TemplateType = this.TemplateType,
            };
        }
    }
}