namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Resources;
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class PlayerValidation : ValidationService<PlayerEntity>
    {
        public override bool Validate(PlayerEntity itemToValidate)
        {
            this.Clear();
            this.ValidatePseudo(itemToValidate.Pseudo);
            this.ValidateMail(itemToValidate.Mail);
            this.ValidatePassword(itemToValidate.Password);

            return this.IsValid;
        }

        private void ValidateMail(string itemToValidate)
        {
            this.ValidateMailFormat(itemToValidate, nameof(PlayerValidationResource.Player_Mail_Format), PlayerValidationResource.Player_Mail_Format);
            this.ValidateStringRequired(itemToValidate, nameof(PlayerValidationResource.Player_Mail_Required), PlayerValidationResource.Player_Mail_Required);
            this.ValidateStringLength(itemToValidate, 100, nameof(PlayerValidationResource.Player_Mail_Max_Length), PlayerValidationResource.Player_Mail_Max_Length);
        }

        private void ValidatePassword(string itemToValidate)
        {
            this.ValidateStringRequired(itemToValidate, nameof(PlayerValidationResource.Player_Password_Required), PlayerValidationResource.Player_Password_Required);
            this.ValidateRegex(itemToValidate, @"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$", nameof(PlayerValidationResource.Player_Password_Regex), PlayerValidationResource.Player_Password_Regex);
        }

        private void ValidatePseudo(string itemToValidate)
        {
            this.ValidateStringMinLength(itemToValidate, 3, nameof(PlayerValidationResource.Player_Pseudo_Min_Length), PlayerValidationResource.Player_Pseudo_Min_Length);
            this.ValidateStringRequired(itemToValidate, nameof(PlayerValidationResource.Player_Pseudo_Required), PlayerValidationResource.Player_Pseudo_Required);
            this.ValidateStringLength(itemToValidate, 20, nameof(PlayerValidationResource.Player_Pseudo_Max_Length), PlayerValidationResource.Player_Pseudo_Max_Length);
        }
    }
}