namespace RandomTurret.BusinessObject.Validation
{
    using RandomTurret.BusinessObject.Validation.Service;
    using RandomTurret.Entities;

    public class WaveValidation : ValidationService<WaveEntity>
    {
        public override bool Validate(WaveEntity itemToValidate)
        {
            this.Clear();

            return this.IsValid;
        }
    }
}