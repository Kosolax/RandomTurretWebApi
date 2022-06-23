namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class GemPlayerValidation : ValidationService<GemPlayerEntity>
    {
        public override bool Validate(GemPlayerEntity itemToValidate)
        {
            this.Clear();
            this.ValidationPlayerId(itemToValidate.PlayerId);
            this.ValidationGemId(itemToValidate.GemId);

            return this.IsValid;
        }

        private void ValidationGemId(int gemId)
        {
            this.ValidateIntMin(gemId, 0, nameof(GemPlayerValidationResource.GemPlayer_GemId_Required), GemPlayerValidationResource.GemPlayer_GemId_Required);
        }

        private void ValidationPlayerId(int playerId)
        {
            this.ValidateIntMin(playerId, 0, nameof(GemPlayerValidationResource.GemPlayer_PlayerId_Required), GemPlayerValidationResource.GemPlayer_PlayerId_Required);
        }
    }
}