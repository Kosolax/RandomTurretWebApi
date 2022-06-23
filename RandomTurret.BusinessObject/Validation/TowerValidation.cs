namespace RandomTurret.BusinessObject.Validation
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TowerValidation : ValidationService<TowerEntity>
    {
        public override bool Validate(TowerEntity itemToValidate)
        {
            this.Clear();
            this.ValidationName(itemToValidate.Name);
            this.ValidationTowerType(itemToValidate.TowerType);

            return this.IsValid;
        }

        private void ValidationName(string itemToValidate)
        {
            this.ValidateStringRequired(itemToValidate, nameof(TowerValidationResource.Tower_Name_Required), TowerValidationResource.Tower_Name_Required);
            this.ValidateStringLength(itemToValidate, 100, nameof(TowerValidationResource.Tower_Name_Length), TowerValidationResource.Tower_Name_Length);
        }

        private void ValidationTowerType(TowerType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int)Enum.GetValues(typeof(TowerType)).Cast<TowerType>().Max(), nameof(TowerValidationResource.Tower_TowerType_Range), TowerValidationResource.Tower_TowerType_Range);
        }
    }
}