namespace RandomTurret.BusinessObject.Validation
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class MobValidation : ValidationService<MobEntity>
    {
        public override bool Validate(MobEntity itemToValidate)
        {
            this.Clear();
            this.ValidationMobType(itemToValidate.MobType);

            return this.IsValid;
        }

        private void ValidationMobType(MobType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int) Enum.GetValues(typeof(MobType)).Cast<MobType>().Max(), nameof(MobValidationResource.Mob_MobType_Range), MobValidationResource.Mob_MobType_Range);
        }
    }
}