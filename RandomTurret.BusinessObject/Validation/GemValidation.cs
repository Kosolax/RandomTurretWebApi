namespace RandomTurret.BusinessObject.Validation
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class GemValidation : ValidationService<GemEntity>
    {
        public override bool Validate(GemEntity itemToValidate)
        {
            this.Clear();
            this.ValidationGemType(itemToValidate.GemType);

            return this.IsValid;
        }

        private void ValidationGemType(GemType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int) Enum.GetValues(typeof(GemType)).Cast<GemType>().Max(), nameof(GemValidationResource.Gem_GemType_Range), GemValidationResource.Gem_GemType_Range);
        }
    }
}