namespace RandomTurret.BusinessObject
{
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Stat : BaseBusinessObject<StatEntity>
    {
        public Stat()
        {
            this.ValidationService = new StatValidation();
        }

        public Stat(StatEntity entity, bool hasError)
            : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new StatValidation();
            }

            this.Id = entity.Id;
            this.StatType = entity.StatType;
        }

        public int Id { get; set; }

        public StatType StatType { get; set; }

        public override StatEntity CreateEntity()
        {
            return new StatEntity
            {
                Id = this.Id,
                StatType = this.StatType,
            };
        }
    }
}