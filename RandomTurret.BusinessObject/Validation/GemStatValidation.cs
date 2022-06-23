namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class GemStatValidation : ValidationService<GemStatEntity>
    {
        public override bool Validate(GemStatEntity itemToValidate)
        {
            this.Clear();
            this.ValidationStatId(itemToValidate.StatId);
            this.ValidationRarityId(itemToValidate.RarityId);

            return this.IsValid;
        }

        private void ValidationRarityId(int rarityId)
        {
            this.ValidateIntMin(rarityId, 0, nameof(GemStatValidationResource.GemStat_RarityId_Required), GemStatValidationResource.GemStat_RarityId_Required);
        }

        private void ValidationStatId(int statId)
        {
            this.ValidateIntMin(statId, 0, nameof(GemStatValidationResource.GemStat_StatId_Required), GemStatValidationResource.GemStat_StatId_Required);
        }
    }
}