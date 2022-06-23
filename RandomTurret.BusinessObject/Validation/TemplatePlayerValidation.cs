namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class TemplatePlayerValidation : ValidationService<TemplatePlayerEntity>
    {
        public override bool Validate(TemplatePlayerEntity itemToValidate)
        {
            this.Clear();
            this.ValidationPlayerId(itemToValidate.PlayerId);
            this.ValidationTemplateId(itemToValidate.TemplateId);

            return this.IsValid;
        }

        private void ValidationPlayerId(int playerId)
        {
            this.ValidateIntMin(playerId, 0, nameof(TemplatePlayerValidationResource.TemplatePlayer_PlayerId_Required), TemplatePlayerValidationResource.TemplatePlayer_PlayerId_Required);
        }

        private void ValidationTemplateId(int templateId)
        {
            this.ValidateIntMin(templateId, 0, nameof(TemplatePlayerValidationResource.TemplatePlayer_TemplateId_Required), TemplatePlayerValidationResource.TemplatePlayer_TemplateId_Required);
        }
    }
}