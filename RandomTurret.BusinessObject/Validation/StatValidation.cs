namespace RandomTurret.BusinessObject
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class StatValidation : ValidationService<StatEntity>
    {
        public override bool Validate(StatEntity itemToValidate)
        {
            this.Clear();
            this.ValidateStatType(itemToValidate.StatType);

            return this.IsValid;
        }

        private void ValidateStatType(StatType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int) Enum.GetValues(typeof(StatType)).Cast<StatType>().Max(), nameof(StatValidationResource.Stat_StatType_Range), StatValidationResource.Stat_StatType_Range);
        }
    }
}