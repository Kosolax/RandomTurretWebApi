namespace RandomTurret.BusinessObject.Validation
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class RarityValidation : ValidationService<RarityEntity>
    {
        public override bool Validate(RarityEntity itemToValidate)
        {
            this.Clear();
            this.ValidationRarityType(itemToValidate.RarityType);
            return this.IsValid;
        }

        private void ValidationRarityType(RarityType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int) Enum.GetValues(typeof(RarityType)).Cast<RarityType>().Max(), nameof(RarityValidationResource.Rarity_RarityType_Range), RarityValidationResource.Rarity_RarityType_Range);
        }
    }
}