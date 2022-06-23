namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class TowerStatValidation : ValidationService<TowerStatEntity>
    {
        public override bool Validate(TowerStatEntity itemToValidate)
        {
            this.Clear();
            this.ValidationStatId(itemToValidate.StatEntityId);

            return this.IsValid;
        }

        private void ValidationStatId(int statId)
        {
            this.ValidateIntMin(statId, 0, nameof(TowerStatValidationResource.TowerStat_StatId_Required), TowerStatValidationResource.TowerStat_StatId_Required);
        }
    }
}