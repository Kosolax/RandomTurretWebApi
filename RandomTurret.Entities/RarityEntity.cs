namespace RandomTurret.Entities
{
    using RandomTurret.Entities.Enum;

    public class RarityEntity : BaseEntity
    {
        public int Id { get; set; }

        public float LootValue { get; set; }

        public RarityType RarityType { get; set; }
    }
}