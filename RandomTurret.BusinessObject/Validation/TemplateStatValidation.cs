namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class TemplateStatValidation : ValidationService<TemplateStatEntity>
    {
        public override bool Validate(TemplateStatEntity itemToValidate)
        {
            this.Clear();
            this.ValidationStatId(itemToValidate.StatId);
            this.ValidationRarityId(itemToValidate.RarityId);

            return this.IsValid;
        }

        private void ValidationRarityId(int rarityId)
        {
            this.ValidateIntMin(rarityId, 0, nameof(TemplateStatValidationResource.TemplateStat_RarityId_Required), TemplateStatValidationResource.TemplateStat_RarityId_Required);
        }

        private void ValidationStatId(int statId)
        {
            this.ValidateIntMin(statId, 0, nameof(TemplateStatValidationResource.TemplateStat_StatId_Required), TemplateStatValidationResource.TemplateStat_StatId_Required);
        }
    }
}