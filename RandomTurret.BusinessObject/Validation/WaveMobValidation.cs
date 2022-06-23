namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class WaveMobValidation : ValidationService<WaveMobEntity>
    {
        public override bool Validate(WaveMobEntity itemToValidate)
        {
            this.Clear();

            return this.IsValid;
        }
    }
}