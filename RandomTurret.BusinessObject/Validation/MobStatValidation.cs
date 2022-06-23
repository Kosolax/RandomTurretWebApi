namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class MobStatValidation : ValidationService<MobStatEntity>
    {
        public override bool Validate(MobStatEntity itemToValidate)
        {
            this.Clear();
            this.ValidationStatId(itemToValidate.StatEntityId);

            return this.IsValid;
        }

        private void ValidationStatId(int statId)
        {
            this.ValidateIntMin(statId, 0, nameof(MobStatValidationResource.MobStat_StatId_Required), MobStatValidationResource.MobStat_StatId_Required);
        }
    }
}