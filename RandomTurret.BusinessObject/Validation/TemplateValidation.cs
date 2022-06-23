namespace RandomTurret.BusinessObject.Validation
{
    using System;
    using System.Linq;

    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class TemplateValidation : ValidationService<TemplateEntity>
    {
        public override bool Validate(TemplateEntity itemToValidate)
        {
            this.Clear();
            this.ValidationTemplateType(itemToValidate.TemplateType);

            return this.IsValid;
        }

        private void ValidationTemplateType(TemplateType itemToValidate)
        {
            this.ValidateIntRange((int) itemToValidate, 0, (int) Enum.GetValues(typeof(TemplateType)).Cast<TemplateType>().Max(), nameof(TemplateValidationResource.Template_TemplateType_Range), TemplateValidationResource.Template_TemplateType_Range);
        }
    }
}