namespace RandomTurret.BusinessObject
{
    using RandomTurret.BusinessObject.Validation;
    using RandomTurret.Entities;
    using RandomTurret.Entities.Enum;

    public class Rarity : BaseBusinessObject<RarityEntity>
    {
        public Rarity()
        {
            this.ValidationService = new RarityValidation();
        }

        public Rarity(RarityEntity entity, bool hasError) : base(entity)
        {
            if (hasError)
            {
                this.ValidationService = new RarityValidation();
            }

            this.Id = entity.Id;
            this.RarityType = entity.RarityType;
            this.LootValue = entity.LootValue;
        }

        public int Id { get; set; }

        public float LootValue { get; set; }

        public RarityType RarityType { get; set; }

        public override RarityEntity CreateEntity()
        {
            return new RarityEntity
            {
                Id = this.Id,
                RarityType = this.RarityType,
                LootValue = this.LootValue
            };
        }
    }
}