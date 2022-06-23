namespace RandomTurret.BusinessObject
{
    using System.Collections.Generic;

    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;

    public class Wave : BaseBusinessObject<WaveEntity>
    {
        public Wave()
        {
            this.ValidationService = new WaveValidation();
        }

        public Wave(WaveEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new WaveValidation();
            }

            this.Id = entity.Id;
            this.DifficultyMultiplier = entity.DifficultyMultiplier;
            this.WaveNumber = entity.WaveNumber;
        }

        public Wave(WaveEntity entity, bool hasError, List<WaveMob> wavesMobs) : this(entity, hasError)
        {
            this.WavesMobs = wavesMobs;
        }

        public float DifficultyMultiplier { get; set; }

        public int Id { get; set; }

        public int WaveNumber { get; set; }

        public List<WaveMob> WavesMobs { get; set; }

        public override WaveEntity CreateEntity()
        {
            return new WaveEntity
            {
                Id = this.Id,
                WaveNumber = this.WaveNumber,
                DifficultyMultiplier = this.DifficultyMultiplier,
            };
        }
    }
}